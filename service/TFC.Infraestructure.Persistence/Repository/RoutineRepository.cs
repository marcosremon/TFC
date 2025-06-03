using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using TFC.Application.DTO.Entity;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetAllUserRoutines;
using TFC.Application.DTO.Routine.GetRoutineById;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutineStats;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;
using TFC.Infraestructure.Persistence.Context;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class RoutineRepository : IRoutineRepository
    {
        private readonly ApplicationDbContext _context;

        public RoutineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreateRoutineResponse> CreateRoutine(CreateRoutineRequest createRoutineRequest)
        {
            CreateRoutineResponse response = new CreateRoutineResponse();
            using (ApplicationDbContext context = _context)
            {
                IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
                try
                {
                    User? user = await _context.Users
                        .Include(u => u.Routines)
                        .FirstOrDefaultAsync(u => u.Email == createRoutineRequest.UserEmail);

                    if (user == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "User not found";
                        return response;
                    }

                    Routine routine = new Routine
                    {
                        RoutineName = createRoutineRequest.RoutineName ?? "New Routine",
                        RoutineDescription = createRoutineRequest.RoutineDescription,
                        UserId = user.UserId,
                        SplitDays = new List<SplitDay>()
                    };

                    if (string.IsNullOrEmpty(routine.RoutineDescription))
                        routine.RoutineDescription = "Rutina sin descripcion";

                    _context.Routines.Add(routine);
                    await _context.SaveChangesAsync();

                    routine.SplitDays = createRoutineRequest.SplitDays.Select(sdRequest =>
                    {
                        SplitDay splitDay = new SplitDay
                        {
                            DayName = sdRequest.DayName.Value,
                            DayExercisesDescription = "Default description",
                            RoutineId = routine.RoutineId,
                            Exercises = sdRequest.Exercises?.Select(exRequest =>
                            {
                                Exercise exercise = new Exercise
                                {
                                    ExerciseName = exRequest.ExerciseName,
                                    Sets = exRequest.Sets ?? 0,
                                    Reps = exRequest.Reps ?? 0,
                                    Weight = exRequest.Weight ?? 0,
                                    RoutineId = routine.RoutineId,
                                    DayName = sdRequest.DayName.Value,
                                };

                                _context.Exercises.Add(exercise);
                                return exercise;
                            }).ToList() ?? new List<Exercise>()
                        };

                        return splitDay;
                    }).ToList();
                    
                    await _context.SaveChangesAsync();

                    foreach (SplitDay splitDay in routine.SplitDays)
                    {
                        foreach (Exercise exercise in splitDay.Exercises)
                        {
                            if (exercise.ExerciseName.IsNullOrEmpty())
                            {
                                response.IsSuccess = false;
                                response.Message = "the exercise can not be null or empty";
                                return response;
                            }

                            ExerciseProgress progress = new ExerciseProgress
                            {
                                ExerciseId = exercise.ExerciseId,
                                RoutineId = routine.RoutineId,
                                DayName = splitDay.DayName.ToString(),
                                Sets = exercise.Sets,
                                Reps = exercise.Reps,
                                Weight = exercise.Weight,
                                PerformedAt = DateTime.UtcNow
                            };
                            _context.ExerciseProgress.Add(progress);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    response.IsSuccess = true;
                    response.Message = "Routine created successfully";
                    response.RoutineDTO = new RoutineDTO
                    {
                        RoutineId = routine.RoutineId,
                        RoutineName = routine.RoutineName,
                        RoutineDescription = routine.RoutineDescription,
                        SplitDays = routine.SplitDays.Select(sd => new SplitDayDTO
                        {
                            DayName = sd.DayName,
                            Exercises = sd.Exercises.Select(e => new ExerciseDTO
                            {
                                ExerciseName = e.ExerciseName,
                                Sets = e.Sets,
                                Reps = e.Reps,
                                Weight = e.Weight,
                                DayName = e.DayName
                            }).ToList()
                        }).ToList()
                    };
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    response.IsSuccess = false;
                    response.Message = $"Error creating routine: {ex.Message}";
                }
            }

            return response;
        }

        public async Task<DeleteRoutineResponse> DeleteRoutine(DeleteRoutineRequest deleteRoutineRequest)
        {
            DeleteRoutineResponse response = new DeleteRoutineResponse();
            using (ApplicationDbContext context = _context)
            {
                IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
                try
                {
                    Routine? routine = _context.Routines.FirstOrDefault(r => r.RoutineId == deleteRoutineRequest.RoutineId);
                    if (routine == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Routine not found";
                        return response;
                    }

                    User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == deleteRoutineRequest.UserEmail);
                    if (user == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "User not found";
                        return response;
                    }

                    if (!user.Routines.Any(r => r.RoutineId == deleteRoutineRequest.RoutineId))
                    {
                        response.IsSuccess = false;
                        response.Message = "User doesn't have this routine";
                        return response;
                    }

                    List<Exercise> exercises = _context.Exercises.Where(e => e.RoutineId == routine.RoutineId).ToList();
                    if (exercises.Any())
                    {
                        _context.Exercises.RemoveRange(exercises);
                    }

                    List<ExerciseProgress> exerciseProgresses = _context.ExerciseProgress.Where(ep => ep.RoutineId == routine.RoutineId).ToList();
                    if (exerciseProgresses.Any())
                    {
                        _context.ExerciseProgress.RemoveRange(exerciseProgresses);
                    }

                    List<SplitDay> splitDays = _context.SplitDays.Where(sd => sd.RoutineId == routine.RoutineId).ToList();
                    if (splitDays.Any())
                    {
                        _context.SplitDays.RemoveRange(splitDays);
                    }

                    _context.Routines.Remove(routine);
                    user.Routines.Remove(routine);
                    await _context.SaveChangesAsync();

                    await dbContextTransaction.CommitAsync();

                    response.IsSuccess = true;
                    response.Message = "Routine deleted successfully";
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                    await dbContextTransaction.RollbackAsync();
                }

                return response;
            }
        }

        public async Task<GetAllUserRoutinesResponse> GetAllUserRoutines(GetAllUserRoutinesRequest getAllUserRoutinesRequest)
        {
            GetAllUserRoutinesResponse response = new GetAllUserRoutinesResponse();
            try
            {
                User? user = await _context.Users
                    .Include(u => u.Routines)
                    .ThenInclude(r => r.SplitDays)
                    .ThenInclude(sd => sd.Exercises)
                    .FirstOrDefaultAsync(u => u.Email == getAllUserRoutinesRequest.UserEmail);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found";
                    return response;
                }

                List<RoutineDTO> routines = user.Routines.Select(r => new RoutineDTO
                {
                    RoutineId = r.RoutineId,
                    RoutineName = r.RoutineName,
                    RoutineDescription = r.RoutineDescription,
                    SplitDays = r.SplitDays.Select(sd => new SplitDayDTO
                    {
                        DayName = sd.DayName,
                        Exercises = sd.Exercises.Select(e => new ExerciseDTO
                        {
                            ExerciseName = e.ExerciseName,
                            Sets = e.Sets,
                            Reps = e.Reps,
                            Weight = e.Weight
                        }).ToList()
                    }).ToList()
                }).ToList();

                response.IsSuccess = true;
                response.Message = "Routines retrieved successfully";
                response.Routines = routines;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<GetRoutineByIdResponse> GetRoutineById(GetRoutineByIdRequest getRoutineByIdRequest)
        {
            GetRoutineByIdResponse getRoutineByIdResponse = new GetRoutineByIdResponse();
            try
            {
                Routine? routine = _context.Routines
                    .Include(r => r.SplitDays)
                    .ThenInclude(sd => sd.Exercises)
                    .FirstOrDefault(r => r.RoutineId == getRoutineByIdRequest.RoutineId);
                if (routine == null)
                {
                    getRoutineByIdResponse.IsSuccess = false;
                    getRoutineByIdResponse.Message = "Routine not found";
                    return getRoutineByIdResponse;
                }

                RoutineDTO routineDTO = new RoutineDTO
                {
                    RoutineId = routine.RoutineId,
                    RoutineName = routine.RoutineName,
                    RoutineDescription = routine.RoutineDescription,
                    SplitDays = routine.SplitDays.Select(sd => new SplitDayDTO
                    {
                        DayName = sd.DayName,
                        Exercises = sd.Exercises.Select(e => new ExerciseDTO
                        {
                            ExerciseName = e.ExerciseName,
                            Sets = e.Sets,
                            Reps = e.Reps,
                            Weight = e.Weight
                        }).ToList()
                    }).ToList()
                };

                getRoutineByIdResponse.IsSuccess = true;
                getRoutineByIdResponse.Message = "Routine retrieved successfully";
                getRoutineByIdResponse.RoutineDTO = routineDTO;
            }
            catch (Exception ex)
            {
                getRoutineByIdResponse.IsSuccess = false;
                getRoutineByIdResponse.Message = ex.Message;
            }

            return getRoutineByIdResponse;
        }

        public async Task<GetRoutineStatsResponse> GetRoutineStats(GetRoutineStatsRequest getRoutineStatsRequest)
        {
            GetRoutineStatsResponse response = new GetRoutineStatsResponse();
            try
            {
                User? user = _context.Users
                    .Include(u => u.Routines)
                    .ThenInclude(r => r.SplitDays)
                    .ThenInclude(sd => sd.Exercises)
                    .FirstOrDefault(u => u.Email == getRoutineStatsRequest.UserEmail);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found";
                    return response;
                }

                List<Routine> routines = user.Routines.ToList();
                if (!routines.Any())
                {
                    response.routinesCount = 0;
                    response.splitsCount = 0;
                    response.exercisesCount = 0;
                    response.IsSuccess = true;
                    response.Message = "No routines found for the user";
                    return response;
                }

                List<SplitDay> splitDays = routines.SelectMany(r => r.SplitDays).ToList();
                if (!splitDays.Any())
                {
                    response.routinesCount = routines.Count;
                    response.splitsCount = 0;
                    response.exercisesCount = 0;
                    response.IsSuccess = true;
                    response.Message = "No split days found for the user's routines";
                    return response;
                }

                List<Exercise> exercises = splitDays.SelectMany(sd => sd.Exercises).ToList();
                if (!exercises.Any())
                {
                    response.routinesCount = routines.Count;
                    response.splitsCount = splitDays.Count;
                    response.exercisesCount = 0;
                    response.IsSuccess = true;
                    response.Message = "No exercises found for the user's split days";
                    return response;
                }

                response.routinesCount = routines.Count;
                response.splitsCount = splitDays.Count;
                response.exercisesCount = exercises.Count;
                response.IsSuccess = true;
                response.Message = "Routine stats retrieved successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<UpdateRoutineResponse> UpdateRoutine(UpdateRoutineRequest updateRoutineRequest)
        {
            UpdateRoutineResponse response = new UpdateRoutineResponse();
            try
            {
                Routine? routine = await _context.Routines.FirstOrDefaultAsync(r => r.RoutineId == updateRoutineRequest.RoutineId);
                if (routine == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Routine not found";
                    return response;
                }

                routine.RoutineName = updateRoutineRequest.RoutineName ?? routine.RoutineName;
                routine.RoutineDescription = updateRoutineRequest.RoutineDescription ?? routine.RoutineDescription;
                routine.SplitDays = updateRoutineRequest.SplitDays.Select(sd => new SplitDay
                {
                    DayName = sd.DayName,
                    Exercises = sd.Exercises.Select(e => new Exercise
                    {
                        ExerciseName = e.ExerciseName,
                        Sets = e.Sets,
                        Reps = e.Reps,
                        Weight = e.Weight
                    }).ToList()
                }).ToList() ?? routine.SplitDays;

                await _context.SaveChangesAsync();

                RoutineDTO routineDTO = new RoutineDTO
                {
                    RoutineName = routine.RoutineName,
                    RoutineDescription = routine.RoutineDescription,
                    SplitDays = routine.SplitDays.Select(sd => new SplitDayDTO
                    {
                        DayName = sd.DayName,
                        Exercises = sd.Exercises.Select(e => new ExerciseDTO
                        {
                            ExerciseName = e.ExerciseName,
                            Sets = e.Sets,
                            Reps = e.Reps,
                            Weight = e.Weight
                        }).ToList()
                    }).ToList()
                };

                response.IsSuccess = true;
                response.RoutineDTO = routineDTO;
                response.Message = "Routine updated successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
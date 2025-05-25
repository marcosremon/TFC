using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MongoDB.Driver;
using Org.BouncyCastle.Asn1.Ocsp;
using TFC.Application.DTO.Entity;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetAllUserRoutines;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutinesByFriendCode;
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
                        RoutineDescription = createRoutineRequest.RoutineDescription ?? "Rutina sin descripcion",
                        UserId = user.UserId,
                        SplitDays = new List<SplitDay>()
                    };

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
                                    ExerciseName = exRequest.ExerciseName ?? "Unnamed Exercise",
                                    Sets = exRequest.Sets ?? 0,
                                    Reps = exRequest.Reps ?? 0,
                                    Weight = exRequest.Weight ?? 0,
                                    RoutineId = routine.RoutineId,
                                    DayName = sdRequest.DayName.Value
                                };
                                return exercise;
                            }).ToList() ?? new List<Exercise>()
                        };

                        return splitDay;
                    }).ToList();

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
                                Weight = e.Weight
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

                    User? user = await _context.Users.FirstOrDefaultAsync(u => u.Dni == deleteRoutineRequest.UserDni);
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

        public async Task<GetRoutinesByFriendCodeResponse> GetRoutinesByFriendCode(GetRoutinesByFriendCodeRequest getRoutinesByFriendCodeRequest)
        {
            GetRoutinesByFriendCodeResponse response = new GetRoutinesByFriendCodeResponse();
            try
            {
                User? friend = _context.Users.Include(u => u.Routines)
                    .ThenInclude(r => r.SplitDays)
                    .ThenInclude(sd => sd.Exercises)
                    .FirstOrDefault(u => u.FriendCode == getRoutinesByFriendCodeRequest.FriendCode);

                if (friend == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found";
                    return response;
                }

                List<RoutineDTO> routines = friend.Routines.Select(r => new RoutineDTO
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
                response.FriendRoutines = routines;
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
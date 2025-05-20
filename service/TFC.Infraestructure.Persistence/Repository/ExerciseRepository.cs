using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Exercise.AddExercise;
using TFC.Application.DTO.Exercise.GetExercisesByDayName;
using TFC.Application.DTO.Exercise.UpdateExercise;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;
using TFC.Infraestructure.Persistence.Context;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddExerciseResponse> AddExercise(AddExerciseRequest addExerciseRequest)
        {
            AddExerciseResponse response = new AddExerciseResponse();
            try
            {
                User? user = await _context.Users
                    .Include(u => u.Routines)
                        .ThenInclude(r => r.SplitDays)
                            .ThenInclude(sd => sd.Exercises)
                    .FirstOrDefaultAsync(u => u.UserId == addExerciseRequest.UserId);

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                    return response;
                }

                Routine? routine = user.Routines.FirstOrDefault(r => r.RoutineId == addExerciseRequest.RoutineId);
                if (routine == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Routine not found or user doesn't have access.";
                    return response;
                }

                SplitDay? splitDay = routine.SplitDays.FirstOrDefault(sd => sd.DayName == addExerciseRequest.DayName);
                if (splitDay == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Split day not found.";
                    return response;
                }

                if (splitDay.Exercises.Any(e => e.ExerciseName.Equals(addExerciseRequest.ExerciseName, StringComparison.OrdinalIgnoreCase)))
                {
                    response.IsSuccess = false;
                    response.Message = "Exercise already exists in the split day.";
                    return response;
                }

                Exercise newExercise = new Exercise
                {
                    ExerciseName = addExerciseRequest.ExerciseName,
                    Sets = addExerciseRequest.Sets,
                    Reps = addExerciseRequest.Reps,
                    Weight = addExerciseRequest.Weight,
                };

                _context.Exercises.Add(newExercise);
                await _context.SaveChangesAsync();

                UserDTO userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    Routines = user.Routines.Select(r => new RoutineDTO
                    {
                        RoutineId = r.RoutineId,
                        RoutineName = r.RoutineName,
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
                    }).ToList()
                };

                response.IsSuccess = true;
                response.Message = "Exercise added successfully.";
                response.UserDTO = userDTO;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }
        
            return response;
        }

        public async Task<DeleteExerciseResponse> DeleteExercise(DeleteExerciseRequest deleteExerciseRequest)
        {
            DeleteExerciseResponse response = new DeleteExerciseResponse();
            try
            {
                User? user = await _context.Users
                        .Include(u => u.Routines)
                            .ThenInclude(r => r.SplitDays)
                                .ThenInclude(sd => sd.Exercises)
                        .FirstOrDefaultAsync(u => u.UserId == deleteExerciseRequest.UserId);

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                    return response;
                }

                Routine? routine = user.Routines.FirstOrDefault(r => r.RoutineId == deleteExerciseRequest.RoutineId);
                if (routine == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Routine not found or user doesn't have access.";
                    return response;
                }

                SplitDay? splitDay = routine.SplitDays.FirstOrDefault(sd => sd.DayName == deleteExerciseRequest.DayName);
                if (splitDay == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Split day not found.";
                    return response;
                }

                Exercise? exerciseToDelete = splitDay.Exercises.FirstOrDefault(e => e.ExerciseName.Equals(deleteExerciseRequest.ExerciseName));
                if (splitDay.Exercises.Any(e => e.ExerciseName.Equals(exerciseToDelete.ExerciseName)))
                {
                    response.IsSuccess = false;
                    response.Message = "Exercise not found in the split day.";
                    return response;
                }

                _context.Exercises.Remove(exerciseToDelete);
                await _context.SaveChangesAsync();
                
                UserDTO userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    Routines = user.Routines.Select(r => new RoutineDTO
                    {
                        RoutineId = r.RoutineId,
                        RoutineName = r.RoutineName,
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
                    }).ToList()
                };

                response.IsSuccess = true;
                response.Message = "Exercise deleted successfully.";
                response.UserDTO = userDTO;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<GetExercisesByDayNameResponse> GetExercisesByDayName(GetExercisesByDayNameRequest getExercisesByDayNameRequest)
        {
            GetExercisesByDayNameResponse response = new GetExercisesByDayNameResponse();
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == getExercisesByDayNameRequest.UserId);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                    return response;
                }

                Routine? routine = await _context.Routines.FirstOrDefaultAsync(r => r.RoutineId == getExercisesByDayNameRequest.RoutineId && r.UserId == user.UserId);
                if (routine == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Routine not found or user doesn't have access.";
                    return response;
                }

                SplitDay? splitDay = await _context.SplitDays.FirstOrDefaultAsync(sd => sd.DayName == getExercisesByDayNameRequest.DayName && sd.RoutineId == routine.RoutineId);
                if (splitDay == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Split day not found.";
                    return response;
                }

                List<ExerciseDTO> exercises = splitDay.Exercises.Select(e => new ExerciseDTO
                {
                    ExerciseId = e.ExerciseId,
                    ExerciseName = e.ExerciseName,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight,
                    DayName = e.DayName
                }).ToList();

                response.IsSuccess = true;
                response.Message = "Exercises retrieved successfully";
                response.Exercises = exercises;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<UpdateExerciseResponse> UpdateExercise(UpdateExerciseRequest updateExerciseRequest)
        {
            UpdateExerciseResponse response = new UpdateExerciseResponse();
            try
            {
                User? user = await _context.Users
                    .Include(u => u.Routines)
                        .ThenInclude(r => r.SplitDays)
                            .ThenInclude(sd => sd.Exercises)
                    .FirstOrDefaultAsync(u => u.UserId == updateExerciseRequest.UserId);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                    return response;
                }

                Routine? routine = user.Routines.FirstOrDefault(r => r.RoutineId == updateExerciseRequest.RoutineId);
                if (routine == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Routine not found or user doesn't have access.";
                    return response;
                }

                SplitDay? splitDay = routine.SplitDays.FirstOrDefault(sd => sd.DayName == updateExerciseRequest.DayName);
                if (splitDay == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Split day not found.";
                    return response;
                }

                Exercise? exerciseToUpdate = splitDay.Exercises.FirstOrDefault(e => e.ExerciseName.Equals(updateExerciseRequest.ExerciseName, StringComparison.OrdinalIgnoreCase));
                if (exerciseToUpdate == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Exercise not found in the split day.";
                    return response;
                }

                exerciseToUpdate.Sets = updateExerciseRequest.Sets;
                exerciseToUpdate.Reps = updateExerciseRequest.Reps;
                exerciseToUpdate.Weight = updateExerciseRequest.Weight;

                await _context.SaveChangesAsync();

                UserDTO userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    Routines = user.Routines.Select(r => new RoutineDTO
                    {
                        RoutineId = r.RoutineId,
                        RoutineName = r.RoutineName,
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
                    }).ToList()
                };

                response.IsSuccess = true;
                response.Message = "Exercise updated successfully.";
                response.UserDTO = userDTO;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error updating exercise: {ex.Message}";
            }

            return response;
        }
    }
}
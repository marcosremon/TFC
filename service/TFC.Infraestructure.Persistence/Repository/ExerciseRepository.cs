using Microsoft.EntityFrameworkCore;
using TFC.Application.DTO.Entity;
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
                    .FirstOrDefaultAsync(u => u.Email == addExerciseRequest.UserEmail);
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

                SplitDay? splitDay = routine.SplitDays.FirstOrDefault(sd => sd.DayName.ToString() == addExerciseRequest.DayName);
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
            var response = new GetExercisesByDayNameResponse();
            try
            {
                Routine? routine = await _context.Routines
                    .FirstOrDefaultAsync(r => r.RoutineId == getExercisesByDayNameRequest.RoutineId);
                if (routine == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Routine not found or user doesn't have access.";
                    return response;
                }

                // Traducción de día
                switch (getExercisesByDayNameRequest.DayName)
                {
                    case "Lunes": getExercisesByDayNameRequest.DayName = "Monday"; break;
                    case "Martes": getExercisesByDayNameRequest.DayName = "Tuesday"; break;
                    case "Miércoles": getExercisesByDayNameRequest.DayName = "Wednesday"; break;
                    case "Jueves": getExercisesByDayNameRequest.DayName = "Thursday"; break;
                    case "Viernes": getExercisesByDayNameRequest.DayName = "Friday"; break;
                    case "Sábado": getExercisesByDayNameRequest.DayName = "Saturday"; break;
                    case "Domingo": getExercisesByDayNameRequest.DayName = "Sunday"; break;
                    default: getExercisesByDayNameRequest.DayName = "Day"; break;
                }

                SplitDay? splitDay = await _context.SplitDays
                    .FirstOrDefaultAsync(sd => sd.DayName.ToString() == getExercisesByDayNameRequest.DayName && sd.RoutineId == routine.RoutineId);
                if (splitDay == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Split day not found.";
                    return response;
                }

                var exercises = await _context.Exercises
                    .Where(e => e.DayName == splitDay.DayName && e.RoutineId == splitDay.RoutineId)
                    .ToListAsync();

                var exercisesDTO = new List<ExerciseDTO>();
                var pastProgressDict = new Dictionary<long, List<string>>();

                // Convierte el DayName a string una vez fuera del bucle
                var dayNameString = splitDay.DayName.ToString();

                foreach (var exercise in exercises)
                {
                    // Busca los 3 últimos progresos de este ejercicio, ese día y rutina SIN usar .ToString() en la consulta
                    var last3Progress = await _context.ExerciseProgress
                        .Where(p => p.ExerciseId == exercise.ExerciseId
                                 && p.RoutineId == splitDay.RoutineId
                                 && p.DayName == dayNameString)
                        .OrderByDescending(p => p.PerformedAt)
                        .Take(3)
                        .ToListAsync();

                    var pastProgress = last3Progress
                        .Select(p => $"{p.Sets}x{p.Reps}@{p.Weight}kg")
                        .ToList();

                    pastProgressDict[exercise.ExerciseId] = pastProgress;

                    exercisesDTO.Add(new ExerciseDTO
                    {
                        ExerciseId = exercise.ExerciseId,
                        ExerciseName = exercise.ExerciseName,
                        Sets = exercise.Sets,
                        Reps = exercise.Reps,
                        Weight = exercise.Weight,
                        DayName = exercise.DayName
                    });
                }

                response.IsSuccess = true;
                response.Message = "Exercises retrieved successfully";
                response.Exercises = exercisesDTO;
                response.PastProgress = pastProgressDict;
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
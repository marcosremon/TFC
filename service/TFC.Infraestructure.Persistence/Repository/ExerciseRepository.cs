using Microsoft.EntityFrameworkCore;
using TFC.Application.DTO.Entity;
using TFC.Application.DTO.Exercise.AddExerciseProgress;
using TFC.Application.DTO.Exercise.DeleteExecise;
using TFC.Application.DTO.Exercise.GetExercisesByDayAndRoutineId;
using TFC.Application.DTO.Exercise.UpdateExercise;
using TFC.Application.DTO.Serialize_Deserialize;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;
using TFC.Infraestructure.Persistence.Context;
using TFC.Transversal.Common;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddExerciseAddExerciseProgressResponse> AddExerciseProgress(AddExerciseAddExerciseProgressRequest addExerciseRequest)
        {
            AddExerciseAddExerciseProgressResponse response = new AddExerciseAddExerciseProgressResponse();
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

                if (splitDay.Exercises.Count != addExerciseRequest.ExercisesProgres.Count)
                {
                    response.IsSuccess = false;
                    response.Message = $"La cantidad de ejercicios ({splitDay.Exercises.Count}) no coincide con los progresos enviados ({addExerciseRequest.ExercisesProgres.Count})";
                    return response;
                }

                for (int i = 0; i < splitDay.Exercises.Count; i++)
                {
                    Exercise exercise = splitDay.Exercises.ToList()[i];
                    string progressString = addExerciseRequest.ExercisesProgres[i];

                    DeserializeDTO progress = ExerciseProgressUtils.Deserialize(progressString);

                    if (progress == null)
                    {
                        response.IsSuccess = false;
                        response.Message = $"Error al deserializar el progreso del ejercicio: {exercise.ExerciseName}";
                        return response;
                    }

                    ExerciseProgress exerciseProgress = new ExerciseProgress
                    {
                        ExerciseId = exercise.ExerciseId,
                        RoutineId = routine.RoutineId,
                        DayName = splitDay.DayName.ToString(),
                        Sets = progress.Set,
                        Reps = progress.Reps,
                        Weight = (double) progress.Weight,
                        PerformedAt = DateTime.UtcNow
                    };

                    await _context.ExerciseProgress.AddAsync(exerciseProgress);
                    await _context.SaveChangesAsync();
                }

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

        public async Task<GetExercisesByDayAndRoutineIdResponse> GetExercisesByDayAndRoutineId(GetExercisesByDayAndRoutineIdRequest getExercisesByDayAndRoutineIdRequest)
        {
            GetExercisesByDayAndRoutineIdResponse response = new GetExercisesByDayAndRoutineIdResponse();
            try
            {
                Routine? routine = await _context.Routines.FirstOrDefaultAsync(r => r.RoutineId == getExercisesByDayAndRoutineIdRequest.RoutineId);
                if (routine == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Routine not found or user doesn't have access.";
                    return response;
                }

                switch (getExercisesByDayAndRoutineIdRequest.DayName)
                {
                    case "Lunes": getExercisesByDayAndRoutineIdRequest.DayName = "Monday"; break;
                    case "Martes": getExercisesByDayAndRoutineIdRequest.DayName = "Tuesday"; break;
                    case "Miércoles": getExercisesByDayAndRoutineIdRequest.DayName = "Wednesday"; break;
                    case "Jueves": getExercisesByDayAndRoutineIdRequest.DayName = "Thursday"; break;
                    case "Viernes": getExercisesByDayAndRoutineIdRequest.DayName = "Friday"; break;
                    case "Sábado": getExercisesByDayAndRoutineIdRequest.DayName = "Saturday"; break;
                    case "Domingo": getExercisesByDayAndRoutineIdRequest.DayName = "Sunday"; break;
                    default: getExercisesByDayAndRoutineIdRequest.DayName = "Day"; break;
                }

                SplitDay? splitDay = await _context.SplitDays
                    .FirstOrDefaultAsync(sd => sd.DayName.ToString() == getExercisesByDayAndRoutineIdRequest.DayName && sd.RoutineId == routine.RoutineId);
                if (splitDay == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Split day not found.";
                    return response;
                }

                List<Exercise> exercises = await _context.Exercises
                    .Where(e => e.DayName == splitDay.DayName && e.RoutineId == splitDay.RoutineId)
                    .ToListAsync();

                List<ExerciseDTO> exercisesDTO = new List<ExerciseDTO>();
                Dictionary<long, List<string>> pastProgressDict = new Dictionary<long, List<string>>();

                string? dayNameString = splitDay.DayName.ToString();

                foreach (Exercise exercise in exercises)
                {
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
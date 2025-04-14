using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
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

            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Dni == createRoutineRequest.UserDni);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found";
                    return response;
                }

                Routine routine = new Routine
                {
                    RoutineName = createRoutineRequest.RoutineName,
                    RoutineDescription = createRoutineRequest.RoutineDescription,
                    SplitDays = createRoutineRequest.SplitDays.Select(sd => new SplitDay
                    {
                        DayName = sd.DayName,
                        Exercises = sd.Exercises.Select(e => new Exercise
                        {
                            ExerciseName = e.ExerciseName,
                            Sets = e.Sets,
                            Reps = e.Reps,
                            Weight = e.Weight
                        }).ToList()
                    }).ToList()
                };

                await _context.Routines.AddAsync(routine);
                await _context.SaveChangesAsync();
                
                if (user.Routines == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User routines not found";
                    return response;
                }

                user.Routines.Add(routine);
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
                        }).ToList() ?? new List<ExerciseDTO>(),
                    }).ToList() ?? new List<SplitDayDTO>(),
                };

                response.IsSuccess = true;
                response.RoutineDTO = routineDTO;
                response.Message = "Routine created successfully";
                return response;
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
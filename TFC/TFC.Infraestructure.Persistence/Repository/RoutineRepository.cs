using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Routine.CreateRoutine;
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

        public async Task<RoutineDTO?> CreateRoutine(CreateRoutineRequest createRoutineRequest)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Dni == createRoutineRequest.UserDni);
                if (user == null)
                {
                    return null;
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
                    user.Routines = new List<Routine>();
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

                return routineDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating routine " + ex.Message);
            }
        }

        public async Task<RoutineDTO?> UpdateRoutine(UpdateRoutineRequest updateRoutineRequest)
        {
            try
            {
                Routine? routine = await _context.Routines.FirstOrDefaultAsync(r => r.RoutineId == updateRoutineRequest.RoutineId);

                if (routine == null)
                {
                    return null;
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

                return new RoutineDTO
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
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating routine", ex);
            }
        }
    }
}
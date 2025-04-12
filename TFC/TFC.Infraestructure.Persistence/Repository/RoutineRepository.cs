using MongoDB.Driver;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;

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
                User? user = await _context.Users.Find(u => u.Dni == createRoutineRequest.UserDni).FirstOrDefaultAsync();
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
                        }).ToList() ?? new List<Exercise>(),
                    }).ToList() ?? new List<SplitDay>(),
                };

                user.Routines.Add(routine);

                await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
                await _context.Routines.InsertOneAsync(routine);

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
                throw new Exception("Error creating routine", ex);
            }
        }
    }
}
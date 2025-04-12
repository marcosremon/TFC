using MongoDB.Bson;
using MongoDB.Driver;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.User.GetUserByEmail;
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
                User userr = await _context.Users.Find(u => u.Dni == createRoutineRequest.UserDni).FirstOrDefaultAsync();
                if (userr == null)
                {
                    throw new Exception();
                }

                Routine routine = new Routine
                {
                    Id = ObjectId.GenerateNewId().ToString(), 
                    RoutineName = createRoutineRequest.RoutineName,
                    RoutineDescription = createRoutineRequest.RoutineDescription,
                    SplitDays = createRoutineRequest.SplitDays.Select(sd => new SplitDay
                    {
                        Id = ObjectId.GenerateNewId().ToString(), 
                        DayName = sd.DayName,
                        Exercises = sd.Exercises.Select(e => new Exercise
                        {
                            Id = ObjectId.GenerateNewId().ToString(),
                            ExerciseName = e.ExerciseName,
                            Sets = e.Sets,
                            Reps = e.Reps,
                            Weight = e.Weight
                        }).ToList() ?? new List<Exercise>(),
                    }).ToList() ?? new List<SplitDay>(),
                };

                await _context.Routines.InsertOneAsync(routine);

                string routineId = routine.Id;
                userr.RoutinesIds.Add(routineId);

                await _context.Users.ReplaceOneAsync(u => u.Id == userr.Id, userr);

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
                RoutineDTO routine = new RoutineDTO();
                return routine;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating routine", ex);
            }
        }
    }
}
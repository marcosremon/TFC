using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TFC.Application.DTO.Entity;
using TFC.Application.DTO.Exercise.GetExercisesByDayAndRoutineId;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.UpdateSplitDay;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;
using TFC.Infraestructure.Persistence.Context;
using TFC.Transversal.GenericUtils;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class SplitDayRepository : ISplitDayRepository
    {
        private readonly ApplicationDbContext _context;

        public SplitDayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteSplitDayResponse> DeleteSplitDay(DeleteSplitDayRequest deleteSplitDayRequest)
        {
            DeleteSplitDayResponse response = new DeleteSplitDayResponse();
            using (ApplicationDbContext context = _context)
            {
                IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
                try
                {
                    User? user = await context.Users
                        .Include(u => u.Routines)
                        .ThenInclude(r => r.SplitDays)
                        .FirstOrDefaultAsync(u => u.UserId == deleteSplitDayRequest.UserId);

                    if (user == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "User not found.";
                    }

                    Routine? routine = user.Routines.FirstOrDefault(r => r.RoutineId == deleteSplitDayRequest.RoutineId);
                    if (routine == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Routine not found.";
                    }

                    SplitDay? splitDay = routine.SplitDays.FirstOrDefault(sd => sd.DayName == deleteSplitDayRequest.DayName);
                    if (splitDay == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Split day not found.";
                    }

                    routine.SplitDays.Remove(splitDay);

                    await context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();

                    response.IsSuccess = true;
                    response.Message = "Split day deleted successfully.";
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                    await dbContextTransaction.RollbackAsync();
                }
            }

            return response;
        }

        public async Task<UpdateSplitDayResponse> UpdateSplitDay(UpdateSplitDayRequest updateSplitDayRequest)
        {
            UpdateSplitDayResponse response = new UpdateSplitDayResponse();
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == updateSplitDayRequest.UserEmail);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                    return response;
                }

                Routine? routine = await _context.Routines.FirstOrDefaultAsync(r => r.RoutineId == updateSplitDayRequest.RoutineId);
                if (routine == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Routine not found.";
                    return response;
                }

                if (!user.Routines.Any(r => r.RoutineId == routine.RoutineId))
                {
                    response.IsSuccess = false;
                    response.Message = "User does not have this routine.";
                    return response;
                }

                if (updateSplitDayRequest.DeleteDays.Count == 0 && updateSplitDayRequest.AddDays.Count == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "No days to delete or add";
                    return response;
                }

                if (updateSplitDayRequest.DeleteDays.Count > 0)
                {
                    updateSplitDayRequest.DeleteDays.ForEach(dayName =>
                    {
                        dayName = GenericUtils.ChangeDayLanguage(dayName);

                        // Buscar el SplitDay a eliminar
                        SplitDay? splitDayToDelete = _context.SplitDays
                            .Include(sd => sd.Exercises)
                            .FirstOrDefault(sd => sd.DayName.ToString() == dayName && sd.RoutineId == routine.RoutineId);

                        if (splitDayToDelete != null)
                        {
                            // Obtener los IDs de los ejercicios de ese día
                            var exerciseIds = splitDayToDelete.Exercises.Select(e => e.ExerciseId).ToList();

                            // Eliminar los ExerciseProgress asociados
                            var progressToDelete = _context.ExerciseProgress.Where(ep => exerciseIds.Contains(ep.ExerciseId));
                            _context.ExerciseProgress.RemoveRange(progressToDelete);

                            // Eliminar los ejercicios asociados
                            _context.Exercises.RemoveRange(splitDayToDelete.Exercises);

                            // Eliminar el SplitDay
                            _context.SplitDays.Remove(splitDayToDelete);
                        }
                    });
                }

                if (updateSplitDayRequest.AddDays.Count > 0)
                {
                    updateSplitDayRequest.AddDays.ForEach(dayName =>
                    {
                        dayName = GenericUtils.ChangeDayLanguage(dayName);

                        WeekDay weekDay = Enum.Parse<WeekDay>(dayName, true);
                        SplitDay newSplitDay = new SplitDay
                        {
                            DayName = weekDay,
                            RoutineId = routine.RoutineId,
                            Exercises = new List<Exercise>()
                        };
                        routine.SplitDays.Add(newSplitDay);
                    });
                }

                await _context.SaveChangesAsync();

                UserDTO userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    FriendCode = user.FriendCode,
                    Email = user.Email,
                    Routines = user.Routines.Select(r => new RoutineDTO
                    {
                        RoutineId = r.RoutineId,
                        RoutineName = r.RoutineName,
                        SplitDays = r.SplitDays.Select(sd => new SplitDayDTO
                        {
                            DayName = sd.DayName,
                            Exercises = sd.Exercises.Select(e => new ExerciseDTO
                            {
                                ExerciseId = e.ExerciseId,
                                ExerciseName = e.ExerciseName,
                                Reps = e.Reps,
                                Sets = e.Sets
                            }).ToList()
                        }).ToList()
                    }).ToList()
                };

                response.IsSuccess = true;
                response.Message = "Split day updated successfully.";
                response.UserDTO = userDTO;
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
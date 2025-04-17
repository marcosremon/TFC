using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.SplitDay.ActualizarSplitDay;
using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;
using TFC.Infraestructure.Persistence.Context;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class SplitDayRepository : ISplitDayRepository
    {
        private readonly ApplicationDbContext _context;

        public SplitDayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddSplitDayResponse> CreateSplitDay(AddSplitDayRequest addSplitDayRequest)
        {
            AddSplitDayResponse response = new AddSplitDayResponse();

            using (ApplicationDbContext context = _context)
            {
                IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();

                try
                {
                    User? user = await context.Users
                        .Include(u => u.Routines)
                        .ThenInclude(r => r.SplitDays)
                        .FirstOrDefaultAsync(u => u.UserId == addSplitDayRequest.UserId);

                    if (user == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "User not found.";
                    }

                    Routine? routine = user.Routines.FirstOrDefault(r => r.RoutineId == addSplitDayRequest.RoutineId);
                    if (routine == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Routine not found.";
                    }

                    SplitDay splitDay = new SplitDay
                    {
                        DayName = addSplitDayRequest.DayName,
                        Exercises = addSplitDayRequest.Exercises.Select(e => new Exercise
                        {
                            ExerciseId = e.ExerciseId,
                            ExerciseName = e.ExerciseName,
                            Reps = e.Reps,
                            Sets = e.Sets
                        }).ToList()
                    };

                    routine.SplitDays.Add(splitDay);
                    await context.SaveChangesAsync();

                    await dbContextTransaction.CommitAsync();

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
                    response.Message = "Split day created successfully.";
                    response.UserDTO = userDTO;
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                }
            }

            return response;
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

                    // No es necesario eliminarlo del user por que entity framework core lo hace automaticamente
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
                }
            }

            return response;
        }

        public async Task<GetAllUserSplitsResponse> GetAllUserSplits(GetAllUserSplitsRequest getAllUserSplitsResponse)
        {
            GetAllUserSplitsResponse response = new GetAllUserSplitsResponse();

            try
            {
                User? user = await _context.Users
                    .Include(u => u.Routines)
                    .ThenInclude(r => r.SplitDays)
                    .ThenInclude(sd => sd.Exercises)
                    .FirstOrDefaultAsync(u => u.UserId == getAllUserSplitsResponse.UserId);

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                    return response;
                }

                List<SplitDayDTO> routinesDTO = user.Routines.SelectMany(r => r.SplitDays).Select(sd => new SplitDayDTO
                {
                    DayName = sd.DayName,
                    Exercises = sd.Exercises.Select(e => new ExerciseDTO
                    {
                        ExerciseId = e.ExerciseId,
                        ExerciseName = e.ExerciseName,
                        Reps = e.Reps,
                        Sets = e.Sets
                    }).ToList()
                }).ToList();

                response.IsSuccess = true;
                response.Message = "User routines retrieved successfully.";
                response.SplitDays = routinesDTO;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ActualizarSplitDayResponse> UpdateSplitDay(ActualizarSplitDayRequest actualizarSplitDayRequest)
        {
            throw new NotImplementedException();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
using TFC.Application.DTO.User.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.UpdateUser;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;
using TFC.Infraestructure.Persistence.Context;
using TFC.Transversal.Security;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangePasswordWithPasswordAndEmail(ChangePasswordWithPasswordAndEmailRequest changePasswordRequest)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == changePasswordRequest.UserEmail);

                if (user == null)
                {
                    return false;
                }

                string decodePassword = PasswordUtils.PasswordDecoder(user.Password);
                if (decodePassword != changePasswordRequest.OldPassword)
                {
                    return false;
                }

                user.Password = PasswordUtils.PasswordEncoder(changePasswordRequest.NewPassword);
                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    return false;
                }

                Mails.SendEmail(user.Username, user.Email, changePasswordRequest.NewPassword);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña: " + ex.Message);
            }
        }

        public async Task<bool> CreateNewPassword(CreateNewPasswordRequest createNewPasswordRequest)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == createNewPasswordRequest.UserEmail);
                if (user == null)
                {
                    return false;
                }

                string newPassword = PasswordUtils.CreatePassword(8);
                byte[] passwordHash = PasswordUtils.PasswordEncoder(newPassword);

                user.Password = passwordHash;
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    return false;
                }

                Mails.SendEmail(user.Username, user.Email, newPassword);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la nueva contraseña: " + ex.Message);
            }
        }

        public async Task<UserDTO> CreateUser(CreateUserRequst createUserRequst)
        {
            try
            {
                User? existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == createUserRequst.Email);
                if (existingUser != null)
                {
                    throw new Exception("Ya existe un usuario con ese email");
                }

                User user = new User()
                {
                    Dni = createUserRequst.Dni,
                    Username = createUserRequst.Username,
                    Surname = createUserRequst.Surname,
                    Password = PasswordUtils.PasswordEncoder(createUserRequst.Password),
                    Email = createUserRequst.Email,
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                UserDTO createdUserDTO = new UserDTO()
                {
                    Dni = user.Dni,
                    Username = user.Username,
                    Surname = user.Surname,
                    Password = "********",
                    Email = user.Email
                };

                return createdUserDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario: " + ex.Message);
            }
        }

        public async Task<bool> DeleteUser(DeleteUserRequest deleteUserRequest)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Dni == deleteUserRequest.Dni);

                _context.Users.Remove(user);
                int affectedRows = await _context.SaveChangesAsync();

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario: " + ex.Message);
            }
        }

        public async Task<UserDTO?> GetUserByEmail(GetUserByEmailRequest getUserByEmailRequest)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == getUserByEmailRequest.Email);
                if (user == null)
                {
                    return null;
                }

                UserDTO userDTO = new UserDTO()
                {
                    Dni = user.Dni,
                    Username = user.Username,
                    Surname = user.Surname,
                    Password = "********",
                    Email = user.Email,
                    Routines = user.Routines.Select(r => new RoutineDTO
                    {
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
                            }).ToList() ?? new List<ExerciseDTO>()
                        }).ToList() ?? new List<SplitDayDTO>()
                    }).ToList() ?? new List<RoutineDTO>()
                };

                return userDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por email: " + ex.Message);
            }
        }

        public async Task<List<UserDTO>?> GetUsers()
        {
            try
            {
                List<User> users = await _context.Users.AsNoTracking().ToListAsync();
                if (users == null || users.Count == 0)
                {
                    return null;
                }

                List<UserDTO> userDTOs = users.Select(user => new UserDTO()
                {
                    Dni = user.Dni,
                    Username = user.Username,
                    Surname = user.Surname,
                    Password = "********",
                    Email = user.Email,
                    Routines = user.Routines.Select(r => new RoutineDTO
                    {
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
                            }).ToList() ?? new List<ExerciseDTO>()
                        }).ToList() ?? new List<SplitDayDTO>()
                    }).ToList() ?? new List<RoutineDTO>()
                }).ToList();

                return userDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }
        }

        public async Task<UserDTO?> UpdateUser(UpdateUserRequst updateUserRequest)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Dni == updateUserRequest.DniToBeFound);
                if (user == null)
                {
                    return null;
                }

                user.Username = updateUserRequest.Username;
                user.Surname = updateUserRequest.Surname;
                user.Password = PasswordUtils.PasswordEncoder(updateUserRequest.Password);
                user.Email = updateUserRequest.Email;
                //if (updateUserRequest.Routines != null)
                //{
                //    user.Routines = updateUserRequest.Routines.Select(routine => new Routine()
                //    {
                //        RoutineName = routine.RoutineName,
                //        RoutineDescription = routine.RoutineDescription,
                //        SplitDays = routine.SplitDays?.Select(splitDay => new SplitDay()
                //        {
                //            DayName = splitDay.DayName,
                //            Exercises = splitDay.Exercises?.Select(exercise => new Exercise()
                //            {
                //                ExerciseName = exercise.ExerciseName,
                //                Sets = exercise.Sets,
                //                Reps = exercise.Reps,
                //                Weight = exercise.Weight
                //            }).ToList() ?? new List<Exercise>()
                //        }).ToList() ?? new List<SplitDay>()
                //    }).ToList();
                //}

                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    return null;
                }

                return await GetUserByEmail(new GetUserByEmailRequest()
                {
                    Email = user.Email
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message);
            }
        }
    }
}
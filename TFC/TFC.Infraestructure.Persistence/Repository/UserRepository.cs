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

        public async Task<bool> ChangePasswordWithPasswordAndEmail(ChangePasswordWithPasswordAndEmailRequest chagePasswordRequest)
        {
            try
            {
                User? user = await _context.Users
               .Find(u => u.Email == chagePasswordRequest.UserEmail && chagePasswordRequest.OldPassword == PasswordUtils.PasswordDecoder(u.Password))
               .FirstOrDefaultAsync();
                if (user == null)
                {
                    return false;
                }

                byte[] passwordHash = PasswordUtils.PasswordEncoder(chagePasswordRequest.NewPassword);
                var filter = Builders<User>.Filter.Eq(u => u.Email, chagePasswordRequest.UserEmail);
                var update = Builders<User>.Update
                    .Set(u => u.Password, passwordHash);

                var updateResult = await _context.Users.UpdateOneAsync(filter, update);

                if (updateResult.ModifiedCount == 0)
                {
                    return false;
                }

                Mails.SendEmail(user.Username, user.Email, chagePasswordRequest.NewPassword);
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
                User? user = await _context.Users.Find(u => u.Email == createNewPasswordRequest.UserEmail).FirstOrDefaultAsync();
                if (user == null)
                {
                    return false;
                }

                string newPassword = PasswordUtils.CreatePassword(8);
                byte[] passwordHash = PasswordUtils.PasswordEncoder(newPassword);

                var filter = Builders<User>.Filter.Eq(u => u.Email, createNewPasswordRequest.UserEmail);
                var update = Builders<User>.Update
                    .Set(u => u.Password, passwordHash);

                var updateResult = await _context.Users.UpdateOneAsync(filter, update);

                if (updateResult.ModifiedCount == 0)
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
                User existingUser = await _context.Users.Find(u => u.Email == createUserRequst.Email).FirstOrDefaultAsync();
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

                await _context.Users.InsertOneAsync(user);

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
                var deleteResult = await _context.Users.DeleteOneAsync(u => u.Dni == deleteUserRequest.Dni);
                return deleteResult.DeletedCount > 0;
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
                User user = await _context.Users.Find(u => u.Email == getUserByEmailRequest.Email).FirstOrDefaultAsync();
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
                    RoutinesIds = user.RoutinesIds
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
                List<User> users = await _context.Users.Find(_ => true).ToListAsync();
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
                    RoutinesIds = user.RoutinesIds
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
                User? user = await _context.Users.Find(u => u.Dni == updateUserRequest.DniToBeFound).FirstOrDefaultAsync();
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

                var filter = Builders<User>.Filter.Eq(u => u.Dni, updateUserRequest.DniToBeFound);
                var result = await _context.Users.ReplaceOneAsync(filter, user);

                if (result.ModifiedCount == 0)
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TFC.Application.DTO.Entity;
using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
using TFC.Application.DTO.User.CreateGenericUser;
using TFC.Application.DTO.User.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.GetUsers;
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

        public async Task<ChangePasswordWithPasswordAndEmailResponse> ChangePasswordWithPasswordAndEmail(ChangePasswordWithPasswordAndEmailRequest changePasswordRequest)
        {
            ChangePasswordWithPasswordAndEmailResponse response = new ChangePasswordWithPasswordAndEmailResponse();
            using (ApplicationDbContext context = _context)
            {
                IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
                try
                {
                    User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == changePasswordRequest.UserEmail);
                    if (user == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "No se encontró el usuario con ese email";
                        return response;
                    }

                    if (PasswordUtils.PasswordDecoder(user.Password) != changePasswordRequest.OldPassword)
                    {
                        response.IsSuccess = false;
                        response.Message = "La contraseña actual no es correcta";
                        return response;
                    }

                    user.Password = PasswordUtils.PasswordEncoder(changePasswordRequest.NewPassword);
                    int affectedRows = await _context.SaveChangesAsync();

                    if (affectedRows == 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "No se pudo cambiar la contraseña";
                        return response;
                    }

                    dbContextTransaction.Commit();

                    MailUtils.SendEmail(user.Username, user.Email, changePasswordRequest.NewPassword);
                    response.IsSuccess = true;
                    response.Message = "Contraseña cambiada correctamente";
                    response.UserId = user.UserId;
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                    dbContextTransaction.Rollback();
                }

                return response;
            }
        }

        public async Task<CreateNewPasswordResponse> CreateNewPassword(CreateNewPasswordRequest createNewPasswordRequest)
        {
            CreateNewPasswordResponse response = new CreateNewPasswordResponse();
            using (ApplicationDbContext context = _context)
            {
                IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
                try
                {
                    User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == createNewPasswordRequest.UserEmail);
                    if (user == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "No se encontró el usuario con ese email";
                        return response;
                    }

                    string newPassword = PasswordUtils.CreatePassword(8);
                    byte[] passwordHash = PasswordUtils.PasswordEncoder(newPassword);

                    user.Password = passwordHash;
                    int affectedRows = await _context.SaveChangesAsync();

                    if (affectedRows == 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "No se pudo cambiar la contraseña";
                    }

                    dbContextTransaction.Commit();

                    MailUtils.SendEmail(user.Username, user.Email, newPassword);
                    response.IsSuccess = true;
                    response.Message = "Contraseña cambiada correctamente";
                    response.UserId = user.UserId;
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                    dbContextTransaction.Rollback();
                }

                return response;
            }
        }

        public async Task<CreateUserResponse> CreateUser(CreateGenericUserRequest createGenericUserRequest)
        {
            CreateUserResponse response = new CreateUserResponse();
            try
            {
                if (!MailUtils.IsEmailValid(createGenericUserRequest.Email))
                {
                    response.IsSuccess = false;
                    response.Message = "El email no es valido";
                    return response;
                }

                if (createGenericUserRequest.Dni.Length != 9
                  || !createGenericUserRequest.Dni.Substring(0, 8).All(char.IsDigit)
                  || !char.IsUpper(createGenericUserRequest.Dni[8]))
                {
                    response.IsSuccess = false;
                    response.Message = "El DNI debe tener 8 números seguidos de 1 letra mayúscula";
                    return response;
                }

                if (await _context.Users.FirstOrDefaultAsync(u => u.Dni == createGenericUserRequest.Dni) != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Ya existe un usuario con ese DNI";
                    return response;
                }

                if (await _context.Users.FirstOrDefaultAsync(u => u.Email == createGenericUserRequest.Email) != null)
                {
                    response.IsSuccess = false;
                    response.Message = "Ya existe un usuario con ese email";
                    return response;
                }

                if (!PasswordUtils.IsPasswordValid(createGenericUserRequest.Password))
                {
                    response.IsSuccess = false;
                    response.Message = "La contraseña no es valida debe tener ocho o mas caracteres, mayusculas, minusculas y al menos un simbolo";
                    return response;
                }

                if (createGenericUserRequest.Password != createGenericUserRequest.ConfirmPassword)
                {
                    response.IsSuccess = false;
                    response.Message = "Las contraseñas no coinciden";
                    return response;
                }

                String friendCode = PasswordUtils.CreatePassword(8);
                while (true)
                {
                    if (await _context.Users.FirstOrDefaultAsync(u => u.FriendCode == friendCode) == null)
                    {
                        break;
                    }
                    friendCode = PasswordUtils.CreatePassword(8);
                }

                User user = new User()
                {
                    Dni = createGenericUserRequest.Dni,
                    Username = createGenericUserRequest.Username,
                    Surname = createGenericUserRequest.Surname,
                    FriendCode = friendCode,
                    Password = PasswordUtils.PasswordEncoder(createGenericUserRequest.Password),
                    Email = createGenericUserRequest.Email,
                    Role = createGenericUserRequest.Role,
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                UserDTO userDTO = new UserDTO()
                {
                    Dni = user.Dni,
                    Username = user.Username,
                    FriendCode = user.FriendCode,
                    Surname = user.Surname,
                    Password = "********",
                    Email = user.Email,
                    Role = user.Role
                };

                response.IsSuccess = true;
                response.Message = "Usuario creado correctametne";
                response.UserDTO = userDTO;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest)
        {
            DeleteUserResponse response = new DeleteUserResponse();
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Dni == deleteUserRequest.Dni);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el usuario con ese dni";
                    return response;
                }

                _context.Users.Remove(user);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo eliminar el usuario";
                    return response;
                }

                response.IsSuccess = true;
                response.Message = "Usuario eliminado correctamente";
                response.UserId = user.UserId;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<GetUserByEmailResponse> GetUserByEmail(GetUserByEmailRequest getUserByEmailRequest)
        {
            GetUserByEmailResponse response = new GetUserByEmailResponse();
            try
            {
                User? user = await _context.Users.Include(u => u.Routines)
                        .ThenInclude(r => r.SplitDays)
                            .ThenInclude(sd => sd.Exercises)
                    .FirstOrDefaultAsync(u => u.Email == getUserByEmailRequest.Email);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el usuario con ese email";
                    return response;
                }

                UserDTO userDTO = new UserDTO()
                {
                    UserId = user.UserId,
                    Dni = user.Dni,
                    Username = user.Username,
                    Surname = user.Surname,
                    FriendCode = user.FriendCode,
                    Password = "********",
                    Email = user.Email,
                    Routines = user.Routines.Select(r => new RoutineDTO
                    {
                        RoutineId = r.RoutineId,
                        RoutineName = r.RoutineName,
                        UserId = r.UserId,
                        RoutineDescription = r.RoutineDescription,
                        SplitDays = r.SplitDays.Select(sd => new SplitDayDTO
                        {
                            DayName = sd.DayName,
                            Exercises = sd.Exercises.Select(e => new ExerciseDTO
                            {
                                ExerciseId = e.ExerciseId,
                                ExerciseName = e.ExerciseName,
                                Sets = e.Sets,
                                Reps = e.Reps,
                                Weight = e.Weight
                            }).ToList() ?? new List<ExerciseDTO>()
                        }).ToList() ?? new List<SplitDayDTO>()
                    }).ToList() ?? new List<RoutineDTO>()
                };

                response.IsSuccess = true;
                response.Message = "Consulta correcta";
                response.UserDTO = userDTO;
                response.routinesCount = userDTO.Routines.Count;
                response.friendsCount = await _context.UserFriends.CountAsync(u => u.UserId == userDTO.UserId);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<GetUsersResponse> GetUsers()
        {
            GetUsersResponse response = new GetUsersResponse();
            try
            {
                List<User> users = await _context.Users
                    .AsNoTracking()
                    .Include(u => u.Routines)  
                        .ThenInclude(r => r.SplitDays)  
                            .ThenInclude(sd => sd.Exercises)  
                    .ToListAsync();

                if (users == null || !users.Any())
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontraron usuarios";
                    return response;
                }

                List<UserDTO> userDTOs = users.Select(user => new UserDTO()
                {

                    Dni = user.Dni,
                    Username = user.Username,
                    Surname = user.Surname,
                    Password = "********",
                    FriendCode = user.FriendCode,
                    Email = user.Email,
                    Role = user.Role,
                    Routines = user.Routines?.Select(r => new RoutineDTO 
                    {
                        RoutineId = r.RoutineId,
                        UserId = r.UserId,
                        RoutineName = r.RoutineName,
                        RoutineDescription = r.RoutineDescription,
                        SplitDays = r.SplitDays?.Select(sd => new SplitDayDTO
                        {
                            DayName = sd.DayName,
                            Exercises = sd.Exercises?.Select(e => new ExerciseDTO
                            {
                                ExerciseId = e.ExerciseId,
                                ExerciseName = e.ExerciseName,
                                Sets = e.Sets,
                                Reps = e.Reps,
                                Weight = e.Weight
                            }).ToList() ?? new List<ExerciseDTO>()
                        }).ToList() ?? new List<SplitDayDTO>()
                    }).ToList() ?? new List<RoutineDTO>()
                }).ToList();

                response.IsSuccess = true;
                response.Message = "Consulta correcta";
                response.UsersDTO = userDTOs;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequst updateUserRequest)
        {
            UpdateUserResponse response = new UpdateUserResponse();
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Dni == updateUserRequest.DniToBeFound);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el usuario con ese dni";
                    return response;
                }

                user.Username = updateUserRequest.Username;
                user.Surname = updateUserRequest.Surname;
                user.Password = PasswordUtils.PasswordEncoder(updateUserRequest.Password);
                user.Email = updateUserRequest.Email;

                await _context.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Usuario actualizado correctamente";
                response.UserId = user.UserId;
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
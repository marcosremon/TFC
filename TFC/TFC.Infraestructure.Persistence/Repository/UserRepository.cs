using TFC.Application.DTO.EntityDTO;
using TFC.Application.Interface.Persistence;
using TFC.DDBB.DatabaseConnection;
using TFC.Domain.Model.Entity;
using TFC.Transversal.Mail;
using TFC.Transversal.Security;

namespace TFC.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNewPassword(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return false;
                }

                User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
                if (user == null)
                {
                    return false;
                }

                string newPassword = PasswordUtils.CreatePassword(8);
                user.Password = PasswordUtils.PasswordEncoder(newPassword);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                Mails.SendEmail(user.Username, email, PasswordUtils.PasswordDecoder(user.Password));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la nueva contraseña: " + ex.Message);
            }
        }

        public async Task<UserDTO> CreateUser(UserDTO userDTO)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.UserEmail == user.UserEmail))
                {
                    throw new Exception("Ya existe un usuario con ese email");
                }

                User user = new User()
                {
                    Dni = userDTO.Dni,
                    Username = userDTO.Username,
                    Surname = userDTO.Surname,
                    Password = PasswordUtils.PasswordEncoder(userDTO.Password),
                    Email = userDTO.Email,
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                UserDTO createdUserDTO = new UserDTO()
                {
                    Dni = user.Dni,
                    Username = user.Username,
                    Surname = user.Surname,
                    Password = "********",
                    Email = user.Email,

                    //To_do: añadir las listas a la clase userDTO
                };

                return createdUserDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario: " + ex.Message);
            }
        }

        public async Task<bool> DeleteUser(long userId)
        {
            try
            {
                User? user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return false;
                }

                _context.Users.Remove(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario: " + ex.Message);
            }
        }

        public async Task<UserDTO?> GetUserByEmail(string email)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
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

                    //To_do: añadir las listas a la clase userDTO
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
                List<User> users = await _context.Users.ToListAsync();
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
                    //To_do: añadir las listas a la clase userDTO
                }).ToList();

                return userDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }
        }

        public async Task<UserDTO?> UpdateUser(UserDTO userDTO)
        {
            try
            {
                User? user = await _context.Users.FindAsync(userDTO.UserId);
                if (user == null)
                {
                    return null;
                }

                user.Dni = userDTO.Dni;
                user.Username = userDTO.Username;
                user.Surname = userDTO.Surname;
                user.Password = PasswordUtils.PasswordEncoder(userDTO.Password);
                user.Email = userDTO.Email;
                //To_do: añadir las listas a la clase userDTO

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                UserDTO updatedUserDTO = new UserDTO()
                {
                    Dni = user.Dni,
                    Username = user.Username,
                    Surname = user.Surname,
                    Password = "********",
                    Email = user.Email,
                    //To_do: añadir las listas a la clase userDTO
                };

                return updatedUserDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message);
            }
        }
    }
}
using TFC.Application.DTO.CreateNewPassword;
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.GetUsers;
using TFC.Application.DTO.User.UpdateUser;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;

namespace TFC.Application.Main
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;

        public UserApplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateNewPasswordResponse> CreateNewPassword(CreateNewPasswordRequest createNewPasswordRequest)
        {
            CreateNewPasswordResponse createNewPasswordResponse = new CreateNewPasswordResponse();

            try
            {
                // Pongo esas dos comprobaciones aunque solo hay un atributo 'email'
                // por si en un futuro se añaden mas parametros para comprobar
                if (createNewPasswordRequest == null || string.IsNullOrEmpty(createNewPasswordRequest.UserEmail))
                {
                    createNewPasswordResponse.IsSuccess = false;
                    createNewPasswordResponse.Message = "La llamada es nula o el email introducido es falso";
                    return createNewPasswordResponse;
                }

                bool newPassword = await _userRepository.CreateNewPassword(createNewPasswordRequest.UserEmail);

                if (!newPassword)
                {
                    createNewPasswordResponse.IsSuccess = false;
                    createNewPasswordResponse.Message = "la nueva contraseña es nula";
                    return createNewPasswordResponse;
                }

                createNewPasswordResponse.UserEmail = createNewPasswordRequest.UserEmail;
                createNewPasswordResponse.IsSuccess = true;
                createNewPasswordResponse.Message = "contraseña guardada correctamente";
                return createNewPasswordResponse;
            }
            catch (Exception ex)
            {
                createNewPasswordResponse.IsSuccess = false;
                createNewPasswordResponse.Message = ex.Message;
            }

            return createNewPasswordResponse;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequst createUserRequst)
        {
            CreateUserResponse createUserResponse = new CreateUserResponse();

            try
            {
                UserDTO userDTO = new UserDTO()
                {
                    Dni = createUserRequst.Dni,
                    Username = createUserRequst.Username,
                    Surname = createUserRequst.Surname,
                    Email = createUserRequst.Email,
                    Password = createUserRequst.Password
                };

                if (userDTO == null)
                {
                    createUserResponse.IsSuccess = false;
                    createUserResponse.Message = "El usuario es nulo";
                    return createUserResponse;
                }

                if (string.IsNullOrEmpty(userDTO.Username) || string.IsNullOrEmpty(userDTO.Password))
                {
                    createUserResponse.IsSuccess = false;
                    createUserResponse.Message = "El nombre de usuario y la contraseña son obligatorios";
                    return createUserResponse;
                }

                if (string.IsNullOrEmpty(userDTO.Dni) || string.IsNullOrEmpty(userDTO.Surname))
                {
                    createUserResponse.IsSuccess = false;
                    createUserResponse.Message = "El DNI y el apellido son obligatorios";
                    return createUserResponse;
                }

                if (string.IsNullOrEmpty(userDTO.Email))
                {
                    createUserResponse.IsSuccess = false;
                    createUserResponse.Message = "El email es obligatorio";
                    return createUserResponse;
                }

                UserDTO? createdUser = await _userRepository.CreateUser(userDTO);

                createUserResponse.UserName = createdUser.Username;
                createUserResponse.Email = createdUser.Email;
                createUserResponse.IsSuccess = true;
                createUserResponse.Message = "Usuario creado correctamente";
            }
            catch (Exception ex)
            {
                createUserResponse.IsSuccess = false;
                createUserResponse.Message = ex.Message;
            }

            return createUserResponse;
        }

        public async Task<DeleteUserResponse> DeleteUser(long userId)
        {
            DeleteUserResponse deleteUserResponse = new DeleteUserResponse();

            try
            {
                bool isDeleted = await _userRepository.DeleteUser(userId);
                if (isDeleted)
                {
                    deleteUserResponse.IsSuccess = true;
                    deleteUserResponse.Message = "Usuario eliminado correctamente";
                    return deleteUserResponse;
                }

                deleteUserResponse.IsSuccess = false;
                deleteUserResponse.Message = "No se pudo eliminar el usuario";
            }
            catch (Exception ex)
            {
                deleteUserResponse.IsSuccess = false;
                deleteUserResponse.Message = ex.Message;
            }

            return deleteUserResponse;
        }

        public async Task<GetUserByEmailResponse> GetUserByEmail(string email)
        {
            GetUserByEmailResponse getUserByEmail = new GetUserByEmailResponse();

            try
            {
                UserDTO? user = await _userRepository.GetUserByEmail(email);
                if (user == null) 
                {
                    getUserByEmail.IsSuccess = false;
                    getUserByEmail.Message = "No se encontraron usuarios";
                    return getUserByEmail;
                }

                getUserByEmail.User = user;
                getUserByEmail.IsSuccess = true;
                getUserByEmail.Message = "query correcta";
            }
            catch (Exception ex)
            {
                getUserByEmail.IsSuccess = false;
                getUserByEmail.Message = ex.Message;
            }
            return getUserByEmail;
        }

        public async Task<GetUsersResponse> GetUsers()
        {
            GetUsersResponse getUsers = new GetUsersResponse();

            try
            {
                List<UserDTO> users = await _userRepository.GetUsers();
                if (users == null || users.Count == 0)
                {
                    getUsers.IsSuccess = false;
                    getUsers.Message = "No se encontraron usuarios";
                    return getUsers;
                }

                getUsers.IsSuccess = true;
                getUsers.Message = "query correcta";
                getUsers.Users = users;
            }
            catch (Exception ex)
            {
                getUsers.IsSuccess = false;
                getUsers.Message = ex.Message;
            }
            return getUsers;
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequst updateUserRequest)
        {
            UpdateUserResponse updateUserResponse = new UpdateUserResponse();

            try
            {
                UserDTO userDTO = new UserDTO()
                {
                    Dni = updateUserRequest.DniToBeFound,
                    Username = updateUserRequest.Username,
                    Surname = updateUserRequest.Surname,
                    Email = updateUserRequest.Email,
                    Password = updateUserRequest.Password
                };

                if (userDTO == null)
                {
                    updateUserResponse.IsSuccess = false;
                    updateUserResponse.Message = "El usuario es nulo";
                    return updateUserResponse;
                }

                if (string.IsNullOrEmpty(userDTO.Username) || string.IsNullOrEmpty(userDTO.Password))
                {
                    updateUserResponse.IsSuccess = false;
                    updateUserResponse.Message = "El nombre de usuario y la contraseña son obligatorios";
                    return updateUserResponse;
                }

                if (string.IsNullOrEmpty(userDTO.Dni) || string.IsNullOrEmpty(userDTO.Surname))
                {
                    updateUserResponse.IsSuccess = false;
                    updateUserResponse.Message = "El DNI y el apellido son obligatorios";
                    return updateUserResponse;
                }

                if (string.IsNullOrEmpty(userDTO.Email))
                {
                    updateUserResponse.IsSuccess = false;
                    updateUserResponse.Message = "El email es obligatorio";
                    return updateUserResponse;
                }

                UserDTO? updatedUser = await _userRepository.UpdateUser(userDTO);

                updateUserResponse.UserName = updatedUser.Username;
                updateUserResponse.Email = updatedUser.Email;
                updateUserResponse.IsSuccess = true;
                updateUserResponse.Message = "Usuario actualizado correctamente";
            }
            catch (Exception ex)
            {
                updateUserResponse.IsSuccess = false;
                updateUserResponse.Message = ex.Message;
            }

            return updateUserResponse;
        }
    }
}
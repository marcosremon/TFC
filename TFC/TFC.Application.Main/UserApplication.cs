using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.User.CreateNewPassword;
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
                bool newPassword = await _userRepository.CreateNewPassword(createNewPasswordRequest);
                if (!newPassword)
                {
                    createNewPasswordResponse.IsSuccess = false;
                    createNewPasswordResponse.Message = "la nueva contraseña es nula";
                    return createNewPasswordResponse;
                }

                createNewPasswordResponse.UserEmail = createNewPasswordRequest.UserEmail;
                createNewPasswordResponse.IsSuccess = true;
                createNewPasswordResponse.Message = "contraseña guardada correctamente";
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
                if (string.IsNullOrEmpty(createUserRequst.Username) || string.IsNullOrEmpty(createUserRequst.Password))
                {
                    createUserResponse.IsSuccess = false;
                    createUserResponse.Message = "El nombre de usuario y la contraseña son obligatorios";
                    return createUserResponse;
                }

                if (string.IsNullOrEmpty(createUserRequst.Dni) || string.IsNullOrEmpty(createUserRequst.Surname))
                {
                    createUserResponse.IsSuccess = false;
                    createUserResponse.Message = "El DNI y el apellido son obligatorios";
                    return createUserResponse;
                }

                if (string.IsNullOrEmpty(createUserRequst.Email))
                {
                    createUserResponse.IsSuccess = false;
                    createUserResponse.Message = "El email es obligatorio";
                    return createUserResponse;
                }

                UserDTO? createdUser = await _userRepository.CreateUser(createUserRequst);

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

        public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest)
        {
            DeleteUserResponse deleteUserResponse = new DeleteUserResponse();

            try
            {
                bool isDeleted = await _userRepository.DeleteUser(deleteUserRequest);
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

        public async Task<GetUserByEmailResponse> GetUserByEmail(GetUserByEmailRequest getUserByEmailRequest)
        {
            GetUserByEmailResponse getUserByEmail = new GetUserByEmailResponse();

            try
            {
                UserDTO? user = await _userRepository.GetUserByEmail(getUserByEmailRequest);
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
                List<UserDTO>? users = await _userRepository.GetUsers();
                if (users == null)
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
                if (string.IsNullOrEmpty(updateUserRequest.Username) || string.IsNullOrEmpty(updateUserRequest.Password))
                {
                    updateUserResponse.IsSuccess = false;
                    updateUserResponse.Message = "El nombre de usuario y la contraseña son obligatorios";
                    return updateUserResponse;
                }

                if (string.IsNullOrEmpty(updateUserRequest.Surname))
                {
                    updateUserResponse.IsSuccess = false;
                    updateUserResponse.Message = "el apellido son obligatorios";
                    return updateUserResponse;
                }

                UserDTO? updatedUser = await _userRepository.UpdateUser(updateUserRequest);
                if (updatedUser == null)
                {
                    updateUserResponse.IsSuccess = false;
                    updateUserResponse.Message = "No se pudo actualizar el usuario";
                    return updateUserResponse;
                }

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
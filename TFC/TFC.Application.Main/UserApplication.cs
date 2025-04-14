using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
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

        public async Task<ChangePasswordWithPasswordAndEmailResponse> ChangePasswordWithPasswordAndEmail(ChangePasswordWithPasswordAndEmailRequest changePasswordWithPasswordAndEmailRequest)
        {
            ChangePasswordWithPasswordAndEmailResponse changePasswordWithPasswordAndEmailResponse = new ChangePasswordWithPasswordAndEmailResponse();

            try
            {
                if (changePasswordWithPasswordAndEmailRequest.NewPassword != changePasswordWithPasswordAndEmailRequest.ConfirmNewPassword)
                {
                    changePasswordWithPasswordAndEmailResponse.IsSuccess = false;
                    changePasswordWithPasswordAndEmailResponse.Message = "las contraseñas no coinciden";
                    return changePasswordWithPasswordAndEmailResponse;
                }

                bool newPassword = await _userRepository.ChangePasswordWithPasswordAndEmail(changePasswordWithPasswordAndEmailRequest);
                if (!newPassword)
                {
                    changePasswordWithPasswordAndEmailResponse.IsSuccess = false;
                    changePasswordWithPasswordAndEmailResponse.Message = "la nueva contraseña es nula";
                    return changePasswordWithPasswordAndEmailResponse;
                }

                changePasswordWithPasswordAndEmailResponse.UserEmail = changePasswordWithPasswordAndEmailRequest.UserEmail;
                changePasswordWithPasswordAndEmailResponse.IsSuccess = true;
                changePasswordWithPasswordAndEmailResponse.Message = "contraseña guardada correctamente";
            }
            catch (Exception ex)
            {
                changePasswordWithPasswordAndEmailResponse.IsSuccess = false;
                changePasswordWithPasswordAndEmailResponse.Message = ex.Message;
            }

            return changePasswordWithPasswordAndEmailResponse;
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
            return await _userRepository.CreateUser(createUserRequst);
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
            return await _userRepository.GetUserByEmail(getUserByEmailRequest);
        }

        public async Task<GetUsersResponse> GetUsers()
        {
            return await _userRepository.GetUsers();
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
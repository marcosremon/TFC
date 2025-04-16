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
            if (changePasswordWithPasswordAndEmailRequest == null
                || string.IsNullOrEmpty(changePasswordWithPasswordAndEmailRequest.UserEmail)
                || string.IsNullOrEmpty(changePasswordWithPasswordAndEmailRequest.NewPassword)
                || string.IsNullOrEmpty(changePasswordWithPasswordAndEmailRequest.OldPassword)
                || string.IsNullOrEmpty(changePasswordWithPasswordAndEmailRequest.ConfirmNewPassword)
                || changePasswordWithPasswordAndEmailRequest.NewPassword != changePasswordWithPasswordAndEmailRequest.ConfirmNewPassword)
            {
                return null;
            }

            return await _userRepository.ChangePasswordWithPasswordAndEmail(changePasswordWithPasswordAndEmailRequest);
        }

        public async Task<CreateNewPasswordResponse> CreateNewPassword(CreateNewPasswordRequest createNewPasswordRequest)
        {
            if (createNewPasswordRequest == null
                || string.IsNullOrEmpty(createNewPasswordRequest.UserEmail))
            {
                return null;
            }

            return await _userRepository.CreateNewPassword(createNewPasswordRequest);
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequst createUserRequst)
        {
            if (createUserRequst == null
                || string.IsNullOrEmpty(createUserRequst.Email)
                || string.IsNullOrEmpty(createUserRequst.Dni)
                || string.IsNullOrEmpty(createUserRequst.Username)
                || string.IsNullOrEmpty(createUserRequst.Password))
            {
                return null;
            }

            return await _userRepository.CreateUser(createUserRequst);
        }

        public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest)
        {
            if (deleteUserRequest == null
                || string.IsNullOrEmpty(deleteUserRequest.Dni))
            {
                return null;
            }

            return await _userRepository.DeleteUser(deleteUserRequest);
        }

        public async Task<GetUserByEmailResponse?> GetUserByEmail(GetUserByEmailRequest getUserByEmailRequest)
        {
            if (getUserByEmailRequest == null || string.IsNullOrEmpty(getUserByEmailRequest.Email))
            {
                return null;
            }

            return await _userRepository.GetUserByEmail(getUserByEmailRequest);
        }

        public async Task<GetUsersResponse> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequst updateUserRequest)
        {
            if (updateUserRequest == null 
                || string.IsNullOrEmpty(updateUserRequest.DniToBeFound) 
                || string.IsNullOrEmpty(updateUserRequest.Email))
            {
                return null;
            }

            return await _userRepository.UpdateUser(updateUserRequest);
        }
    }
}
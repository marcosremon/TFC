using TFC.Application.DTO.User.AddNewUserFriend;
using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
using TFC.Application.DTO.User.CreateGenericUser;
using TFC.Application.DTO.User.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetAllUserFriens;
using TFC.Application.DTO.User.GetFriendByFriendCode;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.GetUsers;
using TFC.Application.DTO.User.UpdateUser;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;
using TFC.Transversal.Logs;

namespace TFC.Application.Main
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;

        public UserApplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AddNewUserFriendResponse> AddNewUserFriend(AddNewUserFriendRequest addNewUserFriendRequest)
        {
            if (addNewUserFriendRequest == null 
                || string.IsNullOrEmpty(addNewUserFriendRequest.UserEmail) 
                || string.IsNullOrEmpty(addNewUserFriendRequest.FriendCode))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new AddNewUserFriendResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: AddNewUserFriendRequest is null or required fields are missing."
                };
            } 

            return await _userRepository.AddNewUserFriend(addNewUserFriendRequest);
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
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new ChangePasswordWithPasswordAndEmailResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: Missing or mismatched fields in ChangePasswordWithPasswordAndEmailRequest."
                };
            }

            return await _userRepository.ChangePasswordWithPasswordAndEmail(changePasswordWithPasswordAndEmailRequest);
        }

        public async Task<CreateNewPasswordResponse> CreateNewPassword(CreateNewPasswordRequest createNewPasswordRequest)
        {
            if (createNewPasswordRequest == null || string.IsNullOrEmpty(createNewPasswordRequest.UserEmail))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new CreateNewPasswordResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: CreateNewPasswordRequest is null or UserEmail is missing."
                };
            }

            return await _userRepository.CreateNewPassword(createNewPasswordRequest);
        }

        public async Task<CreateUserResponse> CreateUser(CreateGenericUserRequest createGenericUserRequest)
        {
            if (createGenericUserRequest == null
                || string.IsNullOrEmpty(createGenericUserRequest.Email)
                || string.IsNullOrEmpty(createGenericUserRequest.Dni)
                || string.IsNullOrEmpty(createGenericUserRequest.Username)
                || string.IsNullOrEmpty(createGenericUserRequest.Password)
                || string.IsNullOrEmpty(createGenericUserRequest.ConfirmPassword))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new CreateUserResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: Missing fields in CreateUserRequst."
                };
            }

            return await _userRepository.CreateUser(createGenericUserRequest);
        }

        public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest)
        {
            if (deleteUserRequest == null || string.IsNullOrEmpty(deleteUserRequest.Dni))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new DeleteUserResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: DeleteUserRequest is null or Dni is missing."
                };
            }

            return await _userRepository.DeleteUser(deleteUserRequest);
        }

        public async Task<GetAllUserFriendsResponse> GetAllUserFriends(GetAllUserFriendsRequest getAllUserFriendsRequest)
        {
            if (getAllUserFriendsRequest == null || string.IsNullOrEmpty(getAllUserFriendsRequest.UserEmail))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new GetAllUserFriendsResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: GetAllUserFriendsRequest is null or UserEmail is missing."
                };
            }

            return await _userRepository.GetAllUserFriends(getAllUserFriendsRequest);
        }

        public async Task<GetFriendByFriendCodeResponse> GetFriendByFriendCode(GetFriendByFriendCodeRequest getFriendByFriendCodeRequest)
        {
            if (getFriendByFriendCodeRequest == null 
                || string.IsNullOrEmpty(getFriendByFriendCodeRequest.FriendCode))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new GetFriendByFriendCodeResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: friendCode is null or empty."
                };
            }

            return await _userRepository.GetFriendByFriendCode(getFriendByFriendCodeRequest);
        }

        public async Task<GetUserByEmailResponse> GetUserByEmail(GetUserByEmailRequest getUserByEmailRequest)
        {
            if (getUserByEmailRequest == null || string.IsNullOrEmpty(getUserByEmailRequest.Email))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new GetUserByEmailResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: GetUserByEmailRequest is null or Email is missing."
                };
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
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new UpdateUserResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: Missing fields in UpdateUserRequst."
                };
            }

            return await _userRepository.UpdateUser(updateUserRequest);
        }
    }
}
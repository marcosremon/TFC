using TFC.Application.DTO.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.GetUsers;
using TFC.Application.DTO.User.UpdateUser;

namespace TFC.Application.Interface.Application
{
    public interface IUserApplication
    {
        public Task<GetUserByEmailResponse> GetUserByEmail(string email);
        public Task<GetUsersResponse> GetUsers();
        public Task<CreateUserResponse> CreateUser(CreateUserRequst createUserRequst);
        public Task<UpdateUserResponse> UpdateUser(UpdateUserRequst updateUserRequest);
        public Task<DeleteUserResponse> DeleteUser(long userId);
        public Task<CreateNewPasswordResponse> CreateNewPassword(CreateNewPasswordRequest createNewPasswordRequest);
    }
}
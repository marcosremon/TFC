using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
using TFC.Application.DTO.User.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.GetUsers;
using TFC.Application.DTO.User.UpdateUser;

namespace TFC.Application.Interface.Persistence
{
    public interface IUserRepository
    {
        public Task<GetUserByEmailResponse> GetUserByEmail(GetUserByEmailRequest getUserByEmailRequest);
        public Task<GetUsersResponse> GetUsers();
        public Task<CreateUserResponse> CreateUser(CreateUserRequst createUserRequst);
        public Task<UpdateUserResponse> UpdateUser(UpdateUserRequst updateUserRequest);
        public Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest);
        public Task<CreateNewPasswordResponse> CreateNewPassword(CreateNewPasswordRequest createNewPasswordRequest);
        public Task<ChangePasswordWithPasswordAndEmailResponse> ChangePasswordWithPasswordAndEmail(ChangePasswordWithPasswordAndEmailRequest changePasswordWithPasswordAndEmailRequest);
    }
}
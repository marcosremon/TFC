using TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail;
using TFC.Application.DTO.User.CreateGenericUser;
using TFC.Application.DTO.User.CreateNewPassword;
using TFC.Application.DTO.User.CreateUser;
using TFC.Application.DTO.User.DeleteUser;
using TFC.Application.DTO.User.GetUserByEmail;
using TFC.Application.DTO.User.GetUsers;
using TFC.Application.DTO.User.UpdateUser;

namespace TFC.Application.Interface.Application
{
    public interface IUserApplication
    {
        Task<GetUserByEmailResponse> GetUserByEmail(GetUserByEmailRequest getUserByEmailRequest);
        Task<GetUsersResponse> GetUsers();
        Task<CreateUserResponse> CreateUser(CreateGenericUserRequest createGenericUserRequest);
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequst updateUserRequest);
        Task<DeleteUserResponse> DeleteUser(DeleteUserRequest deleteUserRequest);
        Task<CreateNewPasswordResponse> CreateNewPassword(CreateNewPasswordRequest createNewPasswordRequest);
        Task<ChangePasswordWithPasswordAndEmailResponse> ChangePasswordWithPasswordAndEmail(ChangePasswordWithPasswordAndEmailRequest changePasswordWithPasswordAndEmailRequest);
    }
}
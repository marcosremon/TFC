using Kintech.WebServices.Applicacion.DTO.CreateLicense;
using TFC.Application.DTO.Request;
using TFC.Application.DTO.Response;

namespace TFC.Application.Interface
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
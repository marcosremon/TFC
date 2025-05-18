using TFC.Application.DTO.Auth.Login;
namespace TFC.Application.Interface.Application
{
    public interface IAuthApplication
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
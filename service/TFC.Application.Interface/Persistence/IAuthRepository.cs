using TFC.Application.DTO.Auth.Login;

namespace TFC.Application.Interface.Persistence
{
    public interface IAuthRepository
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
using TFC.Application.DTO.Auth.Login;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;
using TFC.Transversal.Logs;

namespace TFC.Application.Main
{
    public class AuthApplication : IAuthApplication
    {
        public readonly IAuthRepository _authRepository;

        public AuthApplication(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null
                || string.IsNullOrEmpty(loginRequest.UserEmail))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new LoginResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: LoginRequest is null or required fields are missing.",
                };
            }

            return await _authRepository.Login(loginRequest);
        }
    }
}
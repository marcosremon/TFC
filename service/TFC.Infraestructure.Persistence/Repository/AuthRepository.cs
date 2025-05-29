using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFC.Application.DTO.Auth.Login;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;
using TFC.Infraestructure.Persistence.Context;
using TFC.Service.WebApi;
using TFC.Transversal.Logs;
using TFC.Transversal.Security;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u =>
                    u.Email == loginRequest.UserEmail &&
                    u.Password == PasswordUtils.PasswordEncoder(loginRequest.UserPassword));
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario no encontrado revise el correo y la contraseña";
                    Log.Instance.Trace($"Login failed: {response.Message}");
                    return response;
                }

                response.IsAdmin = user.Role == Domain.Model.Enum.Role.Admin;
                response.IsSuccess = true;
                response.Message = "Login successful.";
                Log.Instance.Trace($"Login successful");
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
                Log.Instance.Error($"Login --> Error: {ex.Message}");
            }

            return response;
        }
    }
}

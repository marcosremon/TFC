using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Auth.CheckTokenStatus;
using TFC.Application.DTO.Auth.Login;
using TFC.Application.Interface.Application;
using TFC.Transversal.Logs;

namespace TFC.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;

        public AuthController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                LoginResponse response = await _authApplication.Login(loginRequest);
                if (response.IsSuccess)
                {
                    response.BearerToken = response.IsAdmin
                        ? JwtUtils.GenerateAdminJwtToken(loginRequest.UserEmail)
                        : JwtUtils.GenerateUserJwtToken(loginRequest.UserEmail);
                    
                    Log.Instance.Trace($"Login successful del usuario con email: {loginRequest.UserEmail}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al iniciar sesion: {response.Message}");
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Login --> Error al iniciar sesion: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("check-token-status")]
        public async Task<ActionResult> CheckTokenStatus([FromBody] CheckTokenStatusRequest checkTokenStatusRequest)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkTokenStatusRequest.Token) && JwtUtils.IsValidToken(checkTokenStatusRequest.Token))
                {
                    return Ok(new { isValid = true });
                }

                return Ok(new { isValid = false });
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"CheckLoginStatus --> Error: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
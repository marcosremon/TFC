namespace TFC.Application.DTO.Auth.Login
{
    public class LoginRequest
    {
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;    
    }
}

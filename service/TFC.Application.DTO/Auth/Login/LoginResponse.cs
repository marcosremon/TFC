using Kintech.RestCA.Transversal.Common;

namespace TFC.Application.DTO.Auth.Login
{
    public class LoginResponse : BaseResponse
    {
        public string BearerToken { get; set; } = string.Empty;
    }
}
using Kintech.RestCA.Transversal.Common;

namespace TFC.Application.DTO.Auth.LoginWeb
{
    public class LoginWebResponse : BaseResponse
    {
        public string BearerToken { get; set; } = string.Empty;
    }
}
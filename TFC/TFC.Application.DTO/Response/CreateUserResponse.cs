using Kintech.RestCA.Transversal.Common;

namespace TFC.Application.DTO.Response
{
    public class CreateUserResponse : BaseResponse 
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
    }
}

using Kintech.RestCA.Transversal.Common;

namespace TFC.Application.DTO.Response
{
    public class CreateUserResponse : BaseResponse 
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}

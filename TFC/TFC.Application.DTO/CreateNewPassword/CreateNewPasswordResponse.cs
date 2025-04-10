using Kintech.RestCA.Transversal.Common;

namespace TFC.Application.DTO.CreateNewPassword
{
    public class CreateNewPasswordResponse : BaseResponse
    {
        public string? UserEmail { get; set; }
    }
}
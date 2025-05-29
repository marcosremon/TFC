using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.User.CreateGoogleUser
{
    public class CreateGoogleUserResponse : BaseResponse
    {
        public UserDTO? UserDTO { get; set; }
    }
}
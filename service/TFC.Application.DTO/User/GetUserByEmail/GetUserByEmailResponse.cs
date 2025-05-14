using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.User.GetUserByEmail
{
    public class GetUserByEmailResponse : BaseResponse
    {
        public UserDTO? UserDTO { get; set; }
    }
}
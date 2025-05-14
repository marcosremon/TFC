using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.User.GetUsers
{
    public class GetUsersResponse : BaseResponse
    {
        public List<UserDTO>? UsersDTO { get; set; }
    }
}
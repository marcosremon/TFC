using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.User.GetAllUserFriens
{
    public class GetAllUserFriendsResponse : BaseResponse
    {
        public List<UserDTO> Friends { get; set; } = new List<UserDTO>();
    }
}

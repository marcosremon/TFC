using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Friend.GetAllUserFriens
{
    public class GetAllUserFriendsResponse : BaseResponse
    {
        public List<UserDTO> Friends { get; set; } = new List<UserDTO>();
    }
}
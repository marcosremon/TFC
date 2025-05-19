using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.User.GetFriendByFriendCode
{
    public class GetFriendByFriendCodeResponse : BaseResponse
    {
        public UserDTO? UserDTO{ get; set; }
    }
}

using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.User.GetUserByEmail
{
    public class GetUserByEmailResponse : BaseResponse
    {
        public UserDTO? UserDTO { get; set; }
        public int routinesCount { get; set; }
        public int friendsCount { get; set; }
    }
}
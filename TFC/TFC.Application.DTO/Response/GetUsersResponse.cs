using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.Response
{
    public class GetUsersResponse : BaseResponse
    {
        public List<UserDTO>? Users { get; set; }
    }
}

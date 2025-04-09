using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.Response
{
    public class GetUserByEmailResponse : BaseResponse
    {
        public UserDTO? User { get; set; }
    }
}
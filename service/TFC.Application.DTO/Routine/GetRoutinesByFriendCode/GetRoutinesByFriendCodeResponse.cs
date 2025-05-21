using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Routine.GetRoutinesByFriendCode
{
    public class GetRoutinesByFriendCodeResponse : BaseResponse
    {
        public List<RoutineDTO> FriendRoutines { get; set; } = new List<RoutineDTO>();
    }
}
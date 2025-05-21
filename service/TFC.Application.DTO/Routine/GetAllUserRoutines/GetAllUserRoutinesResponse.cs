using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Routine.GetAllUserRoutines
{
    public class GetAllUserRoutinesResponse : BaseResponse
    {
        public List<RoutineDTO> Routines { get; set; } = new List<RoutineDTO>();
    }
}

using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Routine.GetRoutines
{
    public class GetRoutinesResponse : BaseResponse
    {
        public List<RoutineDTO>? routines { get; set; }
    }
}
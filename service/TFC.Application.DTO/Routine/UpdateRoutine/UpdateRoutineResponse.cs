using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.Routine.GetRoutines
{
    public class UpdateRoutineResponse : BaseResponse
    {
        public RoutineDTO? RoutineDTO { get; set; }
    }
}
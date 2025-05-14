using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.Routine.CreateRoutine
{
    public class CreateRoutineResponse : BaseResponse
    {
        public RoutineDTO? RoutineDTO { get; set; }
    }
}
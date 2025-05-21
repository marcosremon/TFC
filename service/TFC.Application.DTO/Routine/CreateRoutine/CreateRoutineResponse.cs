using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Routine.CreateRoutine
{
    public class CreateRoutineResponse : BaseResponse
    {
        public RoutineDTO? RoutineDTO { get; set; }
    }
}
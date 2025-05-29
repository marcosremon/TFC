using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Routine.GetRoutineById
{
    public class GetRoutineByIdResponse : BaseResponse
    {
        public RoutineDTO? RoutineDTO { get; set; }
    }
}
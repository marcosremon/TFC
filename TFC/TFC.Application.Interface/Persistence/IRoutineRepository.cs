using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.GetRoutines;

namespace TFC.Application.Interface.Persistence
{
    public interface IRoutineRepository
    {
        public Task<CreateRoutineResponse> CreateRoutine(CreateRoutineRequest createRoutineRequest);
        public Task<UpdateRoutineResponse> UpdateRoutine(UpdateRoutineRequest updateRoutineRequest);
    }
}
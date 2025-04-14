using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetRoutines;

namespace TFC.Application.Interface.Persistence
{
    public interface IRoutineRepository
    {
        public Task<CreateRoutineResponse> CreateRoutine(CreateRoutineRequest createRoutineRequest);
        public Task<DeleteRoutineResponse> DeleteRoutine(DeleteRoutineRequest deleteRoutineRequest);
        public Task<UpdateRoutineResponse> UpdateRoutine(UpdateRoutineRequest updateRoutineRequest);
    }
}
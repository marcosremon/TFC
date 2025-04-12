using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Routine.CreateRoutine;

namespace TFC.Application.Interface.Persistence
{
    public interface IRoutineRepository
    {
        public Task<RoutineDTO?> CreateRoutine(CreateRoutineRequest createRoutineRequest);
        public Task<RoutineDTO?> UpdateRoutine(UpdateRoutineRequest updateRoutineRequest);
    }
}
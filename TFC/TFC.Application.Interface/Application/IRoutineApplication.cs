using TFC.Application.DTO.Routine.CreateRoutine;

namespace TFC.Application.Interface.Application
{
    public interface IRoutineApplication
    {
        public Task<CreateRoutineResponse> CreateRoutine(CreateRoutineRequest createRoutineRequest);
    }
}
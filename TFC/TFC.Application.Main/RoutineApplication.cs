using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutinesByFriendCode;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;

namespace TFC.Application.Main
{
    public class RoutineApplication : IRoutineApplication
    {
        private readonly IRoutineRepository _routineRepository;

        public RoutineApplication(IRoutineRepository routineRepository)
        {
            _routineRepository = routineRepository;
        }

        public async Task<CreateRoutineResponse> CreateRoutine(CreateRoutineRequest createRoutineRequest)
        {
            return await _routineRepository.CreateRoutine(createRoutineRequest);
        }

        public async Task<DeleteRoutineResponse> DeleteRoutine(DeleteRoutineRequest deleteRoutineRequest)
        {
            return await _routineRepository.DeleteRoutine(deleteRoutineRequest);
        }

        public async Task<GetRoutinesByFriendCodeResponse> GetRoutinesByFriendCode(GetRoutinesByFriendCodeRequest getRoutinesByFriendCodeRequest)
        {
            return await _routineRepository.GetRoutinesByFriendCode(getRoutinesByFriendCodeRequest);
        }

        public async Task<UpdateRoutineResponse> UpdateUser(UpdateRoutineRequest updateRoutineRequest)
        {
            return await _routineRepository.UpdateRoutine(updateRoutineRequest);
        }
    }
}
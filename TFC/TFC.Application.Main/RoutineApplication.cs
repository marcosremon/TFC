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
            if (createRoutineRequest == null || string.IsNullOrEmpty(createRoutineRequest.RoutineName))
            {
                return new CreateRoutineResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: CreateRoutineRequest is null or RoutineName is missing."
                };
            }

            return await _routineRepository.CreateRoutine(createRoutineRequest);
        }

        public async Task<DeleteRoutineResponse> DeleteRoutine(DeleteRoutineRequest deleteRoutineRequest)
        {
            if (deleteRoutineRequest == null
               || string.IsNullOrEmpty(deleteRoutineRequest.UserDni)
               || deleteRoutineRequest.RoutineId == null)
            {
                return new DeleteRoutineResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: DeleteRoutineRequest is null, UserDni is missing, or RoutineId is missing."
                };
            }

            return await _routineRepository.DeleteRoutine(deleteRoutineRequest);
        }

        public async Task<GetRoutinesByFriendCodeResponse> GetRoutinesByFriendCode(GetRoutinesByFriendCodeRequest getRoutinesByFriendCodeRequest)
        {
            if (getRoutinesByFriendCodeRequest == null || string.IsNullOrEmpty(getRoutinesByFriendCodeRequest.FriendCode))
            {
                return new GetRoutinesByFriendCodeResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: GetRoutinesByFriendCodeRequest is null or FriendCode is missing."
                };
            }

            return await _routineRepository.GetRoutinesByFriendCode(getRoutinesByFriendCodeRequest);
        }

        public async Task<UpdateRoutineResponse> UpdateUser(UpdateRoutineRequest updateRoutineRequest)
        {
            if (updateRoutineRequest == null || updateRoutineRequest.RoutineId == 0)
            {
                return new UpdateRoutineResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: UpdateRoutineRequest is null or RoutineId is missing."
                };
            }

            return await _routineRepository.UpdateRoutine(updateRoutineRequest);
        }
    }
}
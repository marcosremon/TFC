using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutinesByFriendCode;

namespace TFC.Application.Interface.Application
{
    public interface IRoutineApplication
    {
        public Task<CreateRoutineResponse> CreateRoutine(CreateRoutineRequest createRoutineRequest);
        public Task<DeleteRoutineResponse> DeleteRoutine(DeleteRoutineRequest deleteRoutineRequest);
        public Task<GetRoutinesByFriendCodeResponse> GetRoutinesByFriendCode(GetRoutinesByFriendCodeRequest getRoutinesByFriendCodeRequest);
        public Task<UpdateRoutineResponse> UpdateUser(UpdateRoutineRequest updateRoutineRequest);
    }
}
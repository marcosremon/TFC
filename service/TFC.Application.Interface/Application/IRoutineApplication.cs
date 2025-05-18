using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutinesByFriendCode;

namespace TFC.Application.Interface.Application
{
    public interface IRoutineApplication
    {
        Task<CreateRoutineResponse> CreateRoutine(CreateRoutineRequest createRoutineRequest);
        Task<DeleteRoutineResponse> DeleteRoutine(DeleteRoutineRequest deleteRoutineRequest);
        Task<GetRoutinesByFriendCodeResponse> GetRoutinesByFriendCode(GetRoutinesByFriendCodeRequest getRoutinesByFriendCodeRequest);
        Task<UpdateRoutineResponse> UpdateUser(UpdateRoutineRequest updateRoutineRequest);
    }
}
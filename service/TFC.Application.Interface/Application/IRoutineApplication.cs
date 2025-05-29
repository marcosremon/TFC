using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetAllUserRoutines;
using TFC.Application.DTO.Routine.GetRoutineById;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutineStats;

namespace TFC.Application.Interface.Application
{
    public interface IRoutineApplication
    {
        Task<CreateRoutineResponse> CreateRoutine(CreateRoutineRequest createRoutineRequest);
        Task<DeleteRoutineResponse> DeleteRoutine(DeleteRoutineRequest deleteRoutineRequest);
        Task<GetAllUserRoutinesResponse> GetAllUserRoutines(GetAllUserRoutinesRequest getAllUserRoutinesRequest);
        Task<GetRoutineByIdResponse> GetRoutineById(GetRoutineByIdRequest getRoutineByIdRequest);
        Task<GetRoutineStatsResponse> GetRoutineStats(GetRoutineStatsRequest getRoutineStatsRequest);
        Task<UpdateRoutineResponse> UpdateUser(UpdateRoutineRequest updateRoutineRequest);
    }
}
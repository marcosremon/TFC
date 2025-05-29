using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetAllUserRoutines;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutineStats;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;
using TFC.Transversal.Logs;

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
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
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
               || string.IsNullOrEmpty(deleteRoutineRequest.UserEmail)
               || deleteRoutineRequest.RoutineId == null)
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new DeleteRoutineResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: DeleteRoutineRequest is null, UserDni is missing, or RoutineId is missing."
                };
            }

            return await _routineRepository.DeleteRoutine(deleteRoutineRequest);
        }

        public async Task<GetAllUserRoutinesResponse> GetAllUserRoutines(GetAllUserRoutinesRequest getAllUserRoutinesRequest)
        {
            if (getAllUserRoutinesRequest == null || string.IsNullOrEmpty(getAllUserRoutinesRequest.UserEmail))
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new GetAllUserRoutinesResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: GetAllUserRoutinesRequest is null or UserEmail is missing."
                };
            }

            return await _routineRepository.GetAllUserRoutines(getAllUserRoutinesRequest);
        }

        public async Task<GetRoutineStatsResponse> GetRoutineStats(GetRoutineStatsRequest getRoutineStatsRequest)
        {
            return await _routineRepository.GetRoutineStats(getRoutineStatsRequest);
        }

        public async Task<UpdateRoutineResponse> UpdateUser(UpdateRoutineRequest updateRoutineRequest)
        {
            if (updateRoutineRequest == null || updateRoutineRequest.RoutineId == 0)
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
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
using TFC.Application.DTO.EntityDTO;
using TFC.Application.DTO.Routine.CreateRoutine;
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
            CreateRoutineResponse createRoutineResponse = new CreateRoutineResponse();

            try
            {
                if (createRoutineRequest.SplitDays.Count == 0 || createRoutineRequest.SplitDays == null)
                {
                    createRoutineResponse.IsSuccess = false;
                    createRoutineResponse.Message = "SplitDays cannot be null or empty";
                }

                if (string.IsNullOrEmpty(createRoutineRequest.RoutineDescription))
                {
                    createRoutineResponse.IsSuccess = false;
                    createRoutineResponse.Message = "RoutineDescription cannot be null or empty";
                }

                RoutineDTO? routineDTO = await _routineRepository.CreateRoutine(createRoutineRequest);

                if (routineDTO == null)
                {
                    createRoutineResponse.IsSuccess = false;
                    createRoutineResponse.Message = "Routine could not be created";
                }

                createRoutineResponse.IsSuccess = true;
                createRoutineResponse.Routine = routineDTO;
                createRoutineResponse.Message = "Routine created successfully";
            }
            catch (Exception ex)
            {
                createRoutineResponse.IsSuccess = false;
                createRoutineResponse.Message = ex.Message;
            }
            
            return createRoutineResponse;
        }
    }
}
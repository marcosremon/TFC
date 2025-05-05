using TFC.Application.DTO.Exercise.AddExercise;
using TFC.Application.DTO.Exercise.GetExercisesByDayName;
using TFC.Application.DTO.Exercise.UpdateExercise;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;

namespace TFC.Application.Main
{
    public class ExerciseApplication : IExerciseApplication
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseApplication(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<AddExerciseResponse> AddExercise(AddExerciseRequest addExerciseRequest)
        {
            if (addExerciseRequest == null
                || addExerciseRequest.UserId == null
                || addExerciseRequest.RoutineId == null
                || addExerciseRequest.DayName == null
                || string.IsNullOrEmpty(addExerciseRequest.ExerciseName))
            {
                return new AddExerciseResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: AddExerciseRequest is null or required fields are missing."
                };
            }

            return await _exerciseRepository.AddExercise(addExerciseRequest);
        }

        public async Task<DeleteExerciseResponse> DeleteExercise(DeleteExerciseRequest deleteExerciseRequest)
        {
            if (deleteExerciseRequest == null
                || deleteExerciseRequest.UserId == null
                || deleteExerciseRequest.RoutineId == null
                || deleteExerciseRequest.DayName == null
                || string.IsNullOrEmpty(deleteExerciseRequest.ExerciseName))
            {
                return new DeleteExerciseResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: DeleteExerciseRequest is null or required fields are missing."
                };
            }

            return await _exerciseRepository.DeleteExercise(deleteExerciseRequest);
        }

        public async Task<GetExercisesByDayNameResponse> GetExercisesByDayName(GetExercisesByDayNameRequest getExercisesByDayNameRequest)
        {
            if (getExercisesByDayNameRequest == null
                || getExercisesByDayNameRequest.UserId == null
                || getExercisesByDayNameRequest.RoutineId == null
                || getExercisesByDayNameRequest.DayName == null)
            {
                return new GetExercisesByDayNameResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: GetExercisesByDayNameRequest is null or required fields are missing."
                };
            }

            return await _exerciseRepository.GetExercisesByDayName(getExercisesByDayNameRequest);
        }

        public async Task<UpdateExerciseResponse> UpdateExercise(UpdateExerciseRequest updateExerciseRequest)
        {
            if (updateExerciseRequest == null
                || updateExerciseRequest.UserId == null
                || updateExerciseRequest.RoutineId == null
                || updateExerciseRequest.DayName == null
                || string.IsNullOrEmpty(updateExerciseRequest.ExerciseName))
            {
                return new UpdateExerciseResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: UpdateExerciseRequest is null or required fields are missing."
                };
            }

            return await _exerciseRepository.UpdateExercise(updateExerciseRequest);
        }
    }
}
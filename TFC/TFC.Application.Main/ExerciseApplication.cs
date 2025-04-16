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
            return await _exerciseRepository.AddExercise(addExerciseRequest);
        }

        public async Task<DeleteExerciseResponse> DeleteExercise(DeleteExerciseRequest deleteExerciseRequest)
        {
            return await _exerciseRepository.DeleteExercise(deleteExerciseRequest);
        }

        public async Task<GetExercisesByDayNameResponse> GetExercisesByDayName(GetExercisesByDayNameRequest getExercisesByDayNameRequest)
        {
            return await _exerciseRepository.GetExercisesByDayName(getExercisesByDayNameRequest);
        }

        public async Task<UpdateExerciseResponse> UpdateExercise(UpdateExerciseRequest updateExerciseRequest)
        {
            return await _exerciseRepository.UpdateExercise(updateExerciseRequest);
        }
    }
}
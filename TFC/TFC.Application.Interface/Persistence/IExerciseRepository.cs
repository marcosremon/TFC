using TFC.Application.DTO.Exercise.AddExercise;
using TFC.Application.DTO.Exercise.GetExercisesByDayName;
using TFC.Application.DTO.Exercise.UpdateExercise;

namespace TFC.Application.Interface.Persistence
{
    public interface IExerciseRepository
    {
        public Task<UpdateExerciseResponse> AddExercise(UpdateExerciseRequest addExerciseRequest);
        public Task<DeleteExerciseResponse> DeleteExercise(DeleteExerciseRequest deleteExerciseRequest);
        public Task<GetExercisesByDayNameResponse> GetExercisesByDayName(GetExercisesByDayNameRequest getExercisesByDayNameRequest);
        public Task<UpdateExerciseResponse> UpdateExercise(UpdateExerciseRequest updateExerciseRequest);
    }
}
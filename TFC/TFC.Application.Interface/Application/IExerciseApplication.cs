using TFC.Application.DTO.Exercise.AddExercise;
using TFC.Application.DTO.Exercise.GetExercisesByDayName;
using TFC.Application.DTO.Exercise.UpdateExercise;

namespace TFC.Application.Interface.Application
{
    public interface IExerciseApplication
    {
        public Task<AddExerciseResponse> AddExercise(AddExerciseRequest addExerciseRequest);
        public Task<DeleteExerciseResponse> DeleteExercise(DeleteExerciseRequest deleteExerciseRequest);
        public Task<GetExercisesByDayNameResponse> GetExercisesByDayName(GetExercisesByDayNameRequest getExercisesByDayNameRequest);
        public Task<UpdateExerciseResponse> UpdateExercise(UpdateExerciseRequest updateExerciseRequest);
    }
}
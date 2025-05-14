using TFC.Application.DTO.Exercise.AddExercise;
using TFC.Application.DTO.Exercise.GetExercisesByDayName;
using TFC.Application.DTO.Exercise.UpdateExercise;

namespace TFC.Application.Interface.Application
{
    public interface IExerciseApplication
    {
        Task<AddExerciseResponse> AddExercise(AddExerciseRequest addExerciseRequest);
        Task<DeleteExerciseResponse> DeleteExercise(DeleteExerciseRequest deleteExerciseRequest);
        Task<GetExercisesByDayNameResponse> GetExercisesByDayName(GetExercisesByDayNameRequest getExercisesByDayNameRequest);
        Task<UpdateExerciseResponse> UpdateExercise(UpdateExerciseRequest updateExerciseRequest);
    }
}
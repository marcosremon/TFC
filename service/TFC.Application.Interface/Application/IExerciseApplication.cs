using TFC.Application.DTO.Exercise.DeleteExecise;
using TFC.Application.DTO.Exercise.AddExerciseProgress;
using TFC.Application.DTO.Exercise.GetExercisesByDayAndRoutineId;
using TFC.Application.DTO.Exercise.UpdateExercise;

namespace TFC.Application.Interface.Application
{
    public interface IExerciseApplication
    {
        Task<AddExerciseAddExerciseProgressResponse> AddExerciseProgress(AddExerciseAddExerciseProgressRequest addExerciseRequest);
        Task<DeleteExerciseResponse> DeleteExercise(DeleteExerciseRequest deleteExerciseRequest);
        Task<GetExercisesByDayAndRoutineIdResponse> GetExercisesByDayAndRoutineId(GetExercisesByDayAndRoutineIdRequest getExercisesByDayNameRequest);
        Task<UpdateExerciseResponse> UpdateExercise(UpdateExerciseRequest updateExerciseRequest);
    }
}
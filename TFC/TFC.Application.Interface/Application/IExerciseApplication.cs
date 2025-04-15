using TFC.Application.DTO.Exercise.AddExercise;

namespace TFC.Application.Interface.Application
{
    public interface IExerciseApplication
    {
        public Task<AddExerciseResponse> AddExercise(AddExerciseRequest addExerciseRequest);
    }
}
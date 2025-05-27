using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Exercise.GetExercisesByDayAndRoutineId
{
    public class GetExercisesByDayAndRoutineIdResponse : BaseResponse
    {
        public List<ExerciseDTO> Exercises { get; set; } = new List<ExerciseDTO>();
        public Dictionary<long, List<string>> PastProgress { get; set; } = new();
    }
}
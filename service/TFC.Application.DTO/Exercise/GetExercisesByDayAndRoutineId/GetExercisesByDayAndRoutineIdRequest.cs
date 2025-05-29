using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.Exercise.GetExercisesByDayAndRoutineId
{
    public class GetExercisesByDayAndRoutineIdRequest
    {
        public int? RoutineId { get; set; }
        public string? DayName { get; set; }
    }
}
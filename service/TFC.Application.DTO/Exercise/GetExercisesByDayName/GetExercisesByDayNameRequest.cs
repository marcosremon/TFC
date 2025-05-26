using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.Exercise.GetExercisesByDayName
{
    public class GetExercisesByDayNameRequest
    {
        public int? RoutineId { get; set; }
        public string? DayName { get; set; }
    }
}
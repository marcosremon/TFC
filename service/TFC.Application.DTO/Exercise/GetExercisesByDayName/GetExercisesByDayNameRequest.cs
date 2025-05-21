using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.Exercise.GetExercisesByDayName
{
    public class GetExercisesByDayNameRequest
    {
        public string? UserEmail { get; set; }
        public long? RoutineId { get; set; }
        public WeekDay? DayName { get; set; }
    }
}
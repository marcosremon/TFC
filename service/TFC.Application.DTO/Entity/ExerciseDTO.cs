using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.Entity
{
    public class ExerciseDTO
    {
        public long ExerciseId { get; set; }
        public string? ExerciseName { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }
        public WeekDay DayName { get; set; }
    }
}
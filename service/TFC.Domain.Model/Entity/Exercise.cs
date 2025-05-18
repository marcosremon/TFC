using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class Exercise
    {
        [Key]
        public long ExerciseId { get; set; }

        [MaxLength(100)]
        public string? ExerciseName { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }

        [ForeignKey("RoutineId,DayName")]
        public long RoutineId { get; set; }
        public WeekDay DayName { get; set; }
        public virtual SplitDay? SplitDay { get; set; }
    }
}
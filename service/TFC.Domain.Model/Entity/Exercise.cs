using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class Exercise
    {
        [Key]
        [Column("exercise_id")]
        public long ExerciseId { get; set; }

        [MaxLength(100)]
        [Column("exercise_name")]
        public string? ExerciseName { get; set; }

        [Column("sets")]
        public int? Sets { get; set; }

        [Column("reps")]
        public int? Reps { get; set; }

        [Column("weight")]
        public double? Weight { get; set; }

        [Column("routine_id")]
        public long RoutineId { get; set; }

        [Column("day_name")]
        public WeekDay? DayName { get; set; }

        // Navegaciones
        [ForeignKey("RoutineId,DayName")]
        public virtual SplitDay? SplitDay { get; set; }

        public virtual Routine? Routine { get; set; }
    }
}
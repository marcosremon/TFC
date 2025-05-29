using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class SplitDay
    {
        [Column("day_name")]
        public WeekDay? DayName { get; set; }  // Eliminado el nullable

        [Column("routine_id")]
        public long RoutineId { get; set; }

        [Column("day_exercises_description")]
        public string DayExercisesDescription { get; set; } = string.Empty;

        // Navegaciones
        public virtual Routine? Routine { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class SplitDay
    {
        public WeekDay? DayName { get; set; }
        public string DayExercisesDescription { get; set; } = string.Empty; // Pecho, Espalda, Torso...

        [ForeignKey("RoutineId")]
        public long RoutineId { get; set; }
        public virtual Routine? Routine { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class SplitDay
    {
        [Required]
        public WeekDay DayName { get; set; }

        [ForeignKey("RoutineId")]
        public long RoutineId { get; set; }
        public virtual Routine? Routine { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
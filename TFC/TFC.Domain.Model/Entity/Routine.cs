using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFC.Domain.Model.Entity
{
    public class Routine
    {
        [Key]
        public long RoutineId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? RoutineName { get; set; }

        [MaxLength(500)]
        public string? RoutineDescription { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<SplitDay> SplitDays { get; set; } = new List<SplitDay>();
    }
}
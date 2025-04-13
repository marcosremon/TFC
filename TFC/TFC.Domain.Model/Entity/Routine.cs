using System.ComponentModel.DataAnnotations;

namespace TFC.Domain.Model.Entity
{
    public class Routine
    {
        [Key]
        public long RoutineId { get; set; }
        [Required]
        public string? RoutineName { get; set; }
        public string? RoutineDescription { get; set; }
        public List<SplitDay> SplitDays { get; set; } = new List<SplitDay>();
    }
}
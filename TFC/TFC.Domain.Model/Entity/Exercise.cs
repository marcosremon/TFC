using System.ComponentModel.DataAnnotations;

namespace TFC.Domain.Model.Entity
{
    public class Exercise
    {
        [Key]
        public long ExercisId { get; set; }
        public string? ExerciseName { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }
    }
}
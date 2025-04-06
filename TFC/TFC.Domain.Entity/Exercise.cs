namespace TFC.Domain.Entity
{
    public class Exercise
    {
        public long ExerciseId { get; set; }
        public string? ExerciseName { get; set; }
        public long SplitDayId { get; set; }
        public SplitDay? SplitDay { get; set; }
        public List<Set>? Sets { get; set; }
    }
}
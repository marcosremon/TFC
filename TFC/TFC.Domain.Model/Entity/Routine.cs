namespace TFC.Domain.Model.Entity
{
    public class Routine
    {
        public string? RoutineId { get; set; }
        public string? RoutineName { get; set; }
        public string? RoutineDescription { get; set; }
        public List<SplitDay> SplitDays { get; set; } = new List<SplitDay>();
    }
}
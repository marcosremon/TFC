namespace TFC.Domain.Entity
{
    public class SplitDay
    {
        public long SplitDayId { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 
        public long GymSplitId { get; set; }
        public GymSplit? GymSplit { get; set; }
        public List<Exercise>? Exercises { get; set; }
    }
}
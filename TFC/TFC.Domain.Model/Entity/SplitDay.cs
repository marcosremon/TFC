namespace TFC.Domain.Model.Entity
{
    public class SplitDay
    {
        public string? DayId { get; set; }
        public WeekDay DayName { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
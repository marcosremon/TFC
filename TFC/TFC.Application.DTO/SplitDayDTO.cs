namespace TFC.Application.DTO
{
    public class SplitDayDTO
    {
        public long SplitDayId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<ExerciseDTO>? Exercises { get; set; }
    }
}
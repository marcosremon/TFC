namespace TFC.Application.DTO.EntityDTO
{
    public class SplitDayDTO
    {
        public string? DayId { get; set; }
        public DayOfWeek DayName { get; set; }
        public List<ExerciseDTO> Exercises { get; set; } = new List<ExerciseDTO>();
    }
}
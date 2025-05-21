namespace TFC.Application.DTO.Entity
{
    public class RoutineDTO
    {
        public long RoutineId { get; set; }
        public string? RoutineName { get; set; }
        public string? RoutineDescription { get; set; }
        public long UserId { get; set; }
        public List<SplitDayDTO> SplitDays { get; set; } = new List<SplitDayDTO>();
    }
}
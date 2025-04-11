namespace TFC.Application.DTO.EntityDTO
{
    public class RoutineDTO
    {
        public string? RoutineName { get; set; }
        public string? RoutineDescription { get; set; }
        public List<SplitDayDTO> SplitDays { get; set; } = new List<SplitDayDTO>();
    }
}
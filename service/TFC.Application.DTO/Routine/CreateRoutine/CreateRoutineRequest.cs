using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Routine.CreateRoutine
{
    public class CreateRoutineRequest
    {
        public string? UserEmail { get; set; }
        public string? RoutineName { get; set; }
        public string? RoutineDescription { get; set; }
        public List<SplitDayDTO> SplitDays { get; set; } = new List<SplitDayDTO>();
    }
}
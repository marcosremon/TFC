using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.Routine.CreateRoutine
{
    public class UpdateRoutineRequest
    {
        public long RoutineId { get; set; }
        public string? RoutineName { get; set; }
        public string? RoutineDescription { get; set; }
        public List<SplitDayDTO> SplitDays { get; set; } = new List<SplitDayDTO>();
    }
}
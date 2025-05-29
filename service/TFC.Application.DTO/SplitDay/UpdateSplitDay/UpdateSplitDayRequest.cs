using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.SplitDay.UpdateSplitDay
{
    public class UpdateSplitDayRequest
    {
        public int? RoutineId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public List<string> AddDays { get; set; } = new List<string>();
        public List<string> DeleteDays { get; set; } = new List<string>();
    }
}
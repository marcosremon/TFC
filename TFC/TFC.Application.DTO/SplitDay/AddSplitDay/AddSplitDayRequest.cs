using TFC.Domain.Model.Entity;
using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.SplitDay.AnyadirSplitDay
{
    public class AddSplitDayRequest
    {
        public WeekDay? DayName { get; set; }
        public long? RoutineId { get; set; }
        public long? UserId { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
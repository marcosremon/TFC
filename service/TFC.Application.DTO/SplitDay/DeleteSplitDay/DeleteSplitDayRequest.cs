using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.SplitDay.DeleteSplitDay
{
    public class DeleteSplitDayRequest
    {
        public WeekDay? DayName { get; set; }
        public long? RoutineId { get; set; }
        public long? UserId { get; set; }
    }
}
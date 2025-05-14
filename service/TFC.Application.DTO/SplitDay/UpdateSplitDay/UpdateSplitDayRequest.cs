using TFC.Application.DTO.EntityDTO;
using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.SplitDay.UpdateSplitDay
{
    public class UpdateSplitDayRequest
    {
        public WeekDay? DayName { get; set; }
        public long? RoutineId { get; set; }
        public long? UserId { get; set; }
        public ICollection<ExerciseDTO> Exercises { get; set; } = new List<ExerciseDTO>();
    }
}

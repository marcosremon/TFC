using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.Entity
{
    public class SplitDayDTO
    {
        public WeekDay? DayName { get; set; }
        public List<ExerciseDTO> Exercises { get; set; } = new List<ExerciseDTO>();
    }
}
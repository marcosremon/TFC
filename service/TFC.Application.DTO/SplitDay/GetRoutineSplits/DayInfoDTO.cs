using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.SplitDay.GetRoutineSplits
{
    public class DayInfoDTO
    {
        public WeekDay? WeekDay { get; set; }
        public string DayExercisesDescription { get; set; } = string.Empty; // Pecho, Espalda, Torso...
    }
}

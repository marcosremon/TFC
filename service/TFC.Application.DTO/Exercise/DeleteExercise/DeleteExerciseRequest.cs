using System.ComponentModel.DataAnnotations.Schema;
using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.Exercise.DeleteExecise
{
    public class DeleteExerciseRequest
    {
        public long? UserId { get; set; }
        public long? RoutineId { get; set; }
        public WeekDay? DayName { get; set; }
        public string? ExerciseName { get; set; }
    }
}
namespace TFC.Application.DTO.Exercise.AddExercise
{
    public class AddExerciseRequest
    {
        public List<string> ExercisesProgres { get; set; } = new List<string>();
        public string UserEmail { get; set; } = string.Empty;
        public int RoutineId { get; set; }
        public string DayName { get; set; } = string.Empty;
    }
}
namespace TFC.Application.DTO
{
    public class ExerciseDTO
    {
        public long ExerciseId { get; set; }
        public string? ExerciseName { get; set; }
        public List<SetDTO>? Sets { get; set; }
    }
}
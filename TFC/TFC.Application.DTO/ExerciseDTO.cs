namespace TFC.Service.Web.API.DTOs
{
    public class ExerciseDTO
    {
        public long ExerciseId { get; set; }
        public string? ExerciseName { get; set; }
        public List<SetDTO>? Sets { get; set; }
    }
}
namespace TFC.Domain.Entity
{
    public class Set
    {
        public long SetId { get; set; }
        public int Repetitions { get; set; }
        public long ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
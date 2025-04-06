namespace TFC.Domain.Entity
{
    public class GymSplit
    {
        public long GymSplitId { get; set; }
        public long UserId { get; set; }
        public User? User { get; set; }
        public string? Name { get; set; } 
        public List<SplitDay>? Days { get; set; }
    }
}
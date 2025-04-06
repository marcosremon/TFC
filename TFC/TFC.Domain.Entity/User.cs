namespace TFC.Domain.Entity
{
    public class User
    {
        public long UserId { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public string? UserEmail { get; set; }
        public DateTime InscriptionDate { get; set; }
        public int Seniority { get; set; } 
        public List<GymSplit>? GymSplits { get; set; }
    }
}
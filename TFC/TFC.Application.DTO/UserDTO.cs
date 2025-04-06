using TFC.Service.Web.API.DTOs;

namespace TFC.Application.DTO
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public string? UserEmail { get; set; }
        public DateTime InscriptionDate { get; set; }
        public int Antique { get; set; }
        public List<GymSplitDTO>? GymSplits { get; set; } 
    }
}
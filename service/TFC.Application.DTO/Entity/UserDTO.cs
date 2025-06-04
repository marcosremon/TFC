using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.Entity
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string? Dni { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Surname { get; set; }
        public string? FriendCode { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
        public string StringRole { get; set; } = string.Empty;
        public DateTime InscriptionDate { get; set; }
        public List<RoutineDTO> Routines { get; set; } = new List<RoutineDTO>();
    }
}
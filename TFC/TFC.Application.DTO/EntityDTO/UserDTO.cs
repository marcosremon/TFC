namespace TFC.Application.DTO.EntityDTO
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string? Dni { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; } 
        public DateTime InscriptionDate { get; set; }
        public List<RoutineDTO> Routines { get; set; } = new List<RoutineDTO>();
    }
}
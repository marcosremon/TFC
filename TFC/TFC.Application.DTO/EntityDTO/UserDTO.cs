namespace TFC.Application.DTO.EntityDTO
{
    public class UserDTO
    {
        public string? Dni { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<string> RoutinesIds { get; set; } = new List<string>();
    }
}
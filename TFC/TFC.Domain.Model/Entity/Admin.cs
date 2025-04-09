using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class Admin
    {
        public string? Dni { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public byte[]? Password { get; set; }
        public Role Role { get; set; } = Role.Admin;
        public DateTime InscriptionDate { get; set; } = DateTime.Now;
    }
}
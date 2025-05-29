using TFC.Domain.Model.Enum;

namespace TFC.Application.DTO.User.CreateGenericUser
{
    public class CreateGenericUserRequest
    {
        public string? Dni { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public Role Role { get; set; }
    }
}

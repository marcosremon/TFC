namespace TFC.Application.DTO.User.UpdateUser
{
    public class UpdateUserRequst
    {
        public string? OriginalEmail { get; set; }
        public string? DniToBeFound { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
    }
}
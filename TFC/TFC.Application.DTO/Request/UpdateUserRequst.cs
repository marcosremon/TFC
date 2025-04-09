namespace TFC.Application.DTO.Request
{
    public class UpdateUserRequst
    {
        public string? DniToBeFound { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}

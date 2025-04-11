namespace TFC.Application.DTO.User.ChangePasswordWithPasswordAndEmail
{
    public class ChangePasswordWithPasswordAndEmailRequest
    {
        public string? UserEmail { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }

    }
}
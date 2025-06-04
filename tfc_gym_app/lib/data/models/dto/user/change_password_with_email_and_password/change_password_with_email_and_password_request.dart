class ChangePasswordWithEmailAndPasswordRequest {
   final String email;
  final String newPassword;
  final String confirmPassword;
  final String oldPassword;

  ChangePasswordWithEmailAndPasswordRequest({
    required this.email,
    required this.newPassword,
    required this.confirmPassword,
    required this.oldPassword,
  });
}
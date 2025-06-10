class RegisterUserRequest {
  final String dni;
  final String username;
  final String surname;
  final String email;
  final String password;
  final String passwordConfirm;

  RegisterUserRequest({
    required this.dni,
    required this.username,
    required this.surname,
    required this.email,
    required this.password,
    required this.passwordConfirm,
  });
}
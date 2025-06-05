class UpdateUserRequest {
  String originalEmail;
  final String username;
  final String surname;
  final String dni;
  final String email;

  UpdateUserRequest({
    required this.originalEmail,
    required this.username,
    required this.surname,
    required this.dni,
    required this.email,
  });
}
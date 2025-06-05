import 'package:tfc_gym_app/data/models/enums/role.dart';

class User {
  final int userId;
  final String? dni;
  final String? username;
  final String? surname;
  final String? email;
  final String? friendCode;
  final Role role;
  final DateTime inscriptionDate;

  const User({
    required this.userId,
    this.dni,
    this.username,
    this.surname,
    this.email,
    this.friendCode,
    this.role = Role.user,
    required this.inscriptionDate,
  });
}
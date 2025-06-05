import 'package:json_annotation/json_annotation.dart';
import 'package:tfc_gym_app/data/models/enums/role.dart';

part 'user_dto.g.dart';

@JsonSerializable()
class UserDTO {
  final int userId;
  final String? dni;
  final String? username;
  final String? surname;
  final String? email;
  final String? friendCode;
  final String password;
  final int routinesCount;
  final int friendsCount;
  final DateTime? inscriptionDate; 
  final Role? role;

  const UserDTO({
    required this.userId,
    this.dni,
    this.username,
    this.surname,
    this.email,
    this.friendCode,
    this.password = "********",
    this.routinesCount = 0,
    this.friendsCount = 0,
    this.inscriptionDate, 
    this.role,
  });

  factory UserDTO.fromJson(Map<String, dynamic> json) => _$UserDTOFromJson(json);
  Map<String, dynamic> toJson() => _$UserDTOToJson(this);
}
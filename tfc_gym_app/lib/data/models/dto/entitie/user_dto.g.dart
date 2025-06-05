// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserDTO _$UserDTOFromJson(Map<String, dynamic> json) => UserDTO(
      userId: (json['userId'] as num).toInt(),
      dni: json['dni'] as String?,
      username: json['username'] as String?,
      surname: json['surname'] as String?,
      email: json['email'] as String?,
      friendCode: json['friendCode'] as String?,
      password: json['password'] as String? ?? "********",
      routinesCount: (json['routinesCount'] as num?)?.toInt() ?? 0,
      friendsCount: (json['friendsCount'] as num?)?.toInt() ?? 0,
      inscriptionDate: json['inscriptionDate'] == null
          ? null
          : DateTime.parse(json['inscriptionDate'] as String).toLocal().copyWith(
                hour: 0,
                minute: 0,
                second: 0,
                millisecond: 0,
                microsecond: 0,
              ),
    );

Map<String, dynamic> _$UserDTOToJson(UserDTO instance) => <String, dynamic>{
      'userId': instance.userId,
      'dni': instance.dni,
      'username': instance.username,
      'surname': instance.surname,
      'email': instance.email,
      'friendCode': instance.friendCode,
      'password': instance.password,
      'routinesCount': instance.routinesCount,
      'friendsCount': instance.friendsCount,
      'inscriptionDate': instance.inscriptionDate?.toLocal().toString().substring(0, 10),
    };
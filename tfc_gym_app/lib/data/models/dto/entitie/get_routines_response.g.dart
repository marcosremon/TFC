// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'get_routines_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

GetRoutinesResponse _$GetRoutinesResponseFromJson(Map<String, dynamic> json) =>
    GetRoutinesResponse(
      isSuccess: json['isSuccess'] as bool,
      message: json['message'] as String,
      friendRoutines:
          (json['friendRoutines'] as List<dynamic>)
              .map((e) => RoutineDTO.fromJson(e as Map<String, dynamic>))
              .toList(),
    );

Map<String, dynamic> _$GetRoutinesResponseToJson(
  GetRoutinesResponse instance,
) => <String, dynamic>{
  'isSuccess': instance.isSuccess,
  'message': instance.message,
  'friendRoutines': instance.friendRoutines.map((e) => e.toJson()).toList(),
};

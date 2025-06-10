// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'routine_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RoutineDTO _$RoutineDTOFromJson(Map<String, dynamic> json) => RoutineDTO(
  routineId: (json['routineId'] as num).toInt(),
  routineName: json['routineName'] as String?,
  routineDescription: json['routineDescription'] as String?,
  userId: (json['userId'] as num).toInt(),
  splitDays:
      (json['splitDays'] as List<dynamic>?)
          ?.map((e) => SplitDayDTO.fromJson(e as Map<String, dynamic>))
          .toList(),
);

Map<String, dynamic> _$RoutineDTOToJson(RoutineDTO instance) =>
    <String, dynamic>{
      'routineId': instance.routineId,
      'routineName': instance.routineName,
      'routineDescription': instance.routineDescription,
      'userId': instance.userId,
      'splitDays': instance.splitDays,
    };

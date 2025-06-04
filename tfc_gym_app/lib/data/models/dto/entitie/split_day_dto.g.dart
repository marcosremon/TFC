// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'split_day_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SplitDayDTO _$SplitDayDTOFromJson(Map<String, dynamic> json) => SplitDayDTO(
  dayName: (json['dayName'] as num).toInt(),
  exercises:
      (json['exercises'] as List<dynamic>?)
          ?.map((e) => ExerciseDTO.fromJson(e as Map<String, dynamic>))
          .toList() ??
      const [],
);

Map<String, dynamic> _$SplitDayDTOToJson(SplitDayDTO instance) =>
    <String, dynamic>{
      'dayName': instance.dayName,
      'exercises': instance.exercises.map((e) => e.toJson()).toList(),
    };

// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'exercise_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ExerciseDTO _$ExerciseDTOFromJson(Map<String, dynamic> json) => ExerciseDTO(
  exerciseId: (json['exerciseId'] as num).toInt(),
  exerciseName: json['exerciseName'] as String?,
  sets: (json['sets'] as num?)?.toInt(),
  reps: (json['reps'] as num?)?.toInt(),
  weight: (json['weight'] as num?)?.toDouble(),
  dayName: $enumDecodeNullable(_$WeekDayEnumMap, json['dayName']),
);

Map<String, dynamic> _$ExerciseDTOToJson(ExerciseDTO instance) =>
    <String, dynamic>{
      'exerciseId': instance.exerciseId,
      'exerciseName': instance.exerciseName,
      'sets': instance.sets,
      'reps': instance.reps,
      'weight': instance.weight,
      'dayName': _$WeekDayEnumMap[instance.dayName],
    };

const _$WeekDayEnumMap = {
  WeekDay.monday: 'monday',
  WeekDay.tuesday: 'tuesday',
  WeekDay.wednesday3: 'wednesday3',
  WeekDay.thursday: 'thursday',
  WeekDay.friday: 'friday',
  WeekDay.saturday: 'saturday',
  WeekDay.sunday: 'sunday',
};

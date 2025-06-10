// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'exercise_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ExerciseDTO _$ExerciseDTOFromJson(Map<String, dynamic> json) => ExerciseDTO(
  exerciseId: (json['exerciseId'] as num?)?.toInt(),
  exerciseName: json['exerciseName'] as String?,
  sets: (json['sets'] as num?)?.toInt(),
  reps: (json['reps'] as num?)?.toInt(),
  weight: (json['weight'] as num?)?.toDouble(),
  dayName: (json['dayName'] as num?)?.toInt(),
);

Map<String, dynamic> _$ExerciseDTOToJson(ExerciseDTO instance) =>
    <String, dynamic>{
      'exerciseId': instance.exerciseId,
      'exerciseName': instance.exerciseName,
      'sets': instance.sets,
      'reps': instance.reps,
      'weight': instance.weight,
      'dayName': instance.dayName,
    };

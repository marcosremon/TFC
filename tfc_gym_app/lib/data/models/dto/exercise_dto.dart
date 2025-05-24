import 'package:json_annotation/json_annotation.dart';
import '../enums/week_day.dart';

part 'exercise_dto.g.dart';

@JsonSerializable()
class ExerciseDTO {
  final int exerciseId;
  final String? exerciseName;
  final int? sets;
  final int? reps;
  final double? weight;
  final WeekDay? dayName;

  const ExerciseDTO({
    required this.exerciseId,
    this.exerciseName,
    this.sets,
    this.reps,
    this.weight,
    this.dayName,
  });

  factory ExerciseDTO.fromJson(Map<String, dynamic> json) => _$ExerciseDTOFromJson(json);
  Map<String, dynamic> toJson() => _$ExerciseDTOToJson(this);
}
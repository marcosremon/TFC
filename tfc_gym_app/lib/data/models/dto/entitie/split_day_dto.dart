import 'package:json_annotation/json_annotation.dart';
import 'exercise_dto.dart';

part 'split_day_dto.g.dart';

@JsonSerializable(explicitToJson: true)
class SplitDayDTO {
  final int dayName;
  final List<ExerciseDTO> exercises;

  const SplitDayDTO({
    required this.dayName,
    this.exercises = const [],
  });

  factory SplitDayDTO.fromJson(Map<String, dynamic> json) => _$SplitDayDTOFromJson(json);
  Map<String, dynamic> toJson() => _$SplitDayDTOToJson(this);
}
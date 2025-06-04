import 'package:json_annotation/json_annotation.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/split_day_dto.dart';

part 'routine_dto.g.dart';

@JsonSerializable()
class RoutineDTO {
  final int routineId;
  final String? routineName;
  final String? routineDescription;
  final int userId;
  final List<SplitDayDTO>? splitDays;

  RoutineDTO({
    required this.routineId,
    this.routineName,
    this.routineDescription,
    required this.userId,
    this.splitDays,
  });

  factory RoutineDTO.fromJson(Map<String, dynamic> json) => _$RoutineDTOFromJson(json);
  Map<String, dynamic> toJson() => _$RoutineDTOToJson(this);
}
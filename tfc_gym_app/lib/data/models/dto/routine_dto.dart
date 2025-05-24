import 'package:json_annotation/json_annotation.dart';

part 'routine_dto.g.dart';

@JsonSerializable()
class RoutineDTO {
  final int routineId;
  final String? routineName;
  final String? routineDescription;
  final int userId;

  RoutineDTO({
    required this.routineId,
    this.routineName,
    this.routineDescription,
    required this.userId,
  });

  factory RoutineDTO.fromJson(Map<String, dynamic> json) => _$RoutineDTOFromJson(json);
  Map<String, dynamic> toJson() => _$RoutineDTOToJson(this);
}
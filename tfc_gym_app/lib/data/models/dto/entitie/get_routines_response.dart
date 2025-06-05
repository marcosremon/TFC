import 'package:json_annotation/json_annotation.dart';
import 'routine_dto.dart';

part 'get_routines_response.g.dart';

@JsonSerializable(explicitToJson: true)
class GetRoutinesResponse {
  final bool isSuccess;
  final String message;
  final List<RoutineDTO> friendRoutines;

  GetRoutinesResponse({
    required this.isSuccess,
    required this.message,
    required this.friendRoutines,
  });

  factory GetRoutinesResponse.fromJson(Map<String, dynamic> json) =>
      _$GetRoutinesResponseFromJson(json);

  Map<String, dynamic> toJson() => _$GetRoutinesResponseToJson(this);
}
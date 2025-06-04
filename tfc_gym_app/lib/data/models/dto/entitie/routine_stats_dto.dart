class RoutineStatsDTO {
  final int routinesCount;
  final int exercisesCount;
  final int splitsCount;

  RoutineStatsDTO({
    required this.routinesCount,
    required this.exercisesCount,
    required this.splitsCount,
  });

  factory RoutineStatsDTO.fromJson(Map<String, dynamic> json) {
    return RoutineStatsDTO(
      routinesCount: json['routinesCount'] ?? 0,
      exercisesCount: json['exercisesCount'] ?? 0,
      splitsCount: json['splitsCount'] ?? 0,
    );
  }
}
class Routine {
  final int routineId;
  final String? routineName;
  final String? routineDescription;
  final int userId;

  Routine({
    required this.routineId,
    this.routineName,
    this.routineDescription,
    required this.userId,
  });
}
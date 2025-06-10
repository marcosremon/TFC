class DeleteExerciseRequest {
  final int routineId;
  final int dayName;
  String email;
  int exerciseId;

  DeleteExerciseRequest({
    required this.dayName,
    required this.routineId,
    required this.exerciseId,
    required this.email
  });
}
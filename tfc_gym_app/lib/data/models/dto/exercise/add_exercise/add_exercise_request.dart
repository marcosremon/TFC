class AddExerciseRequest {
  final int routineId;
  final String dayName;
  String email;
  final String exerciseName;

  AddExerciseRequest({
    required this.dayName,
    required this.routineId,
    required this.exerciseName,
    required this.email,
  });
}
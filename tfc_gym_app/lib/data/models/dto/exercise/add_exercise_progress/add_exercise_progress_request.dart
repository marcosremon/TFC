class AddExerciseProgressRequest {
  final List<String> progressList;
  final int routineId;
  final String dayName;
  String email;

  AddExerciseProgressRequest({
    required this.dayName,
    required this.routineId,
    required this.progressList,
    required this.email
  });
}
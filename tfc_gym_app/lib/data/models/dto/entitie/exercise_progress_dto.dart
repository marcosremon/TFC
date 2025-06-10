class ExerciseProgressDTO {
  final String exerciseName;
  final List<String> pastProgress; // Máximo 4 entradas

  ExerciseProgressDTO({
    required this.exerciseName,
    required this.pastProgress,
  });
}

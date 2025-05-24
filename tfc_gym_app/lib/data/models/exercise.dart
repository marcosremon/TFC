import 'package:tfc_gym_app/data/models/enums/week_day.dart';

class Exercise {
  final int exerciseId;
  final String? exerciseName;
  final int? sets;
  final int? reps;
  final double? weight;
  final int routineId;
  final WeekDay dayName;

  Exercise({
    required this.exerciseId,
    this.exerciseName,
    this.sets,
    this.reps,
    this.weight,
    required this.routineId,
    required this.dayName,
  });
}
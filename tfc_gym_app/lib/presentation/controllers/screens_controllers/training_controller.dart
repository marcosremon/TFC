import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_routines_stats/get_routines_stats_request.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';

class TrainingController extends ChangeNotifier {
  String? email;
  final RoutineProvider routineProvider;

  TrainingController({required this.routineProvider});

  Future<void> loadEmailAndFetchStats() async {
    final prefs = await SharedPreferences.getInstance();
    String? userEmail = prefs.getString('email');
    if (userEmail != null) {
      email = userEmail;
      notifyListeners();
      await routineProvider.getRoutineStats(GetRoutinesStatsRequest(email: userEmail));
    }
  }
}
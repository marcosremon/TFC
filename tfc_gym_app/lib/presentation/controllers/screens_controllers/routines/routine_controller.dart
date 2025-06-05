import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/routine/create_routine/create_routine_request.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';
import 'package:provider/provider.dart';

class RoutineController extends ChangeNotifier {
  final nameController = TextEditingController();
  final descController = TextEditingController();

  final List<int> selectedDays = [];
  final Map<int, List<Map<String, TextEditingController>>> exercisesByDay = {};

  final List<String> weekDays = [
    'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo'
  ];

  void addDay(int day) {
    if (!selectedDays.contains(day)) {
      selectedDays.add(day);
      exercisesByDay[day] = [
        {
          "exerciseName": TextEditingController(),
          "sets": TextEditingController(),
          "reps": TextEditingController(),
          "weight": TextEditingController(),
        }
      ];
      notifyListeners();
    }
  }

  void removeDay(int day) {
    selectedDays.remove(day);
    exercisesByDay.remove(day);
    notifyListeners();
  }

  void addExercise(int day) {
    exercisesByDay[day]?.add({
      "exerciseName": TextEditingController(),
      "sets": TextEditingController(),
      "reps": TextEditingController(),
      "weight": TextEditingController(),
    });
    notifyListeners();
  }

  void removeExercise(int day, int index) {
    exercisesByDay[day]?.removeAt(index);
    notifyListeners();
  }

  void clear() {
    nameController.clear();
    descController.clear();
    selectedDays.clear();
    exercisesByDay.clear();
    notifyListeners();
  }

  @override
  void dispose() {
    nameController.dispose();
    descController.dispose();
    for (var day in exercisesByDay.values) {
      for (var ctrls in day) {
        ctrls.values.forEach((c) => c.dispose());
      }
    }
    super.dispose();
  }

  Future<void> submitRoutine(BuildContext context) async {
    final name = nameController.text.trim();
    final desc = descController.text.trim();

    if (name.isEmpty) {
      if (context.mounted) {
        ToastMsg.showToast('Rellena todos los campos');
      }
      return;
    }
    if (selectedDays.isEmpty) {
      if (context.mounted) {
        ToastMsg.showToast('Selecciona al menos un día');
      }
      return;
    }

    final prefs = await SharedPreferences.getInstance();
    final email = prefs.getString('email');

    if (email == null) {
      if (context.mounted) {
        ToastMsg.showToast('No se encontró el email del usuario');
      }
      return;
    }

    final routine = {
      "userEmail": email,
      "routineName": name,
      "routineDescription": desc,
      "splitDays": selectedDays.map((day) {
        return {
          "dayName": day,
          "exercises": exercisesByDay[day]!.map((exerciseCtrls) {
            return {
              "exerciseId": 0,
              "exerciseName": exerciseCtrls["exerciseName"]!.text.trim(),
              "sets": int.tryParse(exerciseCtrls["sets"]!.text) ?? 0,
              "reps": int.tryParse(exerciseCtrls["reps"]!.text) ?? 0,
              "weight": double.tryParse(exerciseCtrls["weight"]!.text) ?? 0,
              "dayName": day + 1
            };
          }).toList()
        };
      }).toList()
    };
    await context.read<RoutineProvider>().createRoutine(CreateRoutineRequest(routine: routine));
    clear();

    if (context.mounted) Navigator.of(context).pop();
  }
}
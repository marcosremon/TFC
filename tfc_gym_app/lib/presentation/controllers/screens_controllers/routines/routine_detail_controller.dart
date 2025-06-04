import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/core/utils/week_day_utils.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/add_exercise_progress/add_exercise_progress_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/get_exercise_by_day_and_routine_name/get_exercise_by_day_and_routine_name_request.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/presentation/controllers/widgets_controllers/exercise_progress_table_controller.dart';
import 'package:tfc_gym_app/presentation/providers/exercise_provider.dart';

class RoutineDetailController extends ChangeNotifier {
  int? selectedIndex;
  late final ExerciseProgressTableController tableController;

  // Mapa para guardar el progreso temporal: {day: {exerciseId: value}}
  final Map<int, Map<int, String>> tempProgress = {};

  RoutineDetailController() {
    tableController = ExerciseProgressTableController(
      onProgressChanged: saveProgress, // <-- PASA EL CALLBACK AQUÍ
    );
  }

  void disposeController() {
    tableController.dispose();
    super.dispose();
  }

  String getWeekDayName(int? day) {
    return WeekDayUtils.getWeekDayName(day);
  }

  Future<void> onDaySelected(
    BuildContext context,
    int index,
    List splitDays,
    RoutineDTO routine,
  ) async {
    if (selectedIndex == index) {
      selectedIndex = null;
      notifyListeners();
      return;
    }
    selectedIndex = index;
    notifyListeners();

    final day = splitDays[index];
    await Provider.of<ExerciseProvider>(context, listen: false)
        .getExercisesByDayNameAndRoutineName(
      GetExerciseByDayAndRoutineNameRequest(
        dayName: day.dayName,
        routineId: routine.routineId,
      ),
    );
    // Rellena los campos con el progreso temporal guardado
    loadProgressForDay(day.dayName, Provider.of<ExerciseProvider>(context, listen: false).exercises);
  }

  void saveProgress(int day, int exerciseId, String value) {
    tempProgress.putIfAbsent(day, () => {});
    tempProgress[day]![exerciseId] = value;
    notifyListeners();
  }

  String? getProgress(int day, int exerciseId) {
    return tempProgress[day]?[exerciseId];
  }

  // Lógica para cargar ejercicios y rellenar los campos con progreso temporal
  void loadProgressForDay(int day, List exercises) {
    for (var exercise in exercises) {
      final value = getProgress(day, exercise.exerciseId);
      if (value != null) {
        tableController.setProgressValue(exercise.exerciseId, value);
      }
    }
  }

  Future<void> onSavePressed(
    BuildContext context,
    RoutineDTO routine,
    List splitDays,
  ) async {
    if (selectedIndex == null) {
      ToastMsg.showToast('Selecciona un día para guardar el progreso');
      return;
    }

    final provider = Provider.of<ExerciseProvider>(context, listen: false);
    if (!tableController.validateAndShowError(provider.exercises)) {
      ToastMsg.showToast('Completa todos los campos de la columna "Nuevo" para guardar');
      return;
    }

    var progressList = tableController.getProgressList(provider.exercises);
    int routineId = routine.routineId;
    var dayName = splitDays[selectedIndex!].dayName ?? 0;
    String dayNameStr = WeekDayUtils.getWeekDayName(dayName);

    bool success = await provider.addExerciseProgress(
      AddExerciseProgressRequest(
        dayName: dayNameStr,
        routineId: routineId,
        progressList: progressList,
        email: "email", 
      ),
    );

    if (success && context.mounted) {
      final day = splitDays[selectedIndex!];
      await provider.getExercisesByDayNameAndRoutineName(
        GetExerciseByDayAndRoutineNameRequest(
          dayName: day.dayName,
          routineId: routine.routineId,
        ),
      );
      ToastMsg.showToast("Progreso guardado y tabla actualizada");
    }
  }
}
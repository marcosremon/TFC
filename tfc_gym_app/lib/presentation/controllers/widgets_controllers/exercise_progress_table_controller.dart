import 'package:flutter/material.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/exercise_dto.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/progrees_input_bottom_sheet.dart';

class ExerciseProgressTableController extends ChangeNotifier  {
  final Map<int, String> _newProgress = {};
  bool showError = false;
  final void Function(int day, int exerciseId, String value)? onProgressChanged;

  ExerciseProgressTableController({this.onProgressChanged});

  void showBottomSheet(BuildContext context, int day, int exerciseId) {
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(top: Radius.circular(24)),
      ),
      builder: (ctx) {
        return ProgressInputBottomSheet(
          onSave: (series, reps, weight) {
            final value = "${series}x${reps}@${weight}";
            _newProgress[exerciseId] = value;
            showError = false;
            onProgressChanged?.call(day, exerciseId, value);
            notifyListeners(); 
            Navigator.pop(context);
          },
        );
      },
    );
  }

  List<String> getProgressList(List<ExerciseDTO> exercises) {
    final progressList = exercises
        .map((e) => _newProgress[e.exerciseId] ?? '')
        .toList();
    _newProgress.clear(); 
    return progressList;
  }

  bool hasEmptyFields(List<ExerciseDTO> exercises) {
    for (final exercise in exercises) {
      final value = _newProgress[exercise.exerciseId];
      if (value == null || value.isEmpty) {
        return true;
      }
    }
    return false;
  }

  bool validateAndShowError(List<ExerciseDTO> exercises) {
    final hasEmpty = hasEmptyFields(exercises);
    showError = hasEmpty;
    return !hasEmpty;
  }

  String? getProgressValue(int exerciseId) {
    return _newProgress[exerciseId];
  }

  void setProgressValue(int exerciseId, String value) {
    _newProgress[exerciseId] = value;
    notifyListeners();
  }
}
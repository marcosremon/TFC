import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/exercise_dto.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/add_exercise/add_exercise_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/add_exercise_progress/add_exercise_progress_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/delete_exercise/delete_exercise_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/get_exercise_by_day_and_routine_name/get_exercise_by_day_and_routine_name_request.dart';
import 'package:tfc_gym_app/data/repositories/exercise_repository.dart';

class ExerciseProvider extends ChangeNotifier {
  final ExerciseRepository _exerciseRepository;
  SharedPreferences? _prefs;

  List<ExerciseDTO> _exercises = [];
  Map<String, dynamic> _pastProgress = {};
  bool _isLoading = false;
  String? _error;

  ExerciseProvider({required ExerciseRepository exerciseRepository})
      : _exerciseRepository = exerciseRepository {
    _initPrefs();
  }

  Future<void> _initPrefs() async {
    _prefs = await SharedPreferences.getInstance();
    notifyListeners();
  }

  List<ExerciseDTO> get exercises => _exercises;
  Map<String, dynamic> get pastProgress => _pastProgress;
  bool get isLoading => _isLoading;
  String? get error => _error;

  Future<void> getExercisesByDayNameAndRoutineName(GetExerciseByDayAndRoutineNameRequest getExerciseByDayAndRoutineNameRequest) async {
    try {
      _isLoading = true;
      notifyListeners();

      var result = await _exerciseRepository.getExercisesByDayAndRoutine(getExerciseByDayAndRoutineNameRequest);
      var exercisesList = (result['exercises'] as List? ?? [])
          .map((json) => ExerciseDTO.fromJson(json))
          .toList();
      _exercises = exercisesList;
      _pastProgress = result['pastProgress'] as Map<String, dynamic>? ?? {};
      _error = null;
    } catch (e) {
      _error = 'Error al cargar ejercicios: ${e.toString()}';
      _exercises = [];
      _pastProgress = {};
      ToastMsg.showToast('Error inesperado al cargar ejercicios.');
    } finally {
      _isLoading = false;
      notifyListeners();
    }
  }

  Future<bool> addExerciseProgress(AddExerciseProgressRequest addExerciseProgressRequest) async {
    try {
      if (_prefs == null) await _initPrefs();
      String email = _prefs?.getString('email') ?? '';
      addExerciseProgressRequest.email = email;
      bool isSuccess = await _exerciseRepository.addExerciseProgress(addExerciseProgressRequest);
      notifyListeners();
      return isSuccess;
    } catch (e) {
      ToastMsg.showToast('Error inesperado al guardar el progreso.');
      notifyListeners();
      return false;
    }
  }

  Future<bool> addExercise(AddExerciseRequest addExerciseRequest) async {
    try {
      if (_prefs == null) await _initPrefs();
      String email = _prefs?.getString('email') ?? '';
      addExerciseRequest.email = email;
      bool result = await _exerciseRepository.addExercise(addExerciseRequest);
      return result;
    } catch (e) {
      ToastMsg.showToast('Error inesperado al añadir ejercicio.');
      return false;
    }
  }

  Future<bool> deleteExercise(DeleteExerciseRequest deleteExerciseRequest) async {
    try {
      if (_prefs == null) await _initPrefs();
      String email = _prefs?.getString('email') ?? '';
      deleteExerciseRequest.email = email;
      bool result = await _exerciseRepository.deleteExercise(deleteExerciseRequest);
      return result;
    } catch (e) {
      ToastMsg.showToast('Error inesperado al eliminar ejercicio.');
      return false;
    }
  }
}
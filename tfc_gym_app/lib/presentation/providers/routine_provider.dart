import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_stats_dto.dart';
import 'package:tfc_gym_app/data/models/dto/routine/create_routine/create_routine_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/delete_routine/delete_routine_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_routine_by_id/get_routine_by_id.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_routines_stats/get_routines_stats_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_user_routines/get_user_routines_request.dart';
import 'package:tfc_gym_app/data/repositories/routine_repository.dart';

class RoutineProvider extends ChangeNotifier {
  final RoutineRepository _routineRepository;
  SharedPreferences? _prefs;

  RoutineStatsDTO? _stats;
  RoutineStatsDTO? get stats => _stats;

  RoutineProvider({required RoutineRepository routineRepository}) : _routineRepository = routineRepository {
    _initPrefs();
  }

  Future<void> _initPrefs() async {
    _prefs = await SharedPreferences.getInstance();
    notifyListeners();
  }

  Future<List<RoutineDTO>> getAllUserRoutines(GetUserRoutinesRequest getUserRoutinesStatsRequest) async {
    try {
      String? userEmail = getUserRoutinesStatsRequest.email;
      if (userEmail.isEmpty) {
        userEmail = _prefs?.getString('email');
      }
      if (userEmail == null) {
        ToastMsg.showToast('No hay email guardado');
        return [];
      }
      var routines = await _routineRepository.getAllUserRoutines(GetUserRoutinesRequest(email: userEmail));
      return routines;
    } catch (_) {
      ToastMsg.showToast('No se pudo obtener las rutinas. Inténtalo de nuevo más tarde.');
      return [];
    }
  }

  Future<void> createRoutine(CreateRoutineRequest createRoutineRequest) async {
    try {
      await _routineRepository.createRoutine(createRoutineRequest);
      String email = createRoutineRequest.routine['userEmail'];
      await getAllUserRoutines(GetUserRoutinesRequest(email: email));
      await getRoutineStats(GetRoutinesStatsRequest(email: email));
      notifyListeners();
    } catch (e) {
      ToastMsg.showToast('Error inesperado al crear la rutina.');
    }
  }

  Future<void> getRoutineStats(GetRoutinesStatsRequest req) async {
    try {
      _stats = await _routineRepository.getRoutineStats(req);
      notifyListeners();
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener estadísticas de rutina.');
    }
  }

  Future<bool> deleteRoutine(DeleteRoutineRequest deleteRoutineRequest) async {
    try {
      deleteRoutineRequest.email = _prefs?.getString('email') ?? "";
      bool deleted = await _routineRepository.deleteRoutine(deleteRoutineRequest);
      if (deleted) {
        String email = deleteRoutineRequest.email;
        await getAllUserRoutines(GetUserRoutinesRequest(email: email));
        await getRoutineStats(GetRoutinesStatsRequest(email: email));
        notifyListeners();
      }
      return deleted;
    } catch (e) {
      ToastMsg.showToast('Error inesperado al eliminar la rutina.');
      return false;
    }
  }

  Future<RoutineDTO?> getRoutineById(GetRoutineById getRoutineById) async {
    try {
      final routine = await _routineRepository.getRoutineById(getRoutineById);
      return routine;
    } catch (e) {
      ToastMsg.showToast('Error al obtener la rutina.');
      return null;
    }
  }
}
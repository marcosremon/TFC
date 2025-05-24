import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/routine_dto.dart';
import 'package:tfc_gym_app/data/repositories/routine_repository.dart';

class RoutineProvider extends ChangeNotifier {
  final RoutineRepository _routineRepository;
  SharedPreferences? _prefs;

  RoutineProvider({required RoutineRepository routineRepository}) : _routineRepository = routineRepository {
    _initPrefs();
  }

  Future<void> _initPrefs() async {
    _prefs = await SharedPreferences.getInstance();
    notifyListeners();
  }

  Future<List<RoutineDTO>> getUserRoutines(String? email) async {
    try {
      String? userEmail = email;
      if (userEmail == null || userEmail.isEmpty) {
        userEmail = _prefs?.getString('email');
      }
      if (userEmail == null) {
        ToastMsg.showToast('No hay email guardado');
        return [];
      }
      var routines = await _routineRepository.getAllUserRoutines(userEmail);
      return routines;
    } catch (_) {
      ToastMsg.showToast('No se pudo obtener las rutinas. Inténtalo de nuevo más tarde.');
      return [];
    }
  }
}
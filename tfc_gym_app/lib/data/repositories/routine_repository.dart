import 'package:tfc_gym_app/data/datasources/routine_datasources.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_stats_dto.dart';
import 'package:tfc_gym_app/data/models/dto/routine/create_routine/create_routine_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/delete_routine/delete_routine_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_routine_by_id/get_routine_by_id.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_routines_stats/get_routines_stats_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_user_routines/get_user_routines_request.dart';

class RoutineRepository {
  final RoutineDatasources _routineDatasource;
  
  RoutineRepository({required RoutineDatasources routineDatasource}) : _routineDatasource = routineDatasource;
  
  Future<List<RoutineDTO>> getAllUserRoutines(GetUserRoutinesRequest getUserRoutinesStatsRequest) async {
    return await _routineDatasource.getAllUserRoutines(getUserRoutinesStatsRequest);
  }

  Future<void> createRoutine(CreateRoutineRequest createRoutineRequest) {
    String email = createRoutineRequest.routine['userEmail'];
    String routineName = createRoutineRequest.routine['routineName'];
    String routineDescription = createRoutineRequest.routine['routineDescription'];
    var splitDays = createRoutineRequest.routine['splitDays'];
    return _routineDatasource.createRoutine(
      email: email,
      routineName: routineName,
      routineDescription: routineDescription,
      splitDays: splitDays,
    );
  }

  Future<RoutineStatsDTO?> getRoutineStats(GetRoutinesStatsRequest getRoutinesStatsRequest) {
    return _routineDatasource.getRoutineStats(getRoutinesStatsRequest);
  }

  Future<bool> deleteRoutine(DeleteRoutineRequest deleteRoutineRequest) async {
    return await _routineDatasource.deleteRoutine(deleteRoutineRequest);
  }

  Future<RoutineDTO?> getRoutineById(GetRoutineById getRoutineById) async {
    return await _routineDatasource.getRoutineById(getRoutineById);
  }
}
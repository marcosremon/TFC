import 'package:tfc_gym_app/data/datasources/routine_datasources.dart';
import 'package:tfc_gym_app/data/models/dto/routine_dto.dart';

class RoutineRepository {
  final RoutineDatasources _routineDatasource;
  
  RoutineRepository({required RoutineDatasources routineDatasource}) : _routineDatasource = routineDatasource;
  
  Future<List<RoutineDTO>> getAllUserRoutines(String email) async {
    return await _routineDatasource.getAllUserRoutines(email);
  }
}
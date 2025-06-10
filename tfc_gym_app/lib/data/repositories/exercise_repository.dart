import 'package:tfc_gym_app/data/datasources/exercise_datasources.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/add_exercise/add_exercise_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/add_exercise_progress/add_exercise_progress_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/delete_exercise/delete_exercise_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/get_exercise_by_day_and_routine_name/get_exercise_by_day_and_routine_name_request.dart';

class ExerciseRepository {
  final ExerciseDatasource _datasource;

  ExerciseRepository({required ExerciseDatasource datasource}) : _datasource = datasource;

  Future<Map<String, dynamic>> getExercisesByDayAndRoutine(GetExerciseByDayAndRoutineNameRequest getExerciseByDayAndRoutineNameRequest) async {
    return await _datasource.getExercisesByDayAndRoutine(getExerciseByDayAndRoutineNameRequest);
  }

  Future<bool> addExerciseProgress(AddExerciseProgressRequest addExerciseProgressRequest) async {
    return await _datasource.addExerciseProgress(addExerciseProgressRequest);
  }

  Future<bool> addExercise(AddExerciseRequest addExerciseRequest) async {
    return await _datasource.addExercise(addExerciseRequest);
  }

  Future<bool> deleteExercise(DeleteExerciseRequest deleteExerciseRequest) async {
    return await _datasource.deleteExercise(deleteExerciseRequest);
  }
}
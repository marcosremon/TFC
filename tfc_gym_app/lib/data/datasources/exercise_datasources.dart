import 'package:dio/dio.dart';
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/core/utils/week_day_utils.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/add_exercise/add_exercise_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/add_exercise_progress/add_exercise_progress_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/delete_exercise/delete_exercise_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/get_exercise_by_day_and_routine_name/get_exercise_by_day_and_routine_name_request.dart';

class ExerciseDatasource {
  final Dio _dio;
  
  ExerciseDatasource()
    : _dio = Dio(BaseOptions(
          baseUrl: ApiConstants.baseUrl,
          headers: {'Content-Type': 'application/json'},
          validateStatus: (status) => status != null && status < 500,
    ));

  Future<Map<String, dynamic>> getExercisesByDayAndRoutine(GetExerciseByDayAndRoutineNameRequest getExerciseByDayAndRoutineNameRequest) async {
    try {
      String dayNameString = WeekDayUtils.getWeekDayName(getExerciseByDayAndRoutineNameRequest.dayName);
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.exerciseEndpoint}/get-exercises-by-day-and-routine-id',
        data: {
          'DayName': dayNameString,
          'RoutineId': getExerciseByDayAndRoutineNameRequest.routineId,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return {};
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      if (!(responseData['isSuccess'] ?? false)) {
        ToastMsg.showToast(responseData['message'] ?? 'Error al obtener ejercicios');
        return {};
      }

      return responseData;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo obtener los ejercicios. Inténtalo de nuevo más tarde.');
      return {};
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener ejercicios.');
      return {};
    }
  }

  Future<bool> addExerciseProgress(AddExerciseProgressRequest addExerciseProgressRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.exerciseEndpoint}/add-exercise-progress',
        data: {
          'ProgressList': addExerciseProgressRequest.progressList,
          'UserEmail': addExerciseProgressRequest.email,
          'RoutineId': addExerciseProgressRequest.routineId,
          'DayName': addExerciseProgressRequest.dayName,
        },
      );

      Map<String, dynamic>? responseData = response.data as Map<String, dynamic>?;
      ToastMsg.showToast(responseData?['message'] ?? 'Error al guardar el progreso');
      if (responseData == null || !(responseData['isSuccess'] ?? false)) {
        return false;
      }
      return true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo guardar el progreso. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al guardar el progreso.');
      return false;
    }
  }

  Future<bool> deleteExercise(DeleteExerciseRequest deleteExerciseRequest) async {
    try {
      String dayNameString = WeekDayUtils.getWeekDayName(deleteExerciseRequest.dayName);
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.exerciseEndpoint}/delete-exercise',
        data: {
          'RoutineId': deleteExerciseRequest.routineId,
          'DayName': dayNameString,
          'ExerciseId': deleteExerciseRequest.exerciseId,
          'UserEmail': deleteExerciseRequest.email,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'No se pudo eliminar el ejercicio.');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo eliminar el ejercicio. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al eliminar el ejercicio.');
      return false;
    }
  }

  Future<bool> addExercise(AddExerciseRequest addExerciseRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.exerciseEndpoint}/add-exercise',
        data: {
          'RoutineId': addExerciseRequest.routineId,
          'DayName': addExerciseRequest.dayName,
          'ExerciseName': addExerciseRequest.exerciseName,
          'UserEmail': addExerciseRequest.email,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'No se pudo añadir el ejercicio.');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo añadir el ejercicio. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al añadir el ejercicio.');
      return false;
    }
  }
}
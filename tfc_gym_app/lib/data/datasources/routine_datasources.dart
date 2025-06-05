import 'package:dio/dio.dart';
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_stats_dto.dart';
import 'package:tfc_gym_app/data/models/dto/routine/delete_routine/delete_routine_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_routine_by_id/get_routine_by_id.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_routines_stats/get_routines_stats_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_user_routines/get_user_routines_request.dart';

class RoutineDatasources {
  final Dio _dio;

  RoutineDatasources() : _dio = Dio() {
    _dio.options.baseUrl = ApiConstants.baseUrl;
    _dio.options.headers = {
      'Content-Type': 'application/json',
    };
    _dio.options.validateStatus = (status) => status! < 500;
  }

  Future<List<RoutineDTO>> getAllUserRoutines(GetUserRoutinesRequest getUserRoutinesStatsRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.routineEndpoint}/get-all-user-routines',
        data: {
          'UserEmail': getUserRoutinesStatsRequest.email
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return [];
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      if (!(responseData['isSuccess'] ?? false)) {
        ToastMsg.showToast(responseData['message'] ?? 'Error al obtener rutinas');
        return [];
      }

      var routinesList = responseData['routines'] as List? ?? [];
      return routinesList
          .map((json) => RoutineDTO.fromJson(json as Map<String, dynamic>))
          .toList();
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo obtener las rutinas. Inténtalo de nuevo más tarde.');
      return [];
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener rutinas.');
      return [];
    }
  }

  Future<void> createRoutine({required String email, required String routineName, required String routineDescription, required splitDays}) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.routineEndpoint}/create-routine',
        data: {
          'UserEmail': email,
          'RoutineName': routineName,
          'RoutineDescription': routineDescription,
          'SplitDays': splitDays,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'Error al crear rutina');
      if (!(responseData['isSuccess'] ?? false)) {
        return;
      }
    } on DioException catch (_) {
      ToastMsg.showToast('Error al crear la rutina. Inténtalo de nuevo más tarde.');
    } catch (_) {
      ToastMsg.showToast('Error inesperado al crear rutina.');
    }
  }

  Future<RoutineStatsDTO?> getRoutineStats(GetRoutinesStatsRequest getRoutinesStatsRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.routineEndpoint}/get-routine-stats',
        data: {
          'UserEmail': getRoutinesStatsRequest.email,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return null;
      }

      Map<String, dynamic> data = response.data as Map<String, dynamic>;
      if (!(data['isSuccess'] ?? false)) {
        ToastMsg.showToast(data['message'] ?? 'Error al obtener rutina');
        return null;
      }

      return RoutineStatsDTO.fromJson(data);
    } on DioException catch (_) {
      ToastMsg.showToast('Error al obtener la rutina. Inténtalo de nuevo más tarde.');
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener rutina.');
    }

    return null;
  }

  Future<bool> deleteRoutine(DeleteRoutineRequest deleteRoutineRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.routineEndpoint}/delete-routine',
        data: {
          'RoutineId': deleteRoutineRequest.routineId,
          'UserEmail': deleteRoutineRequest.email,
        },
      );

      if (response.statusCode == 204) {
        ToastMsg.showToast('Rutina eliminada correctamente.');
        return true;
      }

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'No se pudo eliminar la rutina.');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo eliminar la rutina. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al eliminar la rutina.');
      return false;
    }
  }

  Future<RoutineDTO?> getRoutineById(GetRoutineById getRoutineById) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.routineEndpoint}/get-routine-by-id',
        data: {
          'RoutineId': getRoutineById.routineId,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return null;
      }

      Map<String, dynamic> data = response.data as Map<String, dynamic>;
      if (!(data['isSuccess'] ?? false)) {
        ToastMsg.showToast(data['message'] ?? 'Error al obtener rutina');
        return null;
      }

      return RoutineDTO.fromJson(data['routineDTO']);
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo obtener la rutina. Inténtalo de nuevo más tarde.');
      return null;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener rutina.');
      return null;
    }
  }
}
import 'package:dio/dio.dart';
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/routine_dto.dart';

class RoutineDatasources {
  final Dio _dio;

  RoutineDatasources() : _dio = Dio() {
    _dio.options.baseUrl = ApiConstants.baseUrl;
    _dio.options.headers = {
      'Content-Type': 'application/json',
    };
    _dio.options.validateStatus = (status) => status! < 500;
  }

  Future<List<RoutineDTO>> getAllUserRoutines(String email) async {
    try {
      final response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.routineEndpoint}/get-all-user-routines',
        data: {'UserEmail': email},
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return [];
      }

      final responseData = response.data as Map<String, dynamic>;
      if (!(responseData['isSuccess'] ?? false)) {
        ToastMsg.showToast(responseData['message'] ?? 'Error al obtener rutinas');
        return [];
      }

      final routinesList = responseData['routines'] as List? ?? [];
      return routinesList.map((json) => RoutineDTO.fromJson(json)).toList();
     } on DioException catch (_) {
      ToastMsg.showToast('No se pudo obtener las rutinas. Inténtalo de nuevo más tarde.');
      return [];
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener rutinas.');
      return [];
    }
  }
}
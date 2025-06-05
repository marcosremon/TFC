import 'package:dio/dio.dart';
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/data/models/dto/split_day/update_split_day_request.dart';

class SplitDayDatasource {
  final Dio _dio;

  SplitDayDatasource() : _dio = Dio() {
    _dio.options.baseUrl = ApiConstants.baseUrl;
    _dio.options.headers = {
      'Content-Type': 'application/json',
    };
    _dio.options.validateStatus = (status) => status! < 500;
  }

  Future<bool> updateSplitDay(UpdateSplitDayRequest updateSplitDayRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.splitDayEndpoint}/update-split-day',
        data: {
          'UserEmail': updateSplitDayRequest.userEmail,
          'RoutineId': updateSplitDayRequest.routineId,
          'AddDays': updateSplitDayRequest.addDays,
          'DeleteDays': updateSplitDayRequest.deleteDays,
        },
      );
      if (response.data is Map && response.data['isSuccess'] == true) {
        return true;
      }
      return false;
    } catch (e) {
      return false;
    }
  }
}
import 'package:tfc_gym_app/data/datasources/split_day_datasource.dart';
import 'package:tfc_gym_app/data/models/dto/split_day/update_split_day_request.dart';

class SplitDayRepository {
  final SplitDayDatasource _splitDayDatasource;

  SplitDayRepository({required SplitDayDatasource splitDayDatasource})
      : _splitDayDatasource = splitDayDatasource;

  Future<bool> updateSplitDay(UpdateSplitDayRequest request) async {
    return await _splitDayDatasource.updateSplitDay(request);
  }
}
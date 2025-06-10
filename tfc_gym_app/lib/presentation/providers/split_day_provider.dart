import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/data/models/dto/split_day/update_split_day_request.dart';
import 'package:tfc_gym_app/data/repositories/split_day_repository.dart';

class SplitDayProvider extends ChangeNotifier {
  final SplitDayRepository _splitDayRepository;

  SplitDayProvider({required SplitDayRepository splitDayRepository})
      : _splitDayRepository = splitDayRepository;

  Future<bool> updateSplitDay(UpdateSplitDayRequest updateSplitDayRequest) async {
    var prefs = await SharedPreferences.getInstance();
    String userEmail = prefs.getString('email') ?? '';
    updateSplitDayRequest.userEmail = userEmail;
    return await _splitDayRepository.updateSplitDay(updateSplitDayRequest);
  }
}
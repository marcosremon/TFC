class UpdateSplitDayRequest {
  final List<String> addDays;
  final List<String> deleteDays;
  final int routineId;
  String? userEmail;

  UpdateSplitDayRequest({
    required this.addDays,
    required this.deleteDays,
    required this.routineId,
    required this.userEmail,
  });
}
// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'split_day_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

SplitDayDTO _$SplitDayDTOFromJson(Map<String, dynamic> json) => SplitDayDTO(
  dayName: $enumDecodeNullable(_$WeekDayEnumMap, json['dayName']),
  exercises:
      (json['exercises'] as List<dynamic>?)
          ?.map((e) => ExerciseDTO.fromJson(e as Map<String, dynamic>))
          .toList() ??
      const [],
);

Map<String, dynamic> _$SplitDayDTOToJson(SplitDayDTO instance) =>
    <String, dynamic>{
      'dayName': _$WeekDayEnumMap[instance.dayName],
      'exercises': instance.exercises.map((e) => e.toJson()).toList(),
    };

const _$WeekDayEnumMap = {
  WeekDay.monday: 'monday',
  WeekDay.tuesday: 'tuesday',
  WeekDay.wednesday3: 'wednesday3',
  WeekDay.thursday: 'thursday',
  WeekDay.friday: 'friday',
  WeekDay.saturday: 'saturday',
  WeekDay.sunday: 'sunday',
};

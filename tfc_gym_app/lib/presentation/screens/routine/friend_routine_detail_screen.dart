import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/core/utils/week_day_utils.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/get_exercise_by_day_and_routine_name/get_exercise_by_day_and_routine_name_request.dart';
import 'package:tfc_gym_app/presentation/controllers/widgets_controllers/exercise_progress_table_controller.dart';
import 'package:tfc_gym_app/presentation/providers/exercise_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/other/day_box.dart';
import 'package:tfc_gym_app/presentation/widgets/tables/friend_exercise_progress_table.dart';

class FriendRoutineDetailScreen extends StatefulWidget {
  final RoutineDTO routine;
  const FriendRoutineDetailScreen({super.key, required this.routine});

  @override
  State<FriendRoutineDetailScreen> createState() =>
      _FriendRoutineDetailScreenState();
}

class _FriendRoutineDetailScreenState extends State<FriendRoutineDetailScreen> {
  int? selectedIndex;

  @override
  void initState() {
    super.initState();
  }

  void _loadExercisesForDay(int? dayName) {
    if (dayName == null) return;
    final provider = Provider.of<ExerciseProvider>(context, listen: false);
    provider.getExercisesByDayNameAndRoutineName(
      GetExerciseByDayAndRoutineNameRequest(
        dayName: dayName,
        routineId: widget.routine.routineId,
      ),
    );
  }

  void _onDaySelected(int index, List<dynamic> splitDays) {
    setState(() {
      if (selectedIndex == index) {
        selectedIndex = null;
      } else {
        selectedIndex = index;
      }
    });

    if (selectedIndex != null) {
      _loadExercisesForDay(splitDays[selectedIndex!].dayName);
    }
  }

  @override
  Widget build(BuildContext context) {
    final splitDays = List.from(widget.routine.splitDays ?? []);
    splitDays.sort(
        (a, b) => (a.dayName ?? 0).compareTo(b.dayName ?? 0)); 

    return Scaffold(
      appBar: AppBar(
        title: Text(widget.routine.routineName ?? 'Rutina'),
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => Navigator.of(context).pop(),
        ),
      ),
      body: Column(
        children: [
          SizedBox(
            height: MediaQuery.of(context).size.height * 0.16,
            child: Center(
              child: Wrap(
                spacing: 1,
                runSpacing: 3,
                children: List.generate(splitDays.length, (index) {
                  final day = splitDays[index];
                  return DayBox(
                    day: WeekDayUtils.getWeekDayName(day.dayName),
                    selected: selectedIndex == index,
                    onChanged: (_) => _onDaySelected(index, splitDays),
                  );
                }),
              ),
            ),
          ),

          SizedBox(
            height: MediaQuery.of(context).size.height * 0.5,
            child: Center(
              child: selectedIndex == null
                  ? const Text('Selecciona un día para ver los ejercicios')
                  : Consumer<ExerciseProvider>(
                      builder: (context, provider, _) {
                        return FriendExerciseProgressTable(
                          exercises: provider.exercises,
                          pastProgress: provider.pastProgress,
                          routineId: widget.routine.routineId,
                          day: splitDays[selectedIndex!].dayName ?? 1,
                          controller: ExerciseProgressTableController(),
                        );
                      },
                    ),
            ),
          ),
        ],
      ),
    );
  }
}
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/core/utils/week_day_utils.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/add_exercise/add_exercise_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/delete_exercise/delete_exercise_request.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/get_exercise_by_day_and_routine_name/get_exercise_by_day_and_routine_name_request.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_routine_by_id/get_routine_by_id.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/routines/routine_detail_controller.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/add_exercise_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/edit_splits_days_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/other/day_box.dart';
import 'package:tfc_gym_app/presentation/widgets/tables/exercise_progress_table.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';
import 'package:tfc_gym_app/presentation/providers/exercise_provider.dart';

class RoutineDetailScreen extends StatefulWidget {
  final RoutineDTO routine;
  const RoutineDetailScreen({super.key, required this.routine});

  @override
  State<RoutineDetailScreen> createState() => _RoutineDetailScreenState();
}

class _RoutineDetailScreenState extends State<RoutineDetailScreen> {
  late RoutineDetailController controller;

  @override
  void initState() {
    super.initState();
    controller = RoutineDetailController();
  }

  @override
  void dispose() {
    controller.disposeController();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final splitDays = List.from(widget.routine.splitDays ?? []);
    splitDays.sort((a, b) => (a.dayName ?? 0).compareTo(b.dayName ?? 0));

    return ChangeNotifierProvider.value(
      value: controller,
      child: Consumer<RoutineDetailController>(
        builder: (context, ctrl, _) {
          return Scaffold(
            appBar: AppBar(title: Text(widget.routine.routineName ?? 'Rutina')),
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
                          day: ctrl.getWeekDayName(day.dayName),
                          selected: ctrl.selectedIndex == index,
                          onChanged: (_) => ctrl.onDaySelected(context, index, splitDays, widget.routine),
                        );
                      }),
                    ),
                  ),
                ),
                Expanded(
                  child: ctrl.selectedIndex == null
                      ? const Center(child: Text('Selecciona un día para ver los ejercicios'))
                      : Consumer<ExerciseProvider>(
                          builder: (context, provider, _) {
                            final dayName = splitDays[ctrl.selectedIndex!].dayName;
                            return Column(
                              children: [
                                Row(
                                  mainAxisAlignment: MainAxisAlignment.end,
                                  children: [
                                    IconButton(
                                      icon: const Icon(Icons.add),
                                      tooltip: 'Añadir ejercicio',
                                      onPressed: () async {
                                        showModalBottomSheet(
                                          context: context,
                                          isScrollControlled: true,
                                          shape: const RoundedRectangleBorder(
                                            borderRadius: BorderRadius.vertical(top: Radius.circular(24)),
                                          ),
                                          builder: (_) => AddExerciseBottomSheet(
                                            onAdd: (name) async {
                                              final success = await context.read<ExerciseProvider>().addExercise(
                                                AddExerciseRequest(
                                                  dayName: WeekDayUtils.getWeekDayName(dayName),
                                                  routineId: widget.routine.routineId,
                                                  exerciseName: name,
                                                  email: "",
                                                ),
                                              );
                                              if (success) {
                                                await context.read<ExerciseProvider>().getExercisesByDayNameAndRoutineName(
                                                  GetExerciseByDayAndRoutineNameRequest(
                                                    dayName: dayName,
                                                    routineId: widget.routine.routineId,
                                                  ),
                                                );
                                              }
                                            },
                                          ),
                                        );
                                      },
                                    ),
                                  ],
                                ),
                                Expanded(
                                  child: ExerciseProgressTable(
                                    exercises: provider.exercises,
                                    pastProgress: provider.pastProgress,
                                    controller: ctrl.tableController,
                                    routineId: widget.routine.routineId, 
                                    day: dayName,
                                    onDeleteExercise: (exerciseId) async {
                                      bool success = await context.read<ExerciseProvider>().deleteExercise(
                                        DeleteExerciseRequest(
                                          dayName: dayName,
                                          routineId: widget.routine.routineId,
                                          exerciseId: exerciseId,
                                          email: "",
                                        ),
                                      );
                                      if (success) {
                                        await context.read<ExerciseProvider>().getExercisesByDayNameAndRoutineName(
                                          GetExerciseByDayAndRoutineNameRequest(
                                            dayName: dayName,
                                            routineId: widget.routine.routineId,
                                          ),
                                        );
                                      }
                                    },
                                  ),
                                ),
                              ],
                            );
                          },
                        ),
                ),
                Padding(
                  padding: const EdgeInsets.only(left: 16.0, right: 16.0, bottom: 32.0, top: 16.0),
                  child: Row(
                    children: [
                      Expanded(
                        child: CustomButton(
                          text: 'Guardar',
                          onPressed: () => ctrl.onSavePressed(context, widget.routine, splitDays),
                        ),
                      ),
                      const SizedBox(width: 16),
                      FloatingActionButton(
                        heroTag: 'editSplitDays',
                        mini: true,
                        onPressed: () async {
                          final result = await showModalBottomSheet(
                            context: context,
                            isScrollControlled: true,
                            shape: const RoundedRectangleBorder(
                              borderRadius: BorderRadius.vertical(top: Radius.circular(24)),
                            ),
                            builder: (_) => EditSplitDaysBottomSheet(
                              routineId: widget.routine.routineId,
                              currentDays: splitDays.map((d) => d.dayName as int).toList(),
                            ),
                          );
                          if (result == true) {
                            RoutineDTO? updatedRoutine = await context.read<RoutineProvider>().getRoutineById(
                              GetRoutineById(
                                routineId: widget.routine.routineId,
                              ),
                            );
                            if (updatedRoutine != null && mounted) {
                              Navigator.pushReplacement(
                                context,
                                MaterialPageRoute(
                                  builder: (_) => RoutineDetailScreen(routine: updatedRoutine),
                                ),
                              );
                            }
                          }
                        },
                        child: const Icon(Icons.edit_calendar),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          );
        },
      ),
    );
  }
}
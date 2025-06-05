import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/core/constants/colors_constants.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/exercise_dto.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/get_exercise_by_day_and_routine_name/get_exercise_by_day_and_routine_name_request.dart';
import 'package:tfc_gym_app/presentation/controllers/widgets_controllers/exercise_progress_table_controller.dart';
import 'package:tfc_gym_app/presentation/providers/exercise_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/pop_ups/delete_exercise_pop_up.dart';

class ExerciseProgressTable extends StatelessWidget {
  final int routineId;
  final int day;
  final List<ExerciseDTO> exercises;
  final Map<String, dynamic> pastProgress;
  final ExerciseProgressTableController controller;
  final Future<void> Function(int exerciseId)? onDeleteExercise;

  const ExerciseProgressTable({
    super.key,
    required this.routineId,
    required this.day,
    required this.exercises,
    required this.pastProgress,
    required this.controller,
    this.onDeleteExercise,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Expanded(
          child: SingleChildScrollView(
            scrollDirection: Axis.vertical,
            child: SingleChildScrollView(
              scrollDirection: Axis.horizontal,
              padding: const EdgeInsets.all(12),
              child: DataTable(
                headingRowColor: MaterialStateProperty.all(ColorsConstants.color149),
                headingTextStyle: TextStyle(
                  color: ColorsConstants.color0,
                  fontWeight: FontWeight.bold,
                ),
                dataRowColor: MaterialStateProperty.resolveWith<Color?>((states) {
                  if (states.contains(MaterialState.selected)) {
                    return ColorsConstants.color1.withOpacity(0.1);
                  }
                  return null;
                }),
                columns: const [
                  DataColumn(label: Text('Ejercicio')),
                  DataColumn(label: Text('Nuevo')),
                  DataColumn(label: Text('Última')),
                  DataColumn(label: Text('Anterior')),
                  DataColumn(label: Text('Anterior 2')),
                  DataColumn(label: Text('Acciones')),
                ],
                rows: exercises.map((exercise) {
                  final newValue = controller.getProgressValue(exercise.exerciseId ?? 0);
                  final progress = pastProgress[exercise.exerciseId.toString()] as List? ?? [];
                  final last3 = List.generate(3, (i) => progress.length > i ? progress[i] : '-');
                  return DataRow(
                    cells: [
                      DataCell(Text(exercise.exerciseName ?? 'Sin nombre')),
                      DataCell(
                        GestureDetector(
                          onTap: () {
                            if (exercise.exerciseId != null) {
                              controller.showBottomSheet(
                                  context, day, exercise.exerciseId!);
                            }
                          },
                          child: SizedBox(
                            width: 80,
                            child: newValue != null
                                ? Text(newValue)
                                : TextFormField(
                                    enabled: false,
                                    decoration: const InputDecoration(
                                      hintText: '...',
                                      hintStyle: TextStyle(color: Colors.grey),
                                      contentPadding: EdgeInsets.symmetric(
                                          horizontal: 8, vertical: 4),
                                      border: OutlineInputBorder(),
                                    ),
                                  ),
                          ),
                        ),
                      ),
                      ...last3.map((v) => DataCell(Text(v))),
                      DataCell(
                        IconButton(
                          icon: const Icon(Icons.delete, color: Colors.red),
                          onPressed: () {
                            showDialog(
                              context: context,
                              builder: (_) => DeleteExercisePopUp(
                                routineId: routineId,
                                dayName: day,        
                                exerciseId: exercise.exerciseId!,
                                onDeleted: () async {
                                  await context.read<ExerciseProvider>().getExercisesByDayNameAndRoutineName(
                                    GetExerciseByDayAndRoutineNameRequest(
                                      dayName: day,
                                      routineId: routineId,
                                    ),
                                  );
                                },
                              ),
                            );
                          },
                        ),
                      ),
                    ],
                  );
                }).toList(),
              ),
            ),
          ),
        ),
        if (controller.showError)
          Padding(
            padding: const EdgeInsets.only(bottom: 8.0),
            child: Text(
              'Completa todos los campos de la columna "Nuevo" para guardar',
              style: TextStyle(color: Colors.red),
            ),
          ),
      ],
    );
  }
}
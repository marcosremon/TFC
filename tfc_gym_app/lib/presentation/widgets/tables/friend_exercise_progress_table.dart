import 'package:flutter/material.dart';
import 'package:tfc_gym_app/core/constants/colors_constants.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/exercise_dto.dart';
import 'package:tfc_gym_app/presentation/controllers/widgets_controllers/exercise_progress_table_controller.dart';

class FriendExerciseProgressTable extends StatelessWidget {
  final int routineId;
  final int day;
  final List<ExerciseDTO> exercises;
  final Map<String, dynamic> pastProgress;
  final ExerciseProgressTableController controller;

  const FriendExerciseProgressTable({
    super.key,
    required this.routineId,
    required this.day,
    required this.exercises,
    required this.pastProgress,
    required this.controller,
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
                dataRowColor: MaterialStateProperty.resolveWith<Color?>(
                  (states) => states.contains(MaterialState.selected)
                      ? ColorsConstants.color1.withOpacity(0.1)
                      : null,
                ),
                columns: const [
                  DataColumn(label: Text('Ejercicio')),
                  DataColumn(label: Text('Nuevo')),
                  DataColumn(label: Text('Última')),
                  DataColumn(label: Text('Anterior')),
                  DataColumn(label: Text('Anterior 2')),
                ],
                rows: exercises.map((exercise) {
                  final newValue = controller.getProgressValue(exercise.exerciseId ?? 0);
                  final progress =
                      pastProgress[exercise.exerciseId.toString()] as List? ?? [];
                  final last3 =
                      List.generate(3, (i) => progress.length > i ? progress[i] : '-');

                  return DataRow(
                    cells: [
                      DataCell(Text(exercise.exerciseName ?? 'Sin nombre')),
                      DataCell(
                        GestureDetector(
                          onTap: () {
                            if (exercise.exerciseId != null) {
                              controller.showBottomSheet(
                                context,
                                day,
                                exercise.exerciseId!,
                              );
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
                                      contentPadding:
                                          EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                                      border: OutlineInputBorder(),
                                    ),
                                  ),
                          ),
                        ),
                      ),
                      ...last3.map((v) => DataCell(Text(v))),
                    ],
                  );
                }).toList(),
              ),
            ),
          ),
        ),
        if (controller.showError)
          const Padding(
            padding: EdgeInsets.only(bottom: 8.0),
            child: Text(
              'Completa todos los campos de la columna "Nuevo" para guardar',
              style: TextStyle(color: Colors.red),
            ),
          ),
      ],
    );
  }
}

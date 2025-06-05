import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/exercise/delete_exercise/delete_exercise_request.dart';
import 'package:tfc_gym_app/presentation/providers/exercise_provider.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';

class DeleteExercisePopUp extends StatelessWidget {
  final int routineId;
  final int dayName;
  final int exerciseId;
  final VoidCallback? onDeleted;

  const DeleteExercisePopUp({
    super.key,
    required this.routineId,
    required this.dayName,
    required this.exerciseId,
    this.onDeleted,
  });

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: const Text('Eliminar ejercicio'),
      content: const Text('¿Seguro que quieres eliminar este ejercicio?'),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('Cancelar'),
        ),
        ElevatedButton(
          style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
          onPressed: () async {
            final deleted = await context.read<ExerciseProvider>().deleteExercise(
              DeleteExerciseRequest(
                routineId: routineId,
                dayName: dayName,
                exerciseId: exerciseId,
                email: "",
              ),
            );
            Navigator.of(context).pop();
            if (deleted) {
              ToastMsg.showToast('Ejercicio eliminado correctamente');
              if (onDeleted != null) onDeleted!();
            }
          },
          child: const Text(
            'Eliminar',
            style: TextStyle(color: Colors.white),
          ),
        ),
      ],
    );
  }
}
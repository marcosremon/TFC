import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/data/models/dto/routine/delete_routine/delete_routine_request.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';

class DeleteRoutinePopUp extends StatelessWidget {
  final RoutineDTO routine;
  final VoidCallback onDeleted;

  const DeleteRoutinePopUp({
    super.key,
    required this.routine,
    required this.onDeleted,
  });

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: const Text('Eliminar rutina'),
      content: Text('¿Seguro que quieres eliminar la rutina "${routine.routineName ?? 'Sin nombre'}"?'),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('Cancelar'),
        ),
        ElevatedButton(
          onPressed: () async {
            try {
              bool deleted = await context.read<RoutineProvider>().deleteRoutine(
                DeleteRoutineRequest(
                  routineId: routine.routineId,
                  email: '', 
                ),
              );
              
              if (deleted) {
                Navigator.of(context).pop();
                onDeleted();
                ToastMsg.showToast('Rutina eliminada correctamente');
              }
            } catch (e) {
              ToastMsg.showToast('Error al eliminar: ${e.toString()}');
            }
          },
          style: ElevatedButton.styleFrom(
            backgroundColor: Colors.red,
          ),
          child: const Text(
            'Eliminar',
            style: TextStyle(color: Colors.white),
          ),
        ),
      ],
    );
  }
}
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';

class DeleteAccountPopUp extends StatelessWidget {
  final VoidCallback? onDeleted;

  const DeleteAccountPopUp({super.key, this.onDeleted});

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: const Text('Eliminar cuenta'),
      content: const Text('¿Seguro que quieres eliminar tu cuenta? Esta acción no se puede deshacer.'),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('Cancelar'),
        ),
        ElevatedButton(
          onPressed: () async {
            var deleted = await context.read<UserProvider>().deleteAccount();
            Navigator.of(context).pop();
            if (deleted && onDeleted != null) {
              onDeleted!();
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
import 'package:flutter/material.dart';

class InfoPopUp extends StatelessWidget {
  final String message;

  const InfoPopUp(this.message, {super.key});

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: const Text('Información'),
      content: Text(message),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('Cerrar'),
        ),
      ],
    );
  }
}
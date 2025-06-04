import 'package:flutter/material.dart';

class HowToUseAppBottomSheet extends StatelessWidget {
  const HowToUseAppBottomSheet({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(24.0),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: const [
          Text(
            '¿Cómo usar la app?',
            style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
          ),
          SizedBox(height: 16),
          Text(
            '• Pulsa "Crear Rutina" para añadir tus entrenamientos.\n'
            '• Personaliza tus rutinas a tu gusto.\n'
            '• ¡Empieza a entrenar y sigue tu progreso!',
            style: TextStyle(fontSize: 16),
          ),
        ],
      ),
    );
  }
}
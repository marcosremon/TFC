import 'package:flutter/material.dart';
import 'package:tfc_gym_app/presentation/widgets/other/custom_text_field.dart';

class ExerciseCard extends StatelessWidget {
  final Map<String, TextEditingController> ctrls;
  final VoidCallback? onRemove;
  final bool showRemove;

  const ExerciseCard({
    super.key,
    required this.ctrls,
    this.onRemove,
    this.showRemove = false,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.symmetric(vertical: 8),
      child: Padding(
        padding: const EdgeInsets.all(12.0),
        child: Column(
          children: [
            Row(
              children: [
                Expanded(
                  child: CustomTextField(
                    controller: ctrls["exerciseName"]!,
                    labelText: 'Ejercicio',
                    prefixIcon: Icons.sports_gymnastics,
                  ),
                ),
                if (showRemove)
                  IconButton(
                    icon: const Icon(Icons.delete, color: Colors.red),
                    onPressed: onRemove,
                  ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
import 'package:flutter/material.dart';
import 'package:flutter/services.dart'; // Añade esta importación
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';
import 'package:tfc_gym_app/presentation/widgets/other/custom_text_field.dart';

class ProgressInputBottomSheet extends StatefulWidget {
  final void Function(String series, String reps, String weight) onSave;

  const ProgressInputBottomSheet({super.key, required this.onSave});

  @override
  State<ProgressInputBottomSheet> createState() => _ProgressInputBottomSheetState();
}

class _ProgressInputBottomSheetState extends State<ProgressInputBottomSheet> {
  final TextEditingController seriesController = TextEditingController();
  final TextEditingController repsController = TextEditingController();
  final TextEditingController weightController = TextEditingController();
  bool showError = false;

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Padding(
        padding: EdgeInsets.only(
          left: 24,
          right: 24,
          top: 24,
          bottom: MediaQuery.of(context).viewInsets.bottom + 24,
        ),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const Text(
              'Nuevo progreso',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 18),
            CustomTextField(
              controller: seriesController,
              labelText: 'Series',
              prefixIcon: Icons.repeat,
              keyboardType: TextInputType.number,
              inputFormatters: [FilteringTextInputFormatter.digitsOnly],
            ),
            const SizedBox(height: 10),
            CustomTextField(
              controller: repsController,
              labelText: 'Repeticiones',
              prefixIcon: Icons.numbers,
              keyboardType: TextInputType.number,
              inputFormatters: [FilteringTextInputFormatter.digitsOnly],
            ),
            const SizedBox(height: 10),
            CustomTextField(
              controller: weightController,
              labelText: 'Peso',
              prefixIcon: Icons.fitness_center,
              keyboardType: const TextInputType.numberWithOptions(decimal: true),
              inputFormatters: [
                FilteringTextInputFormatter.allow(RegExp(r'^\d*\.?\d{0,2}')),
              ],
            ),
            const SizedBox(height: 18),
            CustomButton(
              text: 'Guardar',
              onPressed: () {
                String series = seriesController.text.trim();
                String reps = repsController.text.trim();
                String weight = weightController.text.trim();

                if (series.isNotEmpty && reps.isNotEmpty && weight.isNotEmpty) {
                  widget.onSave(series, reps, weight);
                } else {
                  setState(() {
                    showError = true;
                  });
                }
              },
            ),
            if (showError)
              const Padding(
                padding: EdgeInsets.only(top: 8.0),
                child: Text(
                  'Completa todos los campos para guardar',
                  style: TextStyle(color: Colors.red),
                ),
              ),
          ],
        ),
      ),
    );
  }
}
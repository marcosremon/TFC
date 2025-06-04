import 'package:flutter/material.dart';
import 'package:tfc_gym_app/presentation/widgets/other/custom_text_field.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';

class AddExerciseBottomSheet extends StatefulWidget {
  final void Function(String name) onAdd;

  const AddExerciseBottomSheet({super.key, required this.onAdd});

  @override
  State<AddExerciseBottomSheet> createState() => _AddExerciseBottomSheetState();
}

class _AddExerciseBottomSheetState extends State<AddExerciseBottomSheet> {
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();

  @override
  void dispose() {
    _nameController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: MediaQuery.of(context).viewInsets,
      child: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(24.0),
          child: Form(
            key: _formKey,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                Text('Añadir ejercicio', style: Theme.of(context).textTheme.titleLarge),
                const SizedBox(height: 16),
                CustomTextField(
                  controller: _nameController,
                  labelText: 'Nombre del ejercicio',
                  prefixIcon: Icons.sports_gymnastics,
                ),
                const SizedBox(height: 24),
                CustomButton(
                  text: 'Añadir',
                  onPressed: () {
                    if (_formKey.currentState?.validate() ?? false) {
                      final name = _nameController.text.trim();
                      widget.onAdd(name);
                      Navigator.of(context).pop();
                    }
                  },
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
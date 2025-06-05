import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/routines/routine_controller.dart';
import 'package:tfc_gym_app/presentation/widgets/cards/exercise_card.dart';
import 'package:tfc_gym_app/presentation/widgets/other/custom_text_field.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';

class CreateRoutineScreen extends StatelessWidget {
  const CreateRoutineScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final controller = Provider.of<RoutineController>(context);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Crear Rutina'),
        centerTitle: true,
      ),
      body: ListView(
        padding: const EdgeInsets.all(24),
        children: [
          CustomTextField(
            controller: controller.nameController,
            labelText: 'Nombre de la rutina',
            prefixIcon: Icons.fitness_center,
          ),
          const SizedBox(height: 16),
          CustomTextField(
            controller: controller.descController,
            labelText: 'Descripción',
            prefixIcon: Icons.description,
          ),
          const SizedBox(height: 24),
          Text(
            'Selecciona los días',
            style: Theme.of(context).textTheme.titleMedium,
          ),
          const SizedBox(height: 8),
          Wrap(
            spacing: 8,
            children: List.generate(7, (i) {
              final selected = controller.selectedDays.contains(i);
              return FilterChip(
                label: Text(controller.weekDays[i]),
                selected: selected,
                onSelected: (userSelected) {
                  if (userSelected) {
                    controller.addDay(i);
                  } else {
                    controller.removeDay(i);
                  }
                },
              );
            }),
          ),
          const SizedBox(height: 24),
          for (var day in controller.selectedDays)
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const SizedBox(height: 16),
                Row(
                  children: [
                    Text(
                      controller.weekDays[day],
                      style: Theme.of(context).textTheme.titleMedium,
                    ),
                    const Spacer(),
                    IconButton(
                      icon: const Icon(Icons.add),
                      tooltip: 'Añadir ejercicio',
                      onPressed: () => controller.addExercise(day),
                    ),
                  ],
                ),
                for (var entry in controller.exercisesByDay[day]!.asMap().entries)
                  ExerciseCard(
                    ctrls: entry.value,
                    showRemove: controller.exercisesByDay[day]!.length > 1,
                    onRemove: () => controller.removeExercise(day, entry.key),
                  ),
              ],
            ),
          const SizedBox(height: 32),
          CustomButton(
            text: 'Crear rutina',
            onPressed: () {
              controller.submitRoutine(context);
            },
          ),
        ],
      ),
    );
  }
}
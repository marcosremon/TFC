import 'package:flutter/material.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/how_to_use_app_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/stats_card.dart';

class TrainingScreen extends StatelessWidget {
  const TrainingScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final int rutinasCreadas = 0;
    final int diasActivos = 0;
    final int ejerciciosDistintos = 0;

    return Scaffold(
      appBar: AppBar(
        title: const Text('Entrenamientos'),
        centerTitle: true,
      ),
      body: ListView(
        padding: const EdgeInsets.all(24),
        children: [
          Container(
            height: 180,
            decoration: BoxDecoration(
              color: Theme.of(context).colorScheme.secondaryContainer,
              borderRadius: BorderRadius.circular(16),
            ),
            child: Center(
              child: Icon(
                Icons.fitness_center,
                size: 80,
                color: Theme.of(context).colorScheme.primary,
              ),
            ),
          ),
          const SizedBox(height: 32),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: [
              StatsCard(label: 'Rutinas', value: rutinasCreadas.toString(), icon: Icons.list_alt),
              StatsCard(label: 'Ejercicio semanal', value: ejerciciosDistintos.toString(), icon: Icons.sports_gymnastics),
              StatsCard(label: 'Racha', value: diasActivos.toString(), icon: Icons.calendar_today),
            ],
          ),
          const SizedBox(height: 40),
          FilledButton.icon(
            onPressed: () {

              // agregar rutinas

            },
            icon: const Icon(Icons.add),
            label: const Text('Crear Rutina'),
            style: FilledButton.styleFrom(
              padding: const EdgeInsets.symmetric(vertical: 18),
              textStyle: const TextStyle(fontSize: 18),
              shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
            ),
          ),
          const SizedBox(height: 40),
          Container(
            padding: const EdgeInsets.all(24),
            decoration: BoxDecoration(
              color: Theme.of(context).colorScheme.secondaryContainer,
              borderRadius: BorderRadius.circular(16),
            ),
            child: Column(
              children: [
                Icon(Icons.lightbulb_outline, size: 40, color: Theme.of(context).colorScheme.primary),
                const SizedBox(height: 12),
                const Text(
                  '¿Sabías que puedes personalizar tus rutinas y añadir tus propios ejercicios?',
                  textAlign: TextAlign.center,
                  style: TextStyle(fontSize: 16, color: Colors.black87),
                ),
              ],
            ),
          ),
        ],
      ),
      bottomNavigationBar: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: FilledButton.tonalIcon(
            onPressed: () {
              showModalBottomSheet(
                context: context,
                builder: (_) => const HowToUseAppBottomSheet(),
              );
            },
            icon: const Icon(Icons.info_outline),
            label: const Text('¿Cómo usar la app?'),
            style: FilledButton.styleFrom(
              minimumSize: const Size.fromHeight(48),
              shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
            ),
          ),
        ),
      ),
    );
  }
}
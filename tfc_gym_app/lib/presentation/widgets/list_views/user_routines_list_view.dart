import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/routine_dto.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';

class UserRoutinesListView extends StatelessWidget {
  final String? email;
  const UserRoutinesListView({super.key, this.email});

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<RoutineDTO>>(
      future: Provider.of<RoutineProvider>(context, listen: false).getUserRoutines(email),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const Center(child: CircularProgressIndicator());
        }
        if (snapshot.hasError || !snapshot.hasData) {
          return const Center(child: Text('No se pudieron cargar las rutinas'));
        }
        final routines = snapshot.data!;
        if (routines.isEmpty) {
          return const Center(child: Text('No hay rutinas'));
        }
        return ListView.builder(
          itemCount: routines.length,
          itemBuilder: (context, index) {
            final routine = routines[index];
            return ListTile(
              leading: const Icon(Icons.fitness_center, color: Colors.deepPurple),
              title: Text(routine.routineName ?? 'Sin nombre'),
              subtitle: Text(routine.routineDescription ?? 'Sin descripción'),
            );
          },
        );
      },
    );
  }
}
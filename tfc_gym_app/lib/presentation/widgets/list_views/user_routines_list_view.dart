import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_user_routines/get_user_routines_request.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';
import 'package:tfc_gym_app/presentation/screens/routine/routine_deltail_screen.dart';
import 'package:tfc_gym_app/presentation/widgets/pop_ups/delete_routine_pop_up.dart';

class UserRoutinesListView extends StatefulWidget {
  final String? email;
  const UserRoutinesListView({super.key, this.email});

  @override
  State<UserRoutinesListView> createState() => _UserRoutinesListViewState();
}

class _UserRoutinesListViewState extends State<UserRoutinesListView> {
  Future<List<RoutineDTO>>? _routinesFuture;

  @override
  void initState() {
    super.initState();
    _loadRoutines();
  }

  Future<void> _loadRoutines() async {
    setState(() {
      _routinesFuture = Provider.of<RoutineProvider>(context, listen: false)
          .getAllUserRoutines(GetUserRoutinesRequest(email: widget.email ?? ""));
    });
  }

  @override
  Widget build(BuildContext context) {
    return RefreshIndicator(
      onRefresh: _loadRoutines,
      child: FutureBuilder<List<RoutineDTO>>(
        future: _routinesFuture,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError || !snapshot.hasData) {
            return Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text('Error al cargar rutinas'),
                  const SizedBox(height: 16),
                  ElevatedButton(
                    onPressed: _loadRoutines,
                    child: const Text('Reintentar'),
                  ),
                ],
              ),
            );
          }

          final routines = snapshot.data!;

          return routines.isEmpty
              ? const Center(child: Text('No hay rutinas'))
              : ListView.builder(
                  itemCount: routines.length,
                  itemBuilder: (context, index) {
                    final routine = routines[index];
                    return ListTile(
                      leading: const Icon(Icons.fitness_center, color: Colors.deepPurple),
                      title: Text(routine.routineName ?? 'Sin nombre'),
                      subtitle: Text(routine.routineDescription ?? 'Sin descripción'),
                      onTap: () async {
                        await Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (_) => RoutineDetailScreen(routine: routine),
                          ),
                        );
                        _loadRoutines();
                      },
                      trailing: IconButton(
                        icon: const Icon(Icons.delete, color: Colors.red),
                        onPressed: () {
                          showDialog(
                            context: context,
                            builder: (_) => DeleteRoutinePopUp(
                              routine: routine,
                              onDeleted: _loadRoutines,
                            ),
                          );
                        },
                      ),
                    );
                  },
                );
        },
      ),
    );
  }
}
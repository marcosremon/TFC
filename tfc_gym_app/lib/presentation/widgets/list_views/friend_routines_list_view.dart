import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/routine_dto.dart';
import 'package:tfc_gym_app/data/models/dto/routine/get_user_routines/get_user_routines_request.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';
import 'package:tfc_gym_app/presentation/screens/routine/friend_routine_detail_screen.dart';

class FriendRoutinesListView extends StatefulWidget {
  final String? email;
  const FriendRoutinesListView({super.key, this.email});

  @override
  State<FriendRoutinesListView> createState() => _FriendRoutinesListViewState();
}

class _FriendRoutinesListViewState extends State<FriendRoutinesListView> {
  Future<List<RoutineDTO>>? _routinesFuture;

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _loadRoutines();
    });
  }

  Future<void> _loadRoutines() async {
    if (!mounted) return;
    
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
                      onTap: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (_) => FriendRoutineDetailScreen(routine: routine),
                          ),
                        ).then((_) {
                          if (mounted) {
                            _loadRoutines();
                          }
                        });
                      },
                    );
                  },
                );
        },
      ),
    );
  }
}
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/split_day/update_split_day_request.dart';
import 'package:tfc_gym_app/presentation/providers/split_day_provider.dart';

class EditSplitDaysBottomSheet extends StatefulWidget {
  final int routineId;
  final List<int> currentDays; // Ej: [0, 1] para lunes y martes

  const EditSplitDaysBottomSheet({
    super.key,
    required this.routineId,
    required this.currentDays,
  });

  @override
  State<EditSplitDaysBottomSheet> createState() => _EditSplitDaysBottomSheetState();
}

class _EditSplitDaysBottomSheetState extends State<EditSplitDaysBottomSheet> {
  final List<String> weekDays = [
    'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo'
  ];

  late List<int> toRemove;
  late List<int> toAdd;

  @override
  void initState() {
    super.initState();
    toRemove = [];
    toAdd = [];
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        left: 24, right: 24, top: 24,
        bottom: MediaQuery.of(context).viewInsets.bottom + 24,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          const Text('Editar días de la rutina', style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold)),
          const SizedBox(height: 18),
          Wrap(
            spacing: 8,
            runSpacing: 8,
            children: List.generate(7, (i) {
              final isInRoutine = widget.currentDays.contains(i);
              final isMarkedToRemove = toRemove.contains(i);
              final isMarkedToAdd = toAdd.contains(i);

              Color color;
              if (isInRoutine && isMarkedToRemove) {
                color = Colors.red;
              } else if (isInRoutine) {
                color = Colors.grey.shade400;
              } else if (isMarkedToAdd) {
                color = Colors.green;
              } else {
                color = Colors.grey.shade200;
              }

              return GestureDetector(
                onTap: () {
                  setState(() {
                    if (isInRoutine) {
                      if (isMarkedToRemove) {
                        toRemove.remove(i);
                      } else {
                        toRemove.add(i);
                      }
                    } else {
                      if (isMarkedToAdd) {
                        toAdd.remove(i);
                      } else {
                        toAdd.add(i);
                      }
                    }
                  });
                },
                child: AnimatedContainer(
                  duration: const Duration(milliseconds: 200),
                  padding: const EdgeInsets.symmetric(horizontal: 18, vertical: 12),
                  decoration: BoxDecoration(
                    color: color,
                    borderRadius: BorderRadius.circular(12),
                    border: Border.all(
                      color: isMarkedToAdd
                          ? Colors.green
                          : isMarkedToRemove
                              ? Colors.red
                              : Colors.grey,
                      width: 2,
                    ),
                  ),
                  child: Text(
                    weekDays[i],
                    style: TextStyle(
                      color: isMarkedToRemove
                          ? Colors.white
                          : isMarkedToAdd
                              ? Colors.white
                              : Colors.black87,
                      fontWeight: FontWeight.w600,
                    ),
                  ),
                ),
              );
            }),
          ),
          const SizedBox(height: 24),
          SizedBox(
            width: double.infinity,
            child: ElevatedButton(
              onPressed: () async {
                List<String> addDays = toAdd.map((i) => weekDays[i]).toList();
                List<String> deleteDays = toRemove.map((i) => weekDays[i]).toList();

                bool success = await context.read<SplitDayProvider>().updateSplitDay(
                  UpdateSplitDayRequest(
                    userEmail: "",
                    routineId: widget.routineId,
                    addDays: addDays,
                    deleteDays: deleteDays,
                  ),
                );

                if (success) {
                  Navigator.of(context).pop(true);
                }
              },
              child: const Text('Actualizar'),
            ),
          ),
        ],
      ),
    );
  }
}
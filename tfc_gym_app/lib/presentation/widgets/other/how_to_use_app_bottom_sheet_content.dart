import 'package:flutter/material.dart';
import 'package:tfc_gym_app/core/constants/tutorial_constants.dart';
import 'package:tfc_gym_app/presentation/widgets/other/tutorial_header.dart';
import 'package:tfc_gym_app/presentation/widgets/other/tutorial_step_item.dart';

class HowToUseAppBottomSheetContent extends StatelessWidget {
  final ScrollController scrollController;

  const HowToUseAppBottomSheetContent({
    super.key,
    required this.scrollController,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // Indicador de arrastre
        Container(
          width: 40,
          height: 4,
          margin: const EdgeInsets.only(top: 8, bottom: 8),
          decoration: BoxDecoration(
            color: Colors.grey[400],
            borderRadius: BorderRadius.circular(2),
          ),
        ),
        // Header fijo
        const TutorialHeader(),
        // Contenido desplazable
        Expanded(
          child: CustomScrollView(
            controller: scrollController,
            physics: const ClampingScrollPhysics(),
            slivers: [
              SliverList(
                delegate: SliverChildBuilderDelegate(
                  (context, index) => TutorialStepItem(
                        step: tutorialContent[index],
                      ),
                  childCount: tutorialContent.length,
                ),
              ),
            ],
          ),
        ),
      ],
    );
  }
}
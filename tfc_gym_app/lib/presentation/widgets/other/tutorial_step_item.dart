import 'package:flutter/material.dart';
import 'package:tfc_gym_app/data/models/dto/tutorial_step.dart';

class TutorialStepItem extends StatelessWidget {
  final TutorialStep step;
  const TutorialStepItem({Key? key, required this.step}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    double imageHeight = MediaQuery.of(context).size.width * 0.7; 

    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 4),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            step.title,
          ),
          const SizedBox(height: 4),
          Text(
            step.description,
          ),
          const SizedBox(height: 8),
          Align(
            alignment: Alignment.center,
            child: Container(
              width: double.infinity,
              height: imageHeight, 
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(8),
                image: DecorationImage(
                  image: AssetImage(step.imagePath),
                  fit: BoxFit.contain,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}

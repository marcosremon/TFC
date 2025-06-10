import 'package:flutter/material.dart';

class TutorialHeader extends StatelessWidget {
  const TutorialHeader({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(16),
      alignment: Alignment.center,
      child: Text(
        'Tutorial',
      ),
    );
  }
}
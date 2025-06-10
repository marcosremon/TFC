import 'package:flutter/material.dart';

class ConditionalBackButton extends StatelessWidget {
  final bool show;
  const ConditionalBackButton({super.key, required this.show});

  @override
  Widget build(BuildContext context) {
    return show
        ? IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () => Navigator.of(context).pop(),
          )
        : const SizedBox.shrink(); 
  }
}
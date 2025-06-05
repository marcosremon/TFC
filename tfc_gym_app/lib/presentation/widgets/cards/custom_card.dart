import 'package:flutter/material.dart';

class CustomCard extends StatelessWidget {
  final Widget child;

  const CustomCard({Key? key, required this.child}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: EdgeInsets.all(20),
      elevation: 8,
      child: Container(
        padding: EdgeInsets.all(24),
        width: 300,
        child: child,
      ),
    );
  }
}
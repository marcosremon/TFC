import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class CustomTextField extends StatelessWidget {
  final String labelText;
  final IconData prefixIcon;
  final bool obscureText;
  final TextEditingController controller;
  final TextInputType? keyboardType; // Parámetro opcional
  final List<TextInputFormatter>? inputFormatters; // Parámetro opcional

  const CustomTextField({
    Key? key,
    required this.labelText,
    required this.prefixIcon,
    this.obscureText = false,
    required this.controller,
    this.keyboardType, // Opcional - por defecto será null (teclado normal)
    this.inputFormatters, // Opcional - por defecto será null (sin filtros)
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      controller: controller,
      obscureText: obscureText,
      keyboardType: keyboardType, // Se pasa el keyboardType si fue proporcionado
      inputFormatters: inputFormatters, // Se pasan los formateadores si fueron proporcionados
      decoration: InputDecoration(
        labelText: labelText,
        prefixIcon: Icon(prefixIcon),
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(16),
        ),
      ),
    );
  }
}
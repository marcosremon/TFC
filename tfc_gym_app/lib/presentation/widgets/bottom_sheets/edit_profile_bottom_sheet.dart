import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/user_dto.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/custom_text_field.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';

class EditProfileBottomSheet extends StatefulWidget {
  final UserDTO user;
  const EditProfileBottomSheet({super.key, required this.user});

  @override
  State<EditProfileBottomSheet> createState() => _EditProfileBottomSheetState();
}

class _EditProfileBottomSheetState extends State<EditProfileBottomSheet> {
  late TextEditingController usernameController;
  late TextEditingController surnameController;
  late TextEditingController emailController;
  late TextEditingController dniController;

  @override
  void initState() {
    super.initState();
    usernameController = TextEditingController(text: widget.user.username ?? '');
    surnameController = TextEditingController(text: widget.user.surname ?? '');
    emailController = TextEditingController(text: widget.user.email ?? '');
    dniController = TextEditingController(text: widget.user.dni ?? '');
  }

  @override
  void dispose() {
    usernameController.dispose();
    surnameController.dispose();
    emailController.dispose();
    dniController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      padding: EdgeInsets.only(
        left: 24.0,
        right: 24.0,
        top: 24.0,
        bottom: MediaQuery.of(context).viewInsets.bottom + 24.0,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          const Text('Editar Perfil', style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold)),
          const SizedBox(height: 18),
          CustomTextField(
            controller: emailController,
            labelText: 'Correo electrónico',
            prefixIcon: Icons.email,
          ),
          const SizedBox(height: 18),
          CustomTextField(
            controller: usernameController,
            labelText: 'Nombre',
            prefixIcon: Icons.person,
          ),
          const SizedBox(height: 10),
          CustomTextField(
            controller: surnameController,
            labelText: 'Apellidos',
            prefixIcon: Icons.people,
          ),
          const SizedBox(height: 10),
          CustomTextField(
            controller: dniController,
            labelText: 'DNI',
            prefixIcon: Icons.badge,
          ),
          const SizedBox(height: 10),
          Row(
            children: [
              Expanded(
                child: CustomButton(
                  text: 'Cancelar',
                  isPrimary: false,
                  onPressed: () => Navigator.of(context).pop(),
                ),
              ),
              const SizedBox(width: 12),
              Expanded(
                child: CustomButton(
                  text: 'Guardar',
                  onPressed: () async {
                    await context.read<UserProvider>().updateUser(
                      widget.user.email!,
                      usernameController.text.trim(),
                      surnameController.text.trim(),
                      dniController.text.trim(),
                      emailController.text.trim(),
                    );
                    Navigator.of(context).pop();
                  },
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
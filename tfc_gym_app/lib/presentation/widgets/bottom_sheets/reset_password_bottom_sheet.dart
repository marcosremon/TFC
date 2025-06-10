import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/user/reset_password/reset_password_request.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/other/custom_text_field.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';

class ResetPasswordBottomSheet extends StatelessWidget {
  const ResetPasswordBottomSheet({super.key});

  @override
  Widget build(BuildContext context) {
    final resetEmailController = TextEditingController();

    return Padding(
      padding: const EdgeInsets.all(24.0),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          const Icon(Icons.lock_reset, color: Colors.blue, size: 48),
          const SizedBox(height: 12),
          const Text(
            '¿Olvidaste tu contraseña?',
            style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
            textAlign: TextAlign.center,
          ),
          const SizedBox(height: 10),
          const Text(
            'Introduce tu email y te enviaremos instrucciones para cambiar tu contraseña.',
            style: TextStyle(fontSize: 14, color: Colors.black87),
            textAlign: TextAlign.center,
          ),
          const SizedBox(height: 18),
          CustomTextField(
            controller: resetEmailController,
            labelText: 'Email',
            prefixIcon: Icons.email,
          ),
          const SizedBox(height: 22),
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
                  text: 'Enviar',
                  onPressed: () async {
                    String email = resetEmailController.text.trim();
                    if (email.isEmpty) {
                      ToastMsg.showToast('Por favor, ingresa tu correo electrónico');
                      return;
                    }
                    await context.read<UserProvider>().resetPassword(ResetPasswordRequest(email: email));
                    Navigator.of(context).pop();
                  },
                ),
              ),
            ],
          ),
          SizedBox(height: MediaQuery.of(context).viewInsets.bottom),
        ],
      ),
    );
  }
}
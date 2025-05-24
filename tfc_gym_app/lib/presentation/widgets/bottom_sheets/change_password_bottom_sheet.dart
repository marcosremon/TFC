import 'package:flutter/material.dart';
import 'package:modal_bottom_sheet/modal_bottom_sheet.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/custom_text_field.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';

class ChangePasswordBottomSheet {
  static void show(BuildContext context) {
    final emailController = TextEditingController();
    final oldPasswordController = TextEditingController();
    final newPasswordController = TextEditingController();
    final confirmPasswordController = TextEditingController();

    showMaterialModalBottomSheet(
      context: context,
      backgroundColor: Colors.white,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(top: Radius.circular(25.0)),
      ),
      builder: (context) => Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const Icon(Icons.lock, color: Colors.blue, size: 48),
            const SizedBox(height: 12),
            const Text(
              'Cambiar contraseña',
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 10),
            const Text(
              'Introduce tu email, la nueva contraseña y confírmala.',
              style: TextStyle(fontSize: 14, color: Colors.black87),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 18),
            CustomTextField(
              controller: emailController,
              labelText: 'Email',
              prefixIcon: Icons.email,
            ),
            const SizedBox(height: 12),
            CustomTextField(
              controller: oldPasswordController,
              labelText: 'Contraseña actual',
              prefixIcon: Icons.lock,
              obscureText: true,
            ),
            const SizedBox(height: 12),
            CustomTextField(
              controller: newPasswordController,
              labelText: 'Nueva contraseña',
              prefixIcon: Icons.lock_outline,
              obscureText: true,
            ),
            const SizedBox(height: 12),
            CustomTextField(
              controller: confirmPasswordController,
              labelText: 'Confirmar contraseña',
              prefixIcon: Icons.lock_outline,
              obscureText: true,
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
                      String email = emailController.text.trim();
                      String oldPassword = oldPasswordController.text.trim();
                      String newPassword = newPasswordController.text.trim();
                      String confirmPassword = confirmPasswordController.text.trim();
                      if (email.isEmpty || newPassword.isEmpty || confirmPassword.isEmpty || oldPassword.isEmpty) {
                        ToastMsg.showToast('Rellena todos los campos');
                        return;
                      }
                      await context.read<UserProvider>().changePasswordWithEmailAndPassword(
                        email, newPassword, confirmPassword, oldPassword
                      );
                      Navigator.of(context).pop();
                    },
                  ),
                ),
              ],
            ),
            SizedBox(height: MediaQuery.of(context).viewInsets.bottom),
          ],
        ),
      ),
    );
  }
}
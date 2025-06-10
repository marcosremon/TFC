import 'package:flutter/material.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/user/register_user/register_user_request.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/presentation/screens/auth/login_screen.dart';

class RegisterController extends ChangeNotifier {
  final dniController = TextEditingController();
  final usernameController = TextEditingController();
  final surnameController = TextEditingController();
  final emailController = TextEditingController();
  final passwordController = TextEditingController();
  final confirmPasswordController = TextEditingController();

  Future<bool> register(BuildContext context) async {
    return await context.read<UserProvider>().register(
      RegisterUserRequest(
        dni: dniController.text.trim(),
        username: usernameController.text.trim(),
        surname: surnameController.text.trim(),
        email: emailController.text.trim(),
        password: passwordController.text.trim(),
        passwordConfirm: confirmPasswordController.text.trim(),
      ),
    );
  }

  Future<void> onRegisterPressed(BuildContext context) async {
    if (dniController.text.trim().isEmpty ||
        usernameController.text.trim().isEmpty ||
        surnameController.text.trim().isEmpty ||
        emailController.text.trim().isEmpty ||
        passwordController.text.isEmpty ||
        confirmPasswordController.text.isEmpty) {
      ToastMsg.showToast('Rellena todos los campos');
      return;
    }
    if (passwordController.text != confirmPasswordController.text) {
      ToastMsg.showToast('Las contraseñas no coinciden');
      return;
    }

    bool isSuccess = await register(context);
    if (isSuccess && context.mounted) {
      dniController.clear();
      usernameController.clear();
      surnameController.clear();
      emailController.clear();
      passwordController.clear();
      confirmPasswordController.clear();

      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (_) => const LoginScreen()),
      );
    }
  }

  @override
  void dispose() {
    dniController.dispose();
    usernameController.dispose();
    surnameController.dispose();
    emailController.dispose();
    passwordController.dispose();
    confirmPasswordController.dispose();
    super.dispose();
  }
}
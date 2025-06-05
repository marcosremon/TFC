import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/auth/login/login_request.dart';
import 'package:tfc_gym_app/presentation/providers/auth_provider.dart';
import 'package:tfc_gym_app/presentation/screens/auth/home_screen.dart';

class LoginController extends ChangeNotifier {
  final emailController = TextEditingController();
  final passwordController = TextEditingController();

  Future<void> loginWithGoogle(BuildContext context) async {
    bool success = await context.read<AuthProvider>().loginWithGoogle();
    if (success && context.mounted) {
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (_) => const HomeScreen()),
      );
    }
  }

  Future<void> loginWithEmail(BuildContext context) async {
    bool isSuccess = await context.read<AuthProvider>().login(
      LoginRequest(
        email: emailController.text.trim(),
        password: passwordController.text,
      ),
    );
    if (isSuccess && context.mounted) {
      emailController.clear();
      passwordController.clear();
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(builder: (_) => const HomeScreen()),
      );
    }
  }

  @override
  void dispose() {
    emailController.dispose();
    passwordController.dispose();
    super.dispose();
  }
}
import 'package:flutter/material.dart';
import 'package:modal_bottom_sheet/modal_bottom_sheet.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/presentation/screens/home_screen.dart';
import 'package:tfc_gym_app/presentation/screens/register_screen.dart';
import 'package:tfc_gym_app/presentation/providers/auth_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/reset_password_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';
import 'package:tfc_gym_app/presentation/widgets/custom_card.dart';
import 'package:tfc_gym_app/presentation/widgets/custom_text_field.dart';

class LoginScreen extends StatelessWidget {
  const LoginScreen({super.key});

  @override
  Widget build(BuildContext context) {
    var emailController = TextEditingController();
    var passwordController = TextEditingController();

    return Scaffold(
      backgroundColor: const Color.fromARGB(128, 176, 190, 197),
      body: Center(
        child: SingleChildScrollView(
          child: CustomCard(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Text(
                  'Iniciar Sesión',
                  style: TextStyle(
                    fontSize: 24,
                    fontWeight: FontWeight.bold,
                    color: Colors.blue.shade800,
                  ),
                ),
                const SizedBox(height: 30),
                CustomTextField(
                  controller: emailController,
                  labelText: 'Correo electrónico',
                  prefixIcon: Icons.email,
                ),
                const SizedBox(height: 20),
                CustomTextField(
                  controller: passwordController,
                  labelText: 'Contraseña',
                  prefixIcon: Icons.lock,
                  obscureText: true,
                ),
                const SizedBox(height: 20),
                Row(
                  children: [
                    IconButton(
                      icon: Image.asset(
                        'assets/images/google_icon.png',
                        width: 30,
                        height: 30,
                      ),
                      onPressed: () async {
                        bool success = await context.read<AuthProvider>().loginWithGoogle();
                        if (success && context.mounted) {
                          Navigator.pushReplacement(
                            context,
                            MaterialPageRoute(builder: (_) => HomeScreen()),
                          );
                        }
                      },
                      style: IconButton.styleFrom(
                        backgroundColor: Colors.transparent,
                        padding: EdgeInsets.zero,
                        minimumSize: Size.zero,
                      ),
                    ),
                    const SizedBox(width: 8),
                    Expanded(
                      child: CustomButton(
                        text: 'Iniciar con Email',
                        onPressed: () async {
                          bool isSuccess = await context.read<AuthProvider>().login(
                            emailController.text.trim(),
                            passwordController.text,
                          );
                          if (isSuccess) {
                            Navigator.pushReplacement(
                              context,
                              MaterialPageRoute(builder: (_) => const HomeScreen()),
                            );
                          }
                        },
                      ),
                    ),
                  ],
                ),
                const SizedBox(height: 10),
                Row(
                  children: [
                    TextButton(
                      onPressed: () => Navigator.push(
                        context,
                        MaterialPageRoute(builder: (_) => const RegisterScreen()),
                      ),
                      child: Text(
                        '¿No tienes una cuenta?',
                        style: TextStyle(
                          color: Colors.blue.shade800,
                          decoration: TextDecoration.underline,
                        ),
                      ),
                    ),
                    const Spacer(),
                    IconButton(
                      icon: const Icon(Icons.lock_open, color: Colors.blue),
                      tooltip: '¿Olvidaste tu contraseña?',
                      onPressed: () {
                        showMaterialModalBottomSheet(
                          context: context,
                          backgroundColor: Colors.white,
                          shape: const RoundedRectangleBorder(
                            borderRadius: BorderRadius.vertical(top: Radius.circular(25.0)),
                          ),
                          builder: (context) => const ResetPasswordBottomSheet(),
                        );
                      },
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
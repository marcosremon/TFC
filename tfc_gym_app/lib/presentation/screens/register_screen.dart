import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/presentation/screens/login_screen.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/custom_button.dart';
import 'package:tfc_gym_app/presentation/widgets/custom_card.dart';
import 'package:tfc_gym_app/presentation/widgets/custom_text_field.dart';

class RegisterScreen extends StatelessWidget {
  const RegisterScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final dniController = TextEditingController();
    final usernameController = TextEditingController();
    final surnameController = TextEditingController();
    final emailController = TextEditingController();
    final passwordController = TextEditingController();
    final confirmPasswordController = TextEditingController();

    return Scaffold(
      backgroundColor: const Color.fromARGB(128, 176, 190, 197),
      body: Center(
        child: SingleChildScrollView(
          child: CustomCard(
            child: Padding(
              padding: const EdgeInsets.all(0),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Text(
                    'Registro',
                    style: TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                      color: Colors.blue.shade800,
                    ),
                  ),
                  const SizedBox(height: 18),
                  CustomTextField(
                    controller: dniController,
                    labelText: 'DNI',
                    prefixIcon: Icons.badge,
                  ),
                  const SizedBox(height: 10),
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
                    controller: emailController,
                    labelText: 'Correo electrónico',
                    prefixIcon: Icons.email,
                  ),
                  const SizedBox(height: 10),
                  CustomTextField(
                    controller: passwordController,
                    labelText: 'Contraseña',
                    prefixIcon: Icons.lock,
                    obscureText: true,
                  ),
                  const SizedBox(height: 10),
                  CustomTextField(
                    controller: confirmPasswordController,
                    labelText: 'Conf contraseña',
                    prefixIcon: Icons.lock_outline,
                    obscureText: true,
                  ),
                  const SizedBox(height: 15),
                  CustomButton(
                    text: 'Registrar',
                    onPressed: () async {
                      bool isSuccess = await context.read<UserProvider>().register(
                        dniController.text.trim(),
                        usernameController.text.trim(),
                        surnameController.text.trim(),
                        emailController.text.trim(),
                        passwordController.text.trim(),
                        confirmPasswordController.text.trim(),
                      );
                      if (isSuccess) {
                        Navigator.pushReplacement(
                          context,
                          MaterialPageRoute(builder: (_) => const LoginScreen()),
                        );
                      }
                    },
                  ),
                  const SizedBox(height: 0),
                  TextButton(
                    onPressed: () => Navigator.pushReplacement(
                      context,
                      MaterialPageRoute(builder: (_) => const LoginScreen()),
                    ),
                    child: Align(
                      alignment: Alignment.bottomRight,
                      child: Text(
                        '¿Volver a iniciar sesión?',
                        style: TextStyle(
                          color: Colors.blue.shade800,
                          decoration: TextDecoration.underline,
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/repositories/auth_repository.dart';
import 'package:tfc_gym_app/presentation/screens/login_screen.dart';

class AuthProvider extends ChangeNotifier {
  final AuthRepository _authRepository;
  bool _isLoggedIn = false;

  AuthProvider(this._authRepository);

  bool get isLoggedIn => _isLoggedIn;

  Future<void> initialize() async {
    try {
      var prefs = await SharedPreferences.getInstance();
      String? token = prefs.getString('token');
      _isLoggedIn = prefs.getBool('isLoggedIn') ?? false;

      if (_isLoggedIn && token != null && await _authRepository.isValidToken(token)) {
        _isLoggedIn = true;
      } else {
        _isLoggedIn = false;
        await prefs.remove('isLoggedIn');
        await prefs.remove('token');
        await prefs.remove('role');
        await prefs.remove('email');
        ToastMsg.showToast('Tu sesión ha expirado. Por favor, inicia sesión de nuevo.');
      }
    } catch (_) {
      ToastMsg.showToast('Error al verificar la sesión.');
      _isLoggedIn = false;
    } finally {
      notifyListeners();
    }
  }

  Future<bool> login(String email, String password) async {
    try {
      notifyListeners();
      var loginData = await _authRepository.login(email, password) as Map<String, dynamic>;
      if (loginData["isSuccess"] == true) {
        var prefs = await SharedPreferences.getInstance();
        String token = loginData["bearerToken"] as String;
        String role = loginData["isAdmin"] ? "admin" : "user";
        await prefs.setBool('isLoggedIn', true);
        await prefs.setString('token', token);
        await prefs.setString('email', email);
        await prefs.setString('role', role);
        _isLoggedIn = true;
        ToastMsg.showToast('Inicio de sesión exitoso');
      } else {
        ToastMsg.showToast('Credenciales incorrectas. Inténtalo de nuevo.');
        _isLoggedIn = false;
      }
      return _isLoggedIn;
    } catch (ex) {
      ToastMsg.showToast('Error al iniciar sesión. Inténtalo más tarde.');
      _isLoggedIn = false;
      return false;
    } finally {
      notifyListeners();
    }
  }

  Future<bool> loginWithGoogle() async {
    try {
      notifyListeners();
      var loginData = await _authRepository.loginWithGoogle() as Map<String, dynamic>;      
      if (loginData["isSuccess"] == true) {
        var user = loginData["user"];
        var prefs = await SharedPreferences.getInstance();
        String token = loginData["bearerToken"] as String;
        String email = user["email"] as String;
        String role = loginData["isAdmin"] ? "admin" : "user";
        
        await prefs.setBool('isLoggedIn', true);
        await prefs.setString('token', token);
        await prefs.setString('email', email);
        await prefs.setString('role', role);
        
        _isLoggedIn = true;
        ToastMsg.showToast('Inicio de sesión con Google exitoso');
        return true;
      } else {
        ToastMsg.showToast('Error al iniciar sesión con Google');
        _isLoggedIn = false;
        return false;
      }
    } catch (_) {
      ToastMsg.showToast('Error al iniciar sesión con Google.');
      _isLoggedIn = false;
      return false;
    } 
  }

  Future<void> logout(BuildContext context) async {
    try {
      var prefs = await SharedPreferences.getInstance();
      await prefs.remove('isLoggedIn');
      await prefs.remove('token');
      await prefs.remove('email');
      await prefs.remove('role');
      _isLoggedIn = false;
      ToastMsg.showToast('Sesión cerrada correctamente');
      notifyListeners();
      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (_) => const LoginScreen()),
        (route) => false,
      );
    } catch (_) {
      ToastMsg.showToast('Error al cerrar sesión.');
    }
  }
}
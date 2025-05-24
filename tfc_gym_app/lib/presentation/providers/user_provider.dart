import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/user_dto.dart';
import 'package:tfc_gym_app/data/repositories/user_repository.dart';

class UserProvider extends ChangeNotifier {
  final UserRepository _userRepository;
  UserDTO? _currentUser;
  SharedPreferences? _prefs;

  UserDTO? get currentUser => _currentUser;

  UserProvider({required UserRepository userRepository}) : _userRepository = userRepository {
    _initPrefs();
  }

  Future<void> _initPrefs() async {
    _prefs = await SharedPreferences.getInstance();
    notifyListeners();
  }

  Future<bool> register(String dni, String username, String surname, String email, String password, String passwordConfirm) async {
    try {
      notifyListeners();

      bool success = await _userRepository.register(dni, username, surname, email, password, passwordConfirm);
      if (success) {
        ToastMsg.showToast('Registro exitoso');
      } else {
        ToastMsg.showToast('Error en el registro');
      }

      return success;  
    } catch (_) {
      ToastMsg.showToast('No se pudo completar el registro. Inténtalo de nuevo más tarde.');
      return false;  
    } finally {
      notifyListeners();
    }
  }

  Future<UserDTO?> getUserByEmail(String? email) async {
    if (_prefs == null) {
      await _initPrefs();
    }
    if (email != null && email.isNotEmpty) {
      _currentUser = await _userRepository.getUserByEmail(email);
      notifyListeners();
      return _currentUser;
    }
    String? userEmail = _prefs?.getString('email');
    if (userEmail == null || userEmail.isEmpty) {
      ToastMsg.showToast('No hay email guardado');
      return null;
    }
    _currentUser = await _userRepository.getUserByEmail(userEmail);
    notifyListeners();
    return _currentUser;
  }

  Future<bool> deleteAccount() async {
    try {
      notifyListeners();

      if (_prefs == null) {
        await _initPrefs();
      }

      String? email = _prefs?.getString('email');
      if (email == null || email.isEmpty) {
        ToastMsg.showToast('No hay email guardado');
        return false;
      }

      bool success = await _userRepository.deleteAccount(email);
      if (success) {
        ToastMsg.showToast('Cuenta eliminada correctamente');
      } else {
        ToastMsg.showToast('No se pudo eliminar la cuenta.');
      }
      return success;
    } catch (_) {
      ToastMsg.showToast('Error al eliminar la cuenta. Inténtalo de nuevo más tarde.');
      return false;
    } finally {
      notifyListeners();
    }
  }

   Future<void> resetPassword(String email) async {
    try {
      notifyListeners();
      bool success = await _userRepository.resetPassword(email);
      if (success) {
        ToastMsg.showToast('se ha enviado un correo para restablecer la contraseña');
      } else {
        ToastMsg.showToast('Error al enviar el correo de restablecimiento');
      }
    } catch (_) {
      ToastMsg.showToast('Error al restablecer la contraseña. Inténtalo más tarde.');
    } finally {
      notifyListeners();
    }
  }

  Future<void> changePasswordWithEmailAndPassword(String email, String newPassword, String confirmPassword, String oldPassword) async {  
    try {
      notifyListeners();
      bool success = await _userRepository.changePasswordWithEmailAndPassword(email, newPassword, confirmPassword, oldPassword);
      if (success) {
        ToastMsg.showToast('Contraseña cambiada correctamente');
      } else {
        ToastMsg.showToast('Error al cambiar la contraseña');
      }
    } catch (_) {
      ToastMsg.showToast('Error al cambiar la contraseña. Inténtalo más tarde.');
    } finally {
      notifyListeners();
    }
  }

  Future<void> updateUser(String originalEmail, String username, String surname, String dni, String email) async {
    try {
      notifyListeners();
      bool success = await _userRepository.updateUser(originalEmail, username, surname, dni, email);
      if (success) {
        if (_prefs == null) {
          await _initPrefs();
        }
        await _prefs?.setString('email', email);
        await _prefs?.setString('username', username);
        await _prefs?.setString('surname', surname);
        await _prefs?.setString('dni', dni);

        await getUserByEmail(email);

        ToastMsg.showToast('Usuario actualizado correctamente');
      } else {
        ToastMsg.showToast('Error al actualizar el usuario');
      }
    } catch (_) {
      ToastMsg.showToast('Error al actualizar el usuario. Inténtalo más tarde.');
    } finally {
      notifyListeners();
    }
  }
}
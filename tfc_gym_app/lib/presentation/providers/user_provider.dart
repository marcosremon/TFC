import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/data/models/dto/user/change_password_with_email_and_password/change_password_with_email_and_password_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/delete_user/delete_user_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/get_user_by_email/get_user_by_email_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/register_user/register_user_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/reset_password/reset_password_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/update_user/update_user_request.dart';
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

  Future<bool> register(RegisterUserRequest registerUserRequest) async {
    try {
      notifyListeners();
      bool success = await _userRepository.register(registerUserRequest);
      return success;  
    } catch (_) {
      ToastMsg.showToast('No se pudo completar el registro. Inténtalo de nuevo más tarde.');
      return false;  
    } finally {
      notifyListeners();
    }
  }

  Future<UserDTO?> getUserByEmail(GetUserByEmailRequest getUserByEmailRequest) async {
    if (_prefs == null) {
      await _initPrefs();
    }
    
    if (getUserByEmailRequest.email.isNotEmpty) {
      _currentUser = await _userRepository.getUserByEmail(getUserByEmailRequest);
      notifyListeners();
      return _currentUser;
    }

    String userEmail = _prefs?.getString('email') ?? "";
    if (userEmail.isEmpty) {
      ToastMsg.showToast('No hay email guardado');
      return null;
    }

    _currentUser = await _userRepository.getUserByEmail(GetUserByEmailRequest(email: userEmail));
    notifyListeners();
    return _currentUser;
  }

  Future<bool> deleteAccount() async {
    try {
      notifyListeners();

      if (_prefs == null) {
        await _initPrefs();
      }

      String email = _prefs?.getString('email') ?? "";
      if (email.isEmpty) {
        ToastMsg.showToast('No hay email guardado');
        return false;
      }

      DeleteUserRequest deleteUserRequest = DeleteUserRequest(email: email);
      bool success = await _userRepository.deleteAccount(deleteUserRequest);
      if (success) {
        ToastMsg.showToast('Cuenta eliminada correctamente');
      }
      return success;
    } catch (_) {
      ToastMsg.showToast('Error al eliminar la cuenta. Inténtalo de nuevo más tarde.');
      return false;
    } finally {
      notifyListeners();
    }
  }

  Future<void> resetPassword(ResetPasswordRequest resetPasswordRequest) async {
    try {
      notifyListeners();
      bool success = await _userRepository.resetPassword(resetPasswordRequest);
      if (success) {
        ToastMsg.showToast('Se ha enviado un correo para restablecer la contraseña');
      }
    } catch (_) {
      ToastMsg.showToast('Error al restablecer la contraseña. Inténtalo más tarde.');
    } finally {
      notifyListeners();
    }
  }

  Future<void> changePasswordWithEmailAndPassword(ChangePasswordWithEmailAndPasswordRequest changePasswordWithEmailAndPasswordRequest) async {  
    try {
      notifyListeners();
      bool success = await _userRepository.changePasswordWithEmailAndPassword(changePasswordWithEmailAndPasswordRequest);
      if (success) {
        ToastMsg.showToast('Contraseña cambiada correctamente');
      }
    } catch (_) {
      ToastMsg.showToast('Error al cambiar la contraseña. Inténtalo más tarde.');
    } finally {
      notifyListeners();
    }
  }

  Future<void> updateUser(UpdateUserRequest updateUserRequest) async {
    try {
      notifyListeners();
      updateUserRequest.originalEmail =  _prefs?.getString('email') ?? "";
      bool success = await _userRepository.updateUser(updateUserRequest);
      if (success) {
        if (_prefs == null) {
          await _initPrefs();
        }
        
        await _prefs?.setString('email', updateUserRequest.email);
        await _prefs?.setString('username', updateUserRequest.username);
        await _prefs?.setString('surname', updateUserRequest.surname);
        await _prefs?.setString('dni', updateUserRequest.dni);

        await getUserByEmail(GetUserByEmailRequest(email: updateUserRequest.email));

        ToastMsg.showToast('Usuario actualizado correctamente');
      }
    } catch (_) {
      ToastMsg.showToast('Error al actualizar el usuario. Inténtalo más tarde.');
    } finally {
      notifyListeners();
    }
  }
}
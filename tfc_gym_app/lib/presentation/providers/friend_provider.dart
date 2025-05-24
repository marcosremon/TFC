import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/user_dto.dart';
import 'package:tfc_gym_app/data/repositories/friend_repository.dart';

class FriendProvider extends ChangeNotifier {
  final FriendRepository _friendRepository;
  SharedPreferences? _prefs;

  FriendProvider(this._friendRepository) {
    _initPrefs();
  }

  Future<void> _initPrefs() async {
    _prefs = await SharedPreferences.getInstance();
    notifyListeners();
  }

  Future<List<UserDTO>> getAllUserFriends() async {
    try {
      if (_prefs == null) {
        await _initPrefs();
      }
      var email = _prefs!.getString('email');
      if (email == null) {
        ToastMsg.showToast('No hay email guardado');
        return [];
      }
      return await _friendRepository.getAllUserFriends(email);
    } catch (_) {
      ToastMsg.showToast('No se pudo obtener la lista de amigos. Inténtalo de nuevo más tarde.');
      return [];
    }
  }

  Future<bool> addNewUserFriend(String friendCode) async {
    try {
      if (_prefs == null) {
        await _initPrefs();
      }
      var userEmail = _prefs!.getString('email');
      if (userEmail == null) {
        ToastMsg.showToast('No hay email guardado');
        return false;
      }
      var result = await _friendRepository.addNewUserFriend(userEmail, friendCode);
      if (result) {
        ToastMsg.showToast('Amigo agregado exitosamente');
      } else {
        ToastMsg.showToast('No se pudo agregar el amigo.');
      }
      return result;
    } catch (_) {
      ToastMsg.showToast('No se pudo agregar el amigo. Inténtalo de nuevo más tarde.');
      return false;
    }
  }

  Future<bool> deleteUserFriend(String friendEmail) async {
    try {
      if (_prefs == null) {
        await _initPrefs();
      }

      var userEmail = _prefs!.getString('email');
      if (userEmail == null) {
        ToastMsg.showToast('No hay email guardado');
        return false;
      }
      
      bool result = await _friendRepository.deleteUserFriend(userEmail, friendEmail);
      if (result) {
        ToastMsg.showToast('Amigo eliminado exitosamente');
      } else {
        ToastMsg.showToast('No se pudo eliminar el amigo.');
      }

      return result;
    } catch (_) {
      ToastMsg.showToast('No se pudo eliminar el amigo. Inténtalo de nuevo más tarde.');
      return false;
    }
  }
}
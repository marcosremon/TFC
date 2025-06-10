import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/data/models/dto/friend/add_new_user_friend/add_new_user_friend_request.dart';
import 'package:tfc_gym_app/data/models/dto/friend/delete_user_friend/delete_user_friend_request.dart';
import 'package:tfc_gym_app/data/models/dto/friend/get_all_user_friends/get_all_user_friends_request.dart';
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
      String email = _prefs!.getString('email') ?? "";
      if (email.isEmpty) {
        ToastMsg.showToast('No hay email guardado');
        return [];
      }
      return await _friendRepository.getAllUserFriends(GetAllUserFriendsRequest(email: email));
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener amigos.');
      return [];
    }
  }

  Future<bool> addNewUserFriend(AddNewUserFriendRequest addNewUserFriendRequest) async {
    try {
      if (_prefs == null) {
        await _initPrefs();
      }
      String userEmail = _prefs!.getString('email') ?? "";
      if (userEmail.isEmpty) {
        ToastMsg.showToast('No hay email guardado');
        return false;
      }
      addNewUserFriendRequest.email = userEmail;
      var result = await _friendRepository.addNewUserFriend(addNewUserFriendRequest);
      return result;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al agregar amigo.');
      return false;
    }
  }

  Future<bool> deleteUserFriend(DeleteUserFriendRequest deleteUserFriendRequest) async {
    try {
      if (_prefs == null) {
        await _initPrefs();
      }
      String userEmail = _prefs!.getString('email') ?? "";
      if (userEmail.isEmpty) {
        ToastMsg.showToast('No hay email guardado');
        return false;
      }
      deleteUserFriendRequest.userEmail = userEmail;
      bool result = await _friendRepository.deleteUserFriend(deleteUserFriendRequest);
      return result;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al eliminar amigo.');
      return false;
    }
  }
}
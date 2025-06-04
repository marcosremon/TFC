import 'package:flutter/material.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/data/models/dto/friend/add_new_user_friend/add_new_user_friend_request.dart';
import 'package:tfc_gym_app/data/models/dto/friend/delete_user_friend/delete_user_friend_request.dart';
import 'package:tfc_gym_app/presentation/providers/friend_provider.dart';
import 'package:provider/provider.dart';

class FriendController extends ChangeNotifier {
  final TextEditingController searchController = TextEditingController();
  late Future<List<UserDTO>> friendsFuture;

  Future<void> loadFriends(BuildContext context) async {
    friendsFuture = Provider.of<FriendProvider>(context, listen: false).getAllUserFriends();
    await friendsFuture; 
    notifyListeners();
  }

  Future<void> addFriend(BuildContext context) async {
    var friendCode = searchController.text.trim();
    if (friendCode.isNotEmpty) {
      await Provider.of<FriendProvider>(context, listen: false)
          .addNewUserFriend(AddNewUserFriendRequest(email: "", friendCode: friendCode));
      searchController.clear();
      await loadFriends(context);
    }
  }

  Future<void> deleteFriend(BuildContext context, String friendEmail) async {
    bool deleted = await Provider.of<FriendProvider>(context, listen: false)
        .deleteUserFriend(DeleteUserFriendRequest(friendEmail: friendEmail, userEmail: ''));
    if (deleted) {
      await loadFriends(context);
    }
  }

  @override
  void dispose() {
    searchController.dispose();
    super.dispose();
  }
}
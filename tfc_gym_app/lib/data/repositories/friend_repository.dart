import 'package:tfc_gym_app/data/datasources/friend_datasources.dart';
import 'package:tfc_gym_app/data/models/dto/user_dto.dart';

class FriendRepository {
  final FriendDatasource _datasource;

  FriendRepository(FriendDatasource datasource) : _datasource = datasource;
 
  Future<List<UserDTO>> getAllUserFriends(String email) async {
    return await _datasource.getAllUserFriends(email);
  }

  Future<bool> addNewUserFriend(String userEmail, String friendCode) async {
    return await _datasource.addNewUserFriend(userEmail, friendCode);
  }

  Future<bool> deleteUserFriend(String userEmail, String friendEmail) async {
    return await _datasource.deleteUserFriend(userEmail, friendEmail);
  }
}
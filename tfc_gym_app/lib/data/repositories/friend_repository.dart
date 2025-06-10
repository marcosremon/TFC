import 'package:tfc_gym_app/data/datasources/friend_datasources.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/data/models/dto/friend/add_new_user_friend/add_new_user_friend_request.dart';
import 'package:tfc_gym_app/data/models/dto/friend/delete_user_friend/delete_user_friend_request.dart';
import 'package:tfc_gym_app/data/models/dto/friend/get_all_user_friends/get_all_user_friends_request.dart';

class FriendRepository {
  final FriendDatasource _datasource;

  FriendRepository(FriendDatasource datasource) : _datasource = datasource;
 
  Future<List<UserDTO>> getAllUserFriends(GetAllUserFriendsRequest getAllUserFriendsRequest) async {
    return await _datasource.getAllUserFriends(getAllUserFriendsRequest);
  }

  Future<bool> addNewUserFriend(AddNewUserFriendRequest addNewUserFriendRequest) async {
    return await _datasource.addNewUserFriend(addNewUserFriendRequest);
  }

  Future<bool> deleteUserFriend(DeleteUserFriendRequest deleteUserFriendRequest) async {
    return await _datasource.deleteUserFriend(deleteUserFriendRequest);
  }
}
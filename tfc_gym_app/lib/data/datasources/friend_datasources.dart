import 'package:dio/dio.dart';
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/data/models/dto/friend/add_new_user_friend/add_new_user_friend_request.dart';
import 'package:tfc_gym_app/data/models/dto/friend/delete_user_friend/delete_user_friend_request.dart';
import 'package:tfc_gym_app/data/models/dto/friend/get_all_user_friends/get_all_user_friends_request.dart';

class FriendDatasource {
  final Dio _dio;

  FriendDatasource() : _dio = Dio() {
    _dio.options.baseUrl = ApiConstants.baseUrl;
    _dio.options.headers = {
      'Content-Type': 'application/json',
    };
    _dio.options.validateStatus = (status) => status! < 500;
  }

  Future<List<UserDTO>> getAllUserFriends(GetAllUserFriendsRequest getAllUserFriendsRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.friendEndpoint}/get-all-user-friends',
        data: {
          'UserEmail': getAllUserFriendsRequest.email
        },
      );

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;      
      if (!responseData['isSuccess']) {
        ToastMsg.showToast(responseData['message'] ?? 'Error al obtener amigos');
        return [];
      }

      return (responseData['friends'] as List)
          .map((json) => UserDTO.fromJson(json))
          .toList();
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo obtener la lista de amigos. Inténtalo de nuevo más tarde.');
      return [];
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener amigos.');
      return [];
    }
  }

  Future<bool> addNewUserFriend(AddNewUserFriendRequest addNewUserFriendRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.friendEndpoint}/add-new-user-friend',
        data: {
          'UserEmail': addNewUserFriendRequest.email,
          'FriendCode': addNewUserFriendRequest.friendCode,
        },
      );

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      if (!responseData['isSuccess']) {
        ToastMsg.showToast(responseData['message'] ?? 'Error al agregar amigo');
        return false;
      }

      ToastMsg.showToast(responseData['message'] ?? 'Amigo agregado correctamente');
      return true;
   } on DioException catch (_) {
      ToastMsg.showToast('No se pudo agregar el amigo. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al agregar amigo.');
      return false;
    }
  }

  Future<bool> deleteUserFriend(DeleteUserFriendRequest deleteUserFriendRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.friendEndpoint}/delete-friend',
        data: {
          'UserEmail': deleteUserFriendRequest.userEmail,
          'FriendEmail': deleteUserFriendRequest.friendEmail,
        },
      );

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      if (!responseData['isSuccess']) {
        ToastMsg.showToast(responseData['message'] ?? 'Error al eliminar amigo');
        return false;
      }

      ToastMsg.showToast(responseData['message'] ?? 'Amigo eliminado correctamente');
      return true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo eliminar el amigo. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al eliminar amigo.');
      return false;
    }
  }
}
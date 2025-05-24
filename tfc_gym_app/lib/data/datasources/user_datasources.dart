import 'package:dio/dio.dart';
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/user_dto.dart';

class UserDatasource {
  final Dio _dio;
  
  UserDatasource() : _dio = Dio() {
    _dio.options.baseUrl = ApiConstants.baseUrl;
    _dio.options.headers = {
      'Content-Type': 'application/json', 
    };
    _dio.options.validateStatus = (status) => status! < 500;
  }

  Future<bool> register(String dni, String username, String surname, String email, String password, String passwordConfirm) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/create-user', 
        data: {
          'Dni': dni,
          'Username': username,
          'Surname': surname,
          'Email': email,
          'Password': password,
          'ConfirmPassword': passwordConfirm,
        },
      );

      if (response.statusCode! >= 200 && response.statusCode! < 300) {
        var responseData = response.data as Map<String, dynamic>;
        return responseData['isSuccess'] as bool;
      } 
      
      var errorMessage = response.data is String 
          ? response.data 
          : response.data['message'] ?? 'Error desconocido';
      ToastMsg.showToast(errorMessage);
      return false;
     } on DioException catch (_) {
      ToastMsg.showToast('No se pudo registrar el usuario. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al registrar usuario.');
      return false;
    }
  }

  Future<UserDTO?> getUserByEmail(String email) async {
    try {
      // email = "amanea@iesch.org";
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/get-user-by-email',
        data: {'Email': email},
      );

      if (response.statusCode! >= 200 && response.statusCode! < 300) {
        var responseData = response.data as Map<String, dynamic>;
        if (responseData.containsKey('userDTO') && responseData['userDTO'] != null) {
          var userMap = Map<String, dynamic>.from(responseData['userDTO']);
          if (responseData.containsKey('routinesCount')) {
            userMap['routinesCount'] = responseData['routinesCount'];
          }
          if (responseData.containsKey('friendsCount')) {
            userMap['friendsCount'] = responseData['friendsCount'];
          }
          return UserDTO.fromJson(userMap);
        }
      }

      var errorMessage = response.data is String
          ? response.data
          : response.data['message'] ?? 'Error desconocido';
      ToastMsg.showToast(errorMessage);
      return null;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo obtener el usuario. Inténtalo de nuevo más tarde.');
      return null;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener usuario.');
      return null;
    }
  }

  Future<bool> deleteAccount(String email) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/delete-user',
        data: {'Email': email},
      );

      if (response.statusCode! >= 200 && response.statusCode! < 300) {
        var responseData = response.data as Map<String, dynamic>;
        if (responseData['isSuccess'] == true) {
          ToastMsg.showToast('Cuenta eliminada correctamente');
          return true;
        } else {
          ToastMsg.showToast(responseData['message'] ?? 'No se pudo eliminar la cuenta.');
          return false;
        }
      }

      var errorMessage = response.data is String
          ? response.data
          : response.data['message'] ?? 'Error desconocido';
      ToastMsg.showToast(errorMessage);
      return false;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo eliminar la cuenta. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al eliminar la cuenta.');
      return false;
    }
  }

   Future<bool> resetPassword(String email) async {
    try {
      final response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/create-new-password',
        data: {
          'UserEmail': email
        },
      );
      return response.data['isSuccess'] == true;
    } catch (_) {
      ToastMsg.showToast('No se pudo restablecer la contraseña.');
      return false;
    }
  }

  Future<bool> changePasswordWithEmailAndPassword(
    String email,
    String newPassword,
    String confirmPassword,
    String oldPassword,
  ) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/change-password-with-password-and-email',
        data: {
          'UserEmail': email,
          'NewPassword': newPassword,
          'ConfirmNewPassword': confirmPassword,
          'OldPassword': oldPassword,
        },
      );
      return response.data['isSuccess'] == true;
    } catch (_) {
      ToastMsg.showToast('No se pudo cambiar la contraseña.');
      return false;
    }
  }

  Future<bool> registerUserWithGoogle(String dni, String username, String surname, String email, String password, String passwordConfirm) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/create-google-user', 
        data: {
          'Dni': dni,
          'Username': username,
          'Surname': surname,
          'Email': email,
          'Password': password,
          'ConfirmPassword': passwordConfirm,
        },
      );

      if (response.statusCode! >= 200 && response.statusCode! < 300) {
        var responseData = response.data as Map<String, dynamic>;
        return responseData['isSuccess'] as bool;
      } 
      
      var errorMessage = response.data is String 
          ? response.data 
          : response.data['message'] ?? 'Error desconocido';
      ToastMsg.showToast(errorMessage);
      return false;
     } on DioException catch (_) {
      ToastMsg.showToast('No se pudo registrar el usuario. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al registrar usuario.');
      return false;
    }
  }

  Future<bool> updateUser(String originalEmail, String username, String surname, String dni, String email) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/update-user',
        data: {
          'OriginalEmail': originalEmail,
          'Username': username,
          'Surname': surname,
          'DniToBeFound': dni,
          'Email': email,
        },
      );

      if (response.statusCode! >= 200 && response.statusCode! < 300) {
        var responseData = response.data as Map<String, dynamic>;
        return responseData['isSuccess'] as bool;
      } 
      
      var errorMessage = response.data is String 
          ? response.data 
          : response.data['message'] ?? 'Error desconocido';
      ToastMsg.showToast(errorMessage);
      return false;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo actualizar el usuario. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al actualizar usuario.');
      return false;
    }
  }
}
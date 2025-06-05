import 'package:dio/dio.dart';
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/data/models/dto/user/change_password_with_email_and_password/change_password_with_email_and_password_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/delete_user/delete_user_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/get_user_by_email/get_user_by_email_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/register_user/register_user_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/reset_password/reset_password_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/update_user/update_user_request.dart';

class UserDatasource {
  final Dio _dio;
  
  UserDatasource() : _dio = Dio() {
    _dio.options.baseUrl = ApiConstants.baseUrl;
    _dio.options.headers = {
      'Content-Type': 'application/json', 
    };
    _dio.options.validateStatus = (status) => status! < 500;
  }

  Future<bool> register(RegisterUserRequest registerUserRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/create-user', 
        data: {
          'Dni': registerUserRequest.dni,
          'Username': registerUserRequest.username,
          'Surname': registerUserRequest.surname,
          'Email': registerUserRequest.email,
          'Password': registerUserRequest.password,
          'ConfirmPassword': registerUserRequest.passwordConfirm,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'Registro realizado');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo registrar el usuario. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al registrar usuario.');
      return false;
    }
  }

  Future<UserDTO?> getUserByEmail(GetUserByEmailRequest getUserByEmailRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/get-user-by-email',
        data: {
          'Email': getUserByEmailRequest.email
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return null;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      if (responseData.containsKey('userDTO') && responseData['userDTO'] != null) {
        Map<String, dynamic> userMap = Map<String, dynamic>.from(responseData['userDTO']);
        if (responseData.containsKey('routinesCount')) {
          userMap['routinesCount'] = responseData['routinesCount'];
        }
        if (responseData.containsKey('friendsCount')) {
          userMap['friendsCount'] = responseData['friendsCount'];
        }
        return UserDTO.fromJson(userMap);
      }
      ToastMsg.showToast(responseData['message'] ?? 'No se encontró el usuario');
      return null;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo obtener el usuario. Inténtalo de nuevo más tarde.');
      return null;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al obtener usuario.');
      return null;
    }
  }

  Future<bool> deleteAccount(DeleteUserRequest deleteUserRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/delete-user',
        data: {
          'Email': deleteUserRequest.email
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'No se pudo eliminar la cuenta.');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo eliminar la cuenta. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al eliminar la cuenta.');
      return false;
    }
  }

  Future<bool> resetPassword(ResetPasswordRequest resetPasswordRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/create-new-password',
        data: {
          'UserEmail': resetPasswordRequest.email
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'No se pudo restablecer la contraseña.');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo restablecer la contraseña. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al restablecer la contraseña.');
      return false;
    }
  }

  Future<bool> changePasswordWithEmailAndPassword(ChangePasswordWithEmailAndPasswordRequest changePasswordWithEmailAndPasswordRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/change-password-with-password-and-email',
        data: {
          'UserEmail': changePasswordWithEmailAndPasswordRequest.email,
          'NewPassword': changePasswordWithEmailAndPasswordRequest.newPassword,
          'ConfirmNewPassword': changePasswordWithEmailAndPasswordRequest.confirmPassword,
          'OldPassword': changePasswordWithEmailAndPasswordRequest.oldPassword,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'No se pudo cambiar la contraseña.');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo cambiar la contraseña. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al cambiar la contraseña.');
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

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'Registro realizado');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo registrar el usuario. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al registrar usuario.');
      return false;
    }
  }

  Future<bool> updateUser(UpdateUserRequest updateUserRequest) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.usersEndpoint}/update-user',
        data: {
          'OriginalEmail': updateUserRequest.originalEmail,
          'Username': updateUserRequest.username,
          'Surname': updateUserRequest.surname,
          'DniToBeFound': updateUserRequest.dni,
          'Email': updateUserRequest.email,
        },
      );

      if (response.data is String) {
        ToastMsg.showToast(response.data);
        return false;
      }

      Map<String, dynamic> responseData = response.data as Map<String, dynamic>;
      ToastMsg.showToast(responseData['message'] ?? 'No se pudo actualizar el usuario.');
      return responseData['isSuccess'] == true;
    } on DioException catch (_) {
      ToastMsg.showToast('No se pudo actualizar el usuario. Inténtalo de nuevo más tarde.');
      return false;
    } catch (_) {
      ToastMsg.showToast('Error inesperado al actualizar usuario.');
      return false;
    }
  }
}
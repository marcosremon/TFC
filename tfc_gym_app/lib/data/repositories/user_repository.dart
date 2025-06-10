import 'package:tfc_gym_app/data/datasources/user_datasources.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/data/models/dto/user/change_password_with_email_and_password/change_password_with_email_and_password_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/delete_user/delete_user_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/get_user_by_email/get_user_by_email_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/register_user/register_user_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/reset_password/reset_password_request.dart';
import 'package:tfc_gym_app/data/models/dto/user/update_user/update_user_request.dart';

class UserRepository {
  final UserDatasource _datasource;

  UserRepository({required UserDatasource datasource, required UserDatasource userDatasource})
  : _datasource = datasource;

  Future<bool> register(RegisterUserRequest registerUserRequest) async {
    return await _datasource.register(registerUserRequest);
  }

  Future<UserDTO?> getUserByEmail(GetUserByEmailRequest getUserByEmailRequest) async {
    return await _datasource.getUserByEmail(getUserByEmailRequest);
  }

  Future<bool> deleteAccount(DeleteUserRequest deleteUserRequest) async {
    return await _datasource.deleteAccount(deleteUserRequest);
  }

  Future<bool> resetPassword(ResetPasswordRequest resetPasswordRequest) async {
    return await _datasource.resetPassword(resetPasswordRequest);
  }

  Future<bool> changePasswordWithEmailAndPassword(ChangePasswordWithEmailAndPasswordRequest changePasswordWithEmailAndPasswordRequest) async {  
      return await _datasource.changePasswordWithEmailAndPassword(changePasswordWithEmailAndPasswordRequest);
  }

  Future<bool> updateUser(UpdateUserRequest updateUserRequest) async {
    return await _datasource.updateUser(updateUserRequest);
  }
}
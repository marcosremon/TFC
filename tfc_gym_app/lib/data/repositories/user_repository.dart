import 'package:tfc_gym_app/data/datasources/user_datasources.dart';
import 'package:tfc_gym_app/data/models/dto/user_dto.dart';

class UserRepository {
  final UserDatasource _datasource;

  UserRepository({required UserDatasource datasource, required UserDatasource userDatasource})
  : _datasource = datasource;

  Future<bool> register(String dni, String username, String surname, String email, String password, String passwordConfirm) async {
    return await _datasource.register(
      dni,
      username,
      surname,
      email,
      password,
      passwordConfirm,
    );
  }

  Future<UserDTO?> getUserByEmail(String email) async {
    return await _datasource.getUserByEmail(email);
  }

  Future<bool> deleteAccount(String email) async {
    return await _datasource.deleteAccount(email);
  }

  Future<bool> resetPassword(String email) async {
    return await _datasource.resetPassword(email);
  }

  Future<bool> changePasswordWithEmailAndPassword(String email, String newPassword, String confirmPassword, String oldPassword) async {  
      return await _datasource.changePasswordWithEmailAndPassword(email, oldPassword, newPassword, confirmPassword);
  }

  Future<bool> updateUser(String originalEmail, String username, String surname, String dni, String email) async {
    return await _datasource.updateUser(originalEmail, username, surname, dni, email);
  }
}
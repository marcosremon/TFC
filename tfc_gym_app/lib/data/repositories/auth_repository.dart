import 'package:tfc_gym_app/data/datasources/auth_datasources.dart';
import 'package:tfc_gym_app/data/datasources/user_datasources.dart';

class AuthRepository {
  final AuthDatasource _datasource;

  AuthRepository(this._datasource, {required AuthDatasource authDatasource, required UserDatasource userDatasource});

  Future<Object> login(String email, String password) async {
    return await _datasource.login(email, password);
  }

  Future<Object> loginWithGoogle() async {
    return await _datasource.signInWithGoogle();
  }

  Future<bool> isValidToken(String token) async {
    return await _datasource.isValidToken(token);
  }
}
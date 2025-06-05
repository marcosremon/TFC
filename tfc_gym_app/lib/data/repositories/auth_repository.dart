import 'package:tfc_gym_app/data/datasources/auth_datasources.dart';
import 'package:tfc_gym_app/data/datasources/user_datasources.dart';
import 'package:tfc_gym_app/data/models/dto/auth/is_valid_token/is_valid_token_request.dart';
import 'package:tfc_gym_app/data/models/dto/auth/login/login_request.dart';

class AuthRepository {
  final AuthDatasource _datasource;

  AuthRepository(this._datasource, {required AuthDatasource authDatasource, required UserDatasource userDatasource});

  Future<Object> login(LoginRequest loginRequet) async {
    return await _datasource.login(loginRequet);
  }

  Future<Object> loginWithGoogle() async {
    return await _datasource.signInWithGoogle();
  }

  Future<bool> isValidToken(IsValidTokenRequest isValidTokenRequest) async {
    return await _datasource.isValidToken(isValidTokenRequest);
  }
}
import 'package:dio/dio.dart';
import 'package:google_sign_in/google_sign_in.dart';
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:tfc_gym_app/data/datasources/user_datasources.dart';

class AuthDatasource {
   final Dio _dio;
  final GoogleSignIn _googleSignIn;
  final UserDatasource _userDatasource;
  
  AuthDatasource({UserDatasource? userDatasource}) : 
    _dio = Dio(), 
    _googleSignIn = GoogleSignIn(),
    _userDatasource = userDatasource ?? UserDatasource() {
      _dio.options.baseUrl = ApiConstants.baseUrl;
      _dio.options.headers = {
        'Content-Type': 'application/json', 
      };
      _dio.options.validateStatus = (status) => status! < 500;
  }
  
  Future<Object> login(String email, String password, {bool showToast = true}) async {
    try {
      var response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.authEndpoint}/login', 
        data: {
          'UserEmail': email, 
          'UserPassword': password
        }, 
      );

      if (response.statusCode == 200) {
        return response.data as Map<String, dynamic>;
      }
        
      var errorMessage = response.data is String 
          ? response.data 
          : response.data['message'] ?? 'Error desconocido';
      if (showToast) {
        ToastMsg.showToast(errorMessage);
      }
      return false;
    } on DioException catch (_) {
      if (showToast) ToastMsg.showToast('No se pudo iniciar sesión. Inténtalo de nuevo más tarde.');
      return false; 
    } catch (_) {
      if (showToast) ToastMsg.showToast('Error inesperado al iniciar sesión.');
      return false;
    }
  }

  Future<Object> signInWithGoogle() async {
    try {
      await _googleSignIn.signOut(); 
      final GoogleSignInAccount? googleUser = await _googleSignIn.signIn();
      
      if (googleUser == null) {
        ToastMsg.showToast('No se seleccionó ninguna cuenta de Google.');
        return false;
      }

      String dni = "";
      String displayName = googleUser.displayName ?? 'Usuario Google';
      String surname = "";
      String email = googleUser.email; 
      String password = googleUser.email;
      String confirmPassword = googleUser.email;

      bool userCreated = false;
      var loginResult = await login(email, password, showToast: false); 

      if (loginResult is bool) {
        await _userDatasource.registerUserWithGoogle(
          dni, displayName, surname, email, password, confirmPassword
        );
        userCreated = true;
        loginResult = await login(email, password, showToast: false) as Map<String, dynamic>;
      }

      if (loginResult is Map<String, dynamic> && loginResult["isSuccess"] == true) {
        var user = await _userDatasource.getUserByEmail(email);
        if (user != null) {
          if (userCreated) {
            ToastMsg.showToast(
              'Se te ha enviado un correo a tu Gmail. Por su seguridad, siga las instrucciones.'
            );
          }

          loginResult["user"] = user.toJson(); 
          return loginResult;
        }
        return loginResult;
      }

      return loginResult;
    } catch (_) {
      ToastMsg.showToast('No se pudo iniciar sesión con Google.');
      return false;
    }
  }

  Future<bool> isValidToken(String token) async {
    try {
      final response = await _dio.post(
        '${ApiConstants.baseUrl}${ApiConstants.authEndpoint}/check-token-status',
        data: {
          'token': token
        },
      );
      return response.data['isValid'] == true;
    } catch (_) {
      ToastMsg.showToast('No se pudo validar la sesión.');
      return false;
    }
  }
}
import 'package:dio/dio.dart';

class DioErrorHandler {
  static String handleDioError(DioException e) {
    if (e.type == DioExceptionType.connectionTimeout ||
        e.type == DioExceptionType.receiveTimeout ||
        e.type == DioExceptionType.sendTimeout) {
      return 'Tiempo de espera agotado. Verifica tu conexión a internet.';
    }

    if (e.type == DioExceptionType.badResponse) {
      return handleStatusCodeError(e.response?.statusCode);
    }
    
    if (e.type == DioExceptionType.cancel) {
      return 'La solicitud fue cancelada.';
    }
    return 'Error de red. Inténtalo de nuevo más tarde.';
  }

  static String handleStatusCodeError(int? statusCode) {
    switch (statusCode) {
      case 400: return 'Solicitud incorrecta.';
      case 401: return 'No autorizado.';
      case 403: return 'Acceso denegado.';
      case 404: return 'Recurso no encontrado.';
      case 409: return 'Conflicto de datos.';
      case 422: return 'Error de validación.';
      case 429: return 'Demasiadas solicitudes.';
      case 500: return 'Error interno del servidor.';
      case 502: return 'Error de puerta de enlace.';
      case 503: return 'Servicio no disponible.';
      case 504: return 'Tiempo de espera agotado.';
      default: return 'Error de comunicación con el servidor.';
    }
  }
}
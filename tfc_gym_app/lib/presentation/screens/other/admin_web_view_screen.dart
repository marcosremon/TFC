import 'dart:io';
import 'package:flutter/services.dart' show rootBundle;
import 'package:tfc_gym_app/core/constants/api_constants.dart';
import 'package:tfc_gym_app/core/utils/toast_msg.dart';
import 'package:url_launcher/url_launcher.dart';

class AdminWebViewScreen {
  static HttpServer? _server;
  static bool _isServerRunning = false;

  static Future<void> openAdminWeb() async {
    if (_isServerRunning) {
      _launchBrowser();
      return;
    }

    try {
      _server = await HttpServer.bind(InternetAddress.anyIPv4, 8080);
      _isServerRunning = true;

      _server?.listen((request) async {
        try {
          final html = await rootBundle.loadString('assets/html/index.html');
          request.response
            ..headers.contentType = ContentType.html
            ..write(html);
        } catch (_) {
          request.response.statusCode = 500;
        } finally {
          await request.response.close();
        }
      });

      _launchBrowser();
    } catch (_) {
      ToastMsg.showToast("no se pudo mostrar la web");
    }
  }

  static Future<void> _launchBrowser() async {
    final url = Uri.parse('http://${ApiConstants.ip}:8000/web');
    await launchUrl(url, mode: LaunchMode.externalApplication);
  }
}
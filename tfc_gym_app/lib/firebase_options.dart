// File generated by FlutterFire CLI.
// ignore_for_file: type=lint
import 'package:firebase_core/firebase_core.dart' show FirebaseOptions;
import 'package:flutter/foundation.dart'
    show defaultTargetPlatform, kIsWeb, TargetPlatform;

/// Default [FirebaseOptions] for use with your Firebase apps.
///
/// Example:
/// ```dart
/// import 'firebase_options.dart';
/// // ...
/// await Firebase.initializeApp(
///   options: DefaultFirebaseOptions.currentPlatform,
/// );
/// ```
class DefaultFirebaseOptions {
  static FirebaseOptions get currentPlatform {
    if (kIsWeb) {
      return web;
    }
    switch (defaultTargetPlatform) {
      case TargetPlatform.android:
        return android;
      case TargetPlatform.iOS:
        return ios;
      case TargetPlatform.macOS:
        return macos;
      case TargetPlatform.windows:
        return windows;
      case TargetPlatform.linux:
        throw UnsupportedError(
          'DefaultFirebaseOptions have not been configured for linux - '
          'you can reconfigure this by running the FlutterFire CLI again.',
        );
      default:
        throw UnsupportedError(
          'DefaultFirebaseOptions are not supported for this platform.',
        );
    }
  }

  static const FirebaseOptions web = FirebaseOptions(
    apiKey: 'AIzaSyA488PDFPuqnI6sfv4Y1tMKk-6huMOArFA',
    appId: '1:166081583463:web:0b507057e46ee263c977c2',
    messagingSenderId: '166081583463',
    projectId: 'tfc-gym-e3e80',
    authDomain: 'tfc-gym-e3e80.firebaseapp.com',
    storageBucket: 'tfc-gym-e3e80.firebasestorage.app',
    measurementId: 'G-CEFNH7JZHZ',
  );

  static const FirebaseOptions android = FirebaseOptions(
    apiKey: 'AIzaSyB-IZYNd9b3MWuq1iU_DZRjMlyS3MOZL5U',
    appId: '1:166081583463:android:07c513eb1c8c57ffc977c2',
    messagingSenderId: '166081583463',
    projectId: 'tfc-gym-e3e80',
    storageBucket: 'tfc-gym-e3e80.firebasestorage.app',
  );

  static const FirebaseOptions ios = FirebaseOptions(
    apiKey: 'AIzaSyC0w24gjxsPsvB70HXhybtXHWa0SVutYtY',
    appId: '1:166081583463:ios:6e1551078c4db331c977c2',
    messagingSenderId: '166081583463',
    projectId: 'tfc-gym-e3e80',
    storageBucket: 'tfc-gym-e3e80.firebasestorage.app',
    androidClientId: '166081583463-tibtqoo1m0r39bpac78060jk44rojme4.apps.googleusercontent.com',
    iosClientId: '166081583463-lh5vr7afgc69c0orinrgib2tkea88tnk.apps.googleusercontent.com',
    iosBundleId: 'com.example.tfcGymApp',
  );

  static const FirebaseOptions macos = FirebaseOptions(
    apiKey: 'AIzaSyC0w24gjxsPsvB70HXhybtXHWa0SVutYtY',
    appId: '1:166081583463:ios:6e1551078c4db331c977c2',
    messagingSenderId: '166081583463',
    projectId: 'tfc-gym-e3e80',
    storageBucket: 'tfc-gym-e3e80.firebasestorage.app',
    androidClientId: '166081583463-tibtqoo1m0r39bpac78060jk44rojme4.apps.googleusercontent.com',
    iosClientId: '166081583463-lh5vr7afgc69c0orinrgib2tkea88tnk.apps.googleusercontent.com',
    iosBundleId: 'com.example.tfcGymApp',
  );

  static const FirebaseOptions windows = FirebaseOptions(
    apiKey: 'AIzaSyAquOK5edFOYld98W9c2i-gaV5IiDBNFZ4',
    appId: '1:296994659250:web:19b083224983d574f9a2ab',
    messagingSenderId: '296994659250',
    projectId: 'ufc-moviles',
    authDomain: 'ufc-moviles.firebaseapp.com',
    databaseURL: 'https://ufc-moviles-default-rtdb.firebaseio.com',
    storageBucket: 'ufc-moviles.firebasestorage.app',
    measurementId: 'G-FQKLGWGVL5',
  );
}
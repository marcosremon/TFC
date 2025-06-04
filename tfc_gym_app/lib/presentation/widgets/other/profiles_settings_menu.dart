import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ProfileSettingsMenu extends StatelessWidget {
  final void Function(String value)? onSelected;
  const ProfileSettingsMenu({super.key, this.onSelected});

  Future<bool> _isAdmin() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString('role') == 'admin';
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<bool>(
      future: _isAdmin(),
      builder: (context, snapshot) {
        final isAdmin = snapshot.data ?? false;
        return PopupMenuButton<String>(
          icon: const Icon(Icons.settings),
          onSelected: onSelected,
          itemBuilder: (context) => [
            const PopupMenuItem(
              value: 'friendCode',
              child: Text('Mi codigo'),
            ),
            const PopupMenuItem(
              value: 'edit',
              child: Text('Editar cuenta'),
            ),
            const PopupMenuItem(
              value: 'changePassword',
              child: Text('Cambiar contraseña'),
            ),
            const PopupMenuItem(
              value: 'delete',
              child: Text('Borrar cuenta'),
            ),
            const PopupMenuItem(
              value: 'logout',
              child: Text('Cerrar Sesion'),
            ),
            if (isAdmin)
              const PopupMenuItem(
                value: 'admin',
                child: Text('Opción solo admin'),
              ),
          ],
        );
      },
    );
  }
}
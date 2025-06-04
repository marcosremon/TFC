import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/friend/delete_user_friend/delete_user_friend_request.dart';
import 'package:tfc_gym_app/presentation/providers/friend_provider.dart';

class DeleteFriendPopUp extends StatelessWidget {
  final String friendName;
  final String friendEmail;
  final VoidCallback? onDeleted;

  const DeleteFriendPopUp({
    super.key,
    required this.friendName,
    required this.friendEmail,
    this.onDeleted,
  });

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: const Text('Eliminar amigo'),
      content: Text('¿Seguro que quieres eliminar a $friendName de tu lista de amigos?'),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('Cancelar'),
        ),
        ElevatedButton(
          onPressed: () async {
            bool deleted = await context.read<FriendProvider>().deleteUserFriend(DeleteUserFriendRequest(friendEmail: friendEmail, userEmail: ''));
            Navigator.of(context).pop();
            if (deleted && onDeleted != null) {
              onDeleted!();
            }
          },
          style: ElevatedButton.styleFrom(
            backgroundColor: Colors.red,
          ),
          child: const Text(
            'Eliminar',
            style: TextStyle(color: Colors.white),
          ),
        ),
      ],
    );
  }
}
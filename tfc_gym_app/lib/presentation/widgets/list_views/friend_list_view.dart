import 'package:flutter/material.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/presentation/widgets/pop_ups/delete_friend_pop_up.dart';

class FriendsListView extends StatelessWidget {
  final List<UserDTO> friends;
  final Future<void> Function() onRefresh;
  final void Function(UserDTO friend)? onTapFriend;
  final void Function(UserDTO friend)? onDeleteFriend;

  const FriendsListView({
    super.key,
    required this.friends,
    required this.onRefresh,
    this.onTapFriend,
    this.onDeleteFriend,
  });

  @override
  Widget build(BuildContext context) {
    return RefreshIndicator(
      onRefresh: onRefresh,
      child: friends.isEmpty
          ? const Center(child: Text('No tienes amigos'))
          : ListView.builder(
              itemCount: friends.length,
              itemBuilder: (context, index) => ListTile(
                leading: const CircleAvatar(child: Icon(Icons.person)),
                title: Text(friends[index].username ?? 'Usuario'),
                subtitle: friends[index].email != null 
                    ? Text(friends[index].email!)
                    : null,
                trailing: IconButton(
                  icon: const Icon(Icons.delete, color: Colors.red),
                  onPressed: () {
                    showDialog(
                      context: context,
                      builder: (_) => DeleteFriendPopUp(
                        friendName: friends[index].username ?? 'usuario nulo',
                        friendEmail: friends[index].email!,
                        onDeleted: onRefresh,
                      ),
                    );
                  },
                ),           
                onTap: () {
                  if (onTapFriend != null) {
                    onTapFriend!(friends[index]);
                  }
                },
              ),
            ),
    );
  }
}
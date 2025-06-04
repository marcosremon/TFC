import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/presentation/screens/other/friend_profile_screen.dart';
import 'package:tfc_gym_app/presentation/widgets/list_views/friend_list_view.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/friend_controller.dart';

class FriendScreen extends StatefulWidget {
  const FriendScreen({super.key});

  @override
  State<FriendScreen> createState() => _FriendScreenState();
}

class _FriendScreenState extends State<FriendScreen> {
  late FriendController controller;

  @override
  void initState() {
    super.initState();
    controller = Provider.of<FriendController>(context, listen: false);
    controller.loadFriends(context);
  }

  @override
  void dispose() {
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    controller = Provider.of<FriendController>(context);
    return Scaffold(
      appBar: AppBar(title: const Text('Amigos')),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: TextField(
              controller: controller.searchController,
              decoration: InputDecoration(
                hintText: 'Agregar nuevos amigos...',
                prefixIcon: const Icon(Icons.search),
                suffixIcon: IconButton(
                  icon: const Icon(Icons.person_add),
                  onPressed: () => controller.addFriend(context),
                ),
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
            ),
          ),
          Expanded(
            child: FutureBuilder<List<UserDTO>>(
              future: controller.friendsFuture,
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return const Center(child: CircularProgressIndicator());
                }

                if (snapshot.hasError) {
                  return Center(child: Text('Error: ${snapshot.error}'));
                }

                var friends = snapshot.data ?? [];
                return FriendsListView(
                  friends: friends,
                  onRefresh: () => controller.loadFriends(context),
                  onTapFriend: (friend) {
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => FriendProfileScreen(email: friend.email ?? "")),
                    );
                  },
                  onDeleteFriend: (friend) => controller.deleteFriend(context, friend.email ?? ""),
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}
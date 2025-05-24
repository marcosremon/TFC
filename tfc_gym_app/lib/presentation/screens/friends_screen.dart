import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/models/dto/user_dto.dart';
import 'package:tfc_gym_app/presentation/screens/profile_screen.dart';
import 'package:tfc_gym_app/presentation/providers/friend_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/list_views/friend_list_view.dart';

class FriendScreen extends StatefulWidget {
  const FriendScreen({super.key});

  @override
  State<FriendScreen> createState() => _FriendScreenState();
}

class _FriendScreenState extends State<FriendScreen> {
  late Future<List<UserDTO>> _friendsFuture;
  final TextEditingController _searchController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _loadFriends();
  }

  Future<void> _loadFriends() async {
    setState(() {
      _friendsFuture = Provider.of<FriendProvider>(context, listen: false).getAllUserFriends();
    });
  }

  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Amigos')),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: TextField(
              controller: _searchController,
              decoration: InputDecoration(
                hintText: 'Agregar nuevos amigos...',
                prefixIcon: const Icon(Icons.search),
                suffixIcon: IconButton(
                  icon: const Icon(Icons.person_add),
                  onPressed: () async {
                    var friendCode = _searchController.text.trim();
                    if (friendCode.isNotEmpty) {
                      await Provider.of<FriendProvider>(context, listen: false).addNewUserFriend(friendCode);
                      _searchController.clear();
                      _loadFriends();
                    }
                  },
                ),
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
            ),
          ),
          Expanded(
            child: FutureBuilder<List<UserDTO>>(
              future: _friendsFuture,
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
                  onRefresh: _loadFriends,
                  onTapFriend: (friend) {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (_) => ProfileScreen(email: friend.email, showBackButton: true),
                      ),
                    );
                  },
                  onDeleteFriend: (friend) async {
                    bool deleted = await Provider.of<FriendProvider>(context, listen: false)
                        .deleteUserFriend(friend.email!);
                    if (deleted) {
                      _loadFriends();
                    }
                  },
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}
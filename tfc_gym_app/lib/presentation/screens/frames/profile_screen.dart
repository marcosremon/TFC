import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/profile_controller.dart';
import 'package:tfc_gym_app/presentation/widgets/list_views/user_routines_list_view.dart';
import 'package:tfc_gym_app/presentation/widgets/other/profiles_settings_menu.dart';
import 'package:tfc_gym_app/presentation/widgets/other/proflie_stats.dart';

class ProfileScreen extends StatefulWidget {
  final String? email;
  const ProfileScreen({super.key, this.email});

  @override
  State<ProfileScreen> createState() => _ProfileScreenState();
}

class _ProfileScreenState extends State<ProfileScreen> {
  late ProfileController controller;

  @override
  void initState() {
    super.initState();
    controller = Provider.of<ProfileController>(context, listen: false);
    WidgetsBinding.instance.addPostFrameCallback((_) {
      controller.loadUser(context, widget.email);
    });
  }

  @override
  Widget build(BuildContext context) {
    controller = Provider.of<ProfileController>(context);
    if (controller.isLoading || controller.user == null) {
      return const Scaffold(
        body: Center(child: CircularProgressIndicator()),
      );
    }

    final user = controller.user!;
    final username = controller.getUsername();
    final roleText = controller.getRoleText();
    final friendsCount = controller.getFriendsCount();
    final inscriptionDate = controller.getInscriptionDate();

    return Scaffold(
      appBar: AppBar(
        title: const Text('Perfil'),
        actions: [
          ProfileSettingsMenu(
            onSelected: (value) => controller.onProfileMenuSelected(context, value),
          ),
        ],
      ),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const CircleAvatar(
                  radius: 40,
                  backgroundImage: AssetImage('assets/images/profile_image.png'),
                ),
                const SizedBox(width: 16),
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        username,
                        style: const TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
                      ),
                      const SizedBox(height: 12),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          ProfileStat(label: 'Amigos', value: friendsCount.toString()),
                          ProfileStat(label: 'Rol', value: roleText),
                          ProfileStat(label: 'Inscrito', value: inscriptionDate),
                        ],
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
          Expanded(
            child: UserRoutinesListView(email: user.email),
          ),
        ],
      ),
    );
  }
}
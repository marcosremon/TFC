import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:tfc_gym_app/presentation/providers/auth_provider.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/change_password_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/edit_profile_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/buttons/conditional_black_button.dart';
import 'package:tfc_gym_app/presentation/widgets/list_views/user_routines_list_view.dart';
import 'package:tfc_gym_app/presentation/widgets/pop_ups/delete_account_pop_up.dart';
import 'package:tfc_gym_app/presentation/widgets/pop_ups/info_pop_up.dart';
import 'package:tfc_gym_app/presentation/widgets/profiles_settings_menu.dart';
import 'package:tfc_gym_app/presentation/widgets/proflie_stats.dart';

class ProfileScreen extends StatefulWidget {
  final String? email;
  final bool showBackButton;
  const ProfileScreen({super.key, this.email, this.showBackButton = false});

  @override
  State<ProfileScreen> createState() => _ProfileScreenState();
}

class _ProfileScreenState extends State<ProfileScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      context.read<UserProvider>().getUserByEmail(widget.email);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Consumer<UserProvider>(
      builder: (context, userProvider, _) {
      final user = userProvider.currentUser;
      if (user == null) {
        return const Scaffold(
          body: Center(child: CircularProgressIndicator()),
        );
      }

      String username = user.username ?? "Usuario";
      String roleText = "Usuario";
      if (user.role != null) {
        if (user.role.toString().contains('admin')) {
          roleText = "Admin";
        }
      }
      int friendsCount = user.friendsCount;
      var inscriptionDate = user.inscriptionDate != null
          ? user.inscriptionDate.toString().substring(0, 10)
          : "Sin fecha";

        return Scaffold(
          appBar: AppBar(
            title: const Text('Perfil'),
            leading: ConditionalBackButton(show: widget.showBackButton),
            actions: widget.showBackButton
                ? null
                : [
                    ProfileSettingsMenu(
                      onSelected: (value) {
                        switch (value) {
                          case 'admin':
                            // Acción solo admin
                            break;
                          case 'changePassword':
                            ChangePasswordBottomSheet.show(context);
                            break;
                          case 'logout':
                            context.read<AuthProvider>().logout(context);
                            break;
                          case 'edit':
                            showModalBottomSheet(
                              context: context,
                              isScrollControlled: true,
                              shape: const RoundedRectangleBorder(
                                borderRadius: BorderRadius.vertical(top: Radius.circular(25.0)),
                              ),
                              builder: (_) => EditProfileBottomSheet(user: user),
                            );
                            break;
                          case 'friendCode':
                            Future.delayed(Duration.zero, () {
                              showDialog(
                                context: context,
                                builder: (_) => InfoPopUp(
                                  "Tu código de amigo es: ${user.friendCode}",
                                ),
                              );
                            });
                            break;
                          case 'delete':
                            showDialog(
                              context: context,
                              builder: (_) => DeleteAccountPopUp(
                                onDeleted: () {
                                  context.read<AuthProvider>().logout(context);
                                },
                              ),
                            );
                            break;
                        }
                      },
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
                            username.toString(),
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
      },
    );
  }
}
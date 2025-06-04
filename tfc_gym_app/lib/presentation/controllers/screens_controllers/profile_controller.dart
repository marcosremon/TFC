import 'package:flutter/material.dart';
import 'package:tfc_gym_app/data/models/dto/user/get_user_by_email/get_user_by_email_request.dart';
import 'package:tfc_gym_app/data/models/dto/entitie/user_dto.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';
import 'package:tfc_gym_app/presentation/providers/auth_provider.dart';
import 'package:tfc_gym_app/presentation/screens/other/admin_web_view_screen.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/change_password_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/bottom_sheets/edit_profile_bottom_sheet.dart';
import 'package:tfc_gym_app/presentation/widgets/pop_ups/delete_account_pop_up.dart';
import 'package:tfc_gym_app/presentation/widgets/pop_ups/info_pop_up.dart';
import 'package:provider/provider.dart';

class ProfileController extends ChangeNotifier {
  UserDTO? user;
  bool isLoading = true;

  Future<void> loadUser(BuildContext context, String? email) async {
    isLoading = true;
    notifyListeners();
    await context.read<UserProvider>().getUserByEmail(GetUserByEmailRequest(email: email ?? ""));
    user = context.read<UserProvider>().currentUser;
    isLoading = false;
    notifyListeners();
  }

  String getUsername() => user?.username ?? "Usuario";

  String getRoleText() {
    if (user?.role != null && user!.role.toString().contains('admin')) {
      return "Admin";
    }
    return "Usuario";
  }

  int getFriendsCount() => user?.friendsCount ?? 0;

  String getInscriptionDate() =>
      user?.inscriptionDate != null ? user!.inscriptionDate.toString().substring(0, 10) : "Sin fecha";

  void onProfileMenuSelected(BuildContext context, String value) {
    switch (value) {
      case 'admin':
        AdminWebViewScreen.openAdminWeb();
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
          builder: (_) => EditProfileBottomSheet(user: user!),
        );
        break;
      case 'friendCode':
        Future.delayed(Duration.zero, () {
          showDialog(
            context: context,
            builder: (_) => InfoPopUp(
              "Tu código de amigo es: ${user?.friendCode ?? ''}",
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
  }
}
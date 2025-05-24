import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/datasources/friend_datasources.dart';
import 'package:tfc_gym_app/data/datasources/routine_datasources.dart';
import 'package:tfc_gym_app/data/datasources/user_datasources.dart';
import 'package:tfc_gym_app/data/datasources/auth_datasources.dart';
import 'package:tfc_gym_app/data/repositories/friend_repository.dart';
import 'package:tfc_gym_app/data/repositories/routine_repository.dart';
import 'package:tfc_gym_app/data/repositories/user_repository.dart';
import 'package:tfc_gym_app/data/repositories/auth_repository.dart';
import 'package:tfc_gym_app/presentation/providers/auth_provider.dart';
import 'package:tfc_gym_app/presentation/providers/friend_provider.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';

final appProviders = <ChangeNotifierProvider>[
  ChangeNotifierProvider<AuthProvider>(
    create: (_) => AuthProvider(
      AuthRepository(
        AuthDatasource(),
        authDatasource: AuthDatasource(),
        userDatasource: UserDatasource(),
      ),
    ),
  ),
  ChangeNotifierProvider<UserProvider>(
    create: (_) => UserProvider(
      userRepository: UserRepository(
        datasource: UserDatasource(),
        userDatasource: UserDatasource(),
      ),
    ),
  ),
  ChangeNotifierProvider<FriendProvider>(
    create: (_) => FriendProvider(
      FriendRepository(FriendDatasource()),
    ),
  ),
  ChangeNotifierProvider<RoutineProvider>(
    create: (_) => RoutineProvider(
      routineRepository: RoutineRepository(routineDatasource: RoutineDatasources()),
    ),
  ),
];
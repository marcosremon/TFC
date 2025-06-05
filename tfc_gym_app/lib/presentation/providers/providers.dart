import 'package:provider/provider.dart';
import 'package:tfc_gym_app/data/datasources/exercise_datasources.dart';
import 'package:tfc_gym_app/data/datasources/friend_datasources.dart';
import 'package:tfc_gym_app/data/datasources/routine_datasources.dart';
import 'package:tfc_gym_app/data/datasources/split_day_datasource.dart';
import 'package:tfc_gym_app/data/datasources/user_datasources.dart';
import 'package:tfc_gym_app/data/datasources/auth_datasources.dart';
import 'package:tfc_gym_app/data/repositories/exercise_repository.dart';
import 'package:tfc_gym_app/data/repositories/friend_repository.dart';
import 'package:tfc_gym_app/data/repositories/routine_repository.dart';
import 'package:tfc_gym_app/data/repositories/split_day_repository.dart';
import 'package:tfc_gym_app/data/repositories/user_repository.dart';
import 'package:tfc_gym_app/data/repositories/auth_repository.dart';
import 'package:tfc_gym_app/presentation/providers/auth_provider.dart';
import 'package:tfc_gym_app/presentation/providers/exercise_provider.dart';
import 'package:tfc_gym_app/presentation/providers/friend_provider.dart';
import 'package:tfc_gym_app/presentation/providers/routine_provider.dart';
import 'package:tfc_gym_app/presentation/providers/user_provider.dart';
import 'package:tfc_gym_app/presentation/providers/split_day_provider.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/auth/login_controller.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/auth/register_controller.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/routines/routine_controller.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/friend_controller.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/profile_controller.dart';
import 'package:tfc_gym_app/presentation/controllers/screens_controllers/routines/routine_detail_controller.dart';

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
  ChangeNotifierProvider<ExerciseProvider>(
    create: (_) => ExerciseProvider(
      exerciseRepository: ExerciseRepository(
        datasource: ExerciseDatasource(),
      ),
    ),
  ),
  ChangeNotifierProvider<SplitDayProvider>(
    create: (_) => SplitDayProvider(
      splitDayRepository: SplitDayRepository(
        splitDayDatasource: SplitDayDatasource(),
      ),
    ),
  ),

  ChangeNotifierProvider<LoginController>(
    create: (_) => LoginController(),
  ),
  ChangeNotifierProvider<RegisterController>(
    create: (_) => RegisterController(),
  ),
  ChangeNotifierProvider<RoutineController>(
    create: (_) => RoutineController(),
  ),
  ChangeNotifierProvider<FriendController>(
    create: (_) => FriendController(),
  ),
  ChangeNotifierProvider<ProfileController>(
    create: (_) => ProfileController(),
  ),
  ChangeNotifierProvider<RoutineDetailController>(
    create: (_) => RoutineDetailController(),
  ),
];
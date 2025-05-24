import 'package:flutter/material.dart';
import 'package:tfc_gym_app/presentation/screens/friends_screen.dart';
import 'package:tfc_gym_app/presentation/screens/profile_screen.dart';
import 'package:tfc_gym_app/presentation/screens/training_screen.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  int _selectedIndex = 0;

  Widget _getCurrentScreen() {
    switch (_selectedIndex) {
      case 0:
        return const FriendScreen();
      case 1:
        return const TrainingScreen();
      case 2:
        return const ProfileScreen();
      default:
        return const FriendScreen(); 
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: _getCurrentScreen(), 
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _selectedIndex,
        onTap: (index) {
          setState(() {
            _selectedIndex = index; 
          });
        },
        items: const [
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Inicio',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.fitness_center),
            label: 'Entrenamiento',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: 'Perfil',
          ),
        ],
      ),
    );
  }
}
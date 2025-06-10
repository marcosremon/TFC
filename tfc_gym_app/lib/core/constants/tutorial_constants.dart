import 'package:tfc_gym_app/data/models/dto/tutorial_step.dart';

final List<TutorialStep> tutorialContent = [
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/screen-friends.png',
    title: 'Amigos',
    description:
        'Para empezar, en la pantalla de amigos podemos ver que tenemos un cuadro de texto en el que podemos introducir el friend code de nuestro amigo el cual se encuntra en la pantalla perfil.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/friend-code-add.png',
    title: '',
    description:
        'al escribir el codigo de tu amigo podras pulsar en el boton de la derecha para agregarlo en caso de que no exista ese codigo no agregaras a nadie.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/friend-added.png',
    title: '',
    description:
        'tras agregar a tu amigo podras ver como se ha añadido a una lista de amigos.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/friend-delete.png',
    title: '',
    description:
        'ademas en caso de desearlo puedes borrar al amigo pulsando en el icono rojo de la paelera a la derecha.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/friend-info.png',
    title: '',
    description:
        'si quieres consultar la informacion de tu amigo rutinas etc, solo has de clickar en su icono para que se te despliegue su perfil.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/training-screen.png',
    title: 'Entrenamiento',
    description:
        'Ahora centremonos en el entrenamiento, en la screen entrenamiento podras ver tus rutinas, el numero de dias que vas al gym en todas tus rutinas (splits) y el numero de ejercicios totales que tienes que se va actualizando dinamicamente.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/create-routine-menu.png',
    title: '',
    description:
        'tras pulsar el boton de crear rutina veras este menu, el nombre de la rutina es obligatorio ponerlo, pero la descripcion no ademas puedes elejir para tu rutina los dias que desees.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/create-and-delete-routine-data.png',
    title: '',
    description:
        'cuando hallas seleccionado un dia podras añadir el nombre del ejercicio ademas de tener la opcion de añadri mas ejercicios a ese dia o borrar alguno si te has equivocado tras eso podras guardar la rutina en el boton de abajo de la pantalla.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/profile-screen.png',
    title: 'Perfil',
    description:
        'en la pantalla perfil podremos ver diversas cosas, tu foto de perfil, el numero de amigos que tienes, tu role y el dia que te uniste a la aplicacion, ademas podras ver tus rutinas dinamicamente o pulsar en el icono de opciones.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/profile-options-menu.png',
    title: '',
    description:
        'tras pulsar en el boton de opciones podremos ver distintas opciones como un boton para ver nuestro friend code, otro para editar nuestro parametros personales, otro para cambiar nuestra contraseaña, otro para borrar nuestra cuenta y un ultimo de cerrar sesion.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/routine-info.png',
    title: 'Routine',
    description:
        'si en el menu de perfil pulsamos sobre una rutina se nos abrira esta ventana en la que podremos eleir un dia de nuestro split para ver nuestro progreso.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/table-stats.png',
    title: '',
    description:
        'cuando presionemos un dia podremos ver esa tabla y si clikamos en los "..." se nos abrira un menu para poder apuntar el progreso diario.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/add-exercise-progress.png',
    title: 'Exercise',
    description:
        'en dicho menu podremos apuntar el numero de series, el numero de repeticiones y el peso levantado en ese dia.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/save-progress.png',
    title: '',
    description:
        'tras guardar el progreso podremos ver que se nos ha puesto en la tabla los datos introducidos serializados en el formato seriesXrepeticions@peso.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/add-exercise.png',
    title: '',
    description:
        'ademas si quieres añadir un nuevo ejercicio a la tabla no pasa nada, hay un boton en forma de + para poder añadir nuevos ejercicios.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/delete-exercise.png',
    title: '',
    description:
        'tambien podras borrar ejercicios de la tabla arrastrando hacia la derecha veras un icono de papelera roja.',
  ),
  TutorialStep(
    imagePath: 'assets/images/app-tutorial/add-or-delete-split.png',
    title: 'Splits',
    description:
        'otra opcion que tienes es añadir o borrar dias de tu rutina los dias que ya estan en tu rutina los veras de color rojo (borrar) y los que no estan los veras de color verde (añadir).',
  ),
];

# **Maze-Runners-Project**
## Informe del proyecto de programación del primer semestre de *Ciencias de la Computación* en MATCOM.


### **Asegurate de tener instalado:**
- **Visual Studio** (o cualquier otro IDE compatible con C#)
- **.NET SDK** (versión 6.0 o superior)
- **NAudio**: Librería para manejar audio en C#
  - Puedes instalarlo ejecutando el siguiente comando en la terminal: ***Install-Package NAudio -Version 2.0.0***
  - También revisa la dirección de la música, la ruta por defecto que le di yo es: *"D:\\proyects\\PRO-YECTO\\proyecto\\Maze-Runners-Project\\music\\Game of Thrones 8-bit(MP3_160K).mp3"*

### **Para Descargar y Ejecutar el Proyecto:**
Utiliza los siguientes comandos desde una terminal 
- Clonar el Repositorio
  - ***git clone*** https://github.com/Muzkatanke/Maze-Runners-Project.git .
- Navegar al Directorio del Proyecto
  - ***cd*** *D:\proyects\PRO-YECTO\proyecto\Maze-Runners-Project*
- Restaurar Paquetes NuGet
  - ***dotnet restore***
- Compilar el Proyecto
  - ***dotnet build***
- Ejecutar el Proyecto
  - ***dotnet run***
---
### **Instrucciones sobre como jugar:**
Estas se encuentran dentro del mismo juego, para leerlas deberás ejecutar el proyecto y saldrá el Menú Principal con las opciones:
- Jugar
- Opciones
- Salir

Dirígete al apartado **Opciones**. Allí dentro verás otro menú de opciones:
- Elegir tamaño del mapa
- Leer las instrucciones
- Atrás

Entra a la sección **Leer las instrucciones** y así podrás ver
toda la información relacionada a las mecánicas, funcionalidades, controles y otros aspectos. 

--- 

## **Descripción del Código:** 
El proyecto está diseñado siguiendo una arquitectura modular y orientada a objetos. La estructura principal del código se organiza en varias clases y espacios de nombres, cada uno responsable de una funcionalidad específica. A continuación, se describen los principales componentes del proyecto:

### Componentes Principales
1. **Program.cs**
    - Propósito: Punto de entrada del programa.
    - Responsabilidades: 
        - Inicializa los jugadores.
        - Configura la consola y muestra el menú principal.
        - Controla el bucle principal del juego, alternando entre los turnos de los jugadores y gestionando la lógica del juego.

2. **Algorithm.cs**
    - Propósito: Generación y manipulación del laberinto.
    - Responsabilidades:
        - Inicializa y genera el laberinto utilizando un algoritmo de Recursive Backtracking.
        - Implementa el algoritmo BFS (Breadth-First Search) para recorrer el mapa y colocar la condición de victoria en la posición más alejada del jugador.
        - Maneja la colocación de elementos en el laberinto, como trampas, obstáculos y beneficios.

3. **Map.cs**
    - Propósito: Movimiento, colisiones e interacción con los jugadores.
    - Responsabilidades:
        - Gestiona el movimiento de los jugadores en el laberinto.
        - Verifica colisiones y obstáculos.
        - Aplica los efectos de las trampas y beneficios.
        - Contiene la lógica para interactuar con diferentes elementos del mapa.

4. **Player.cs**
    - Propósito: Modelo de los jugadores.
    - Responsabilidades:
        - Define las propiedades de los jugadores, como posición, símbolo, velocidad y salud.
        - Proporciona un constructor para inicializar los jugadores con sus atributos específicos.

5. **Skills.cs**
    - Propósito: Habilidades especiales de los personajes.
    - Responsabilidades:
        - Implementa las habilidades únicas de cada personaje.
        - Aplica los efectos de las habilidades en el juego, como ataques, curaciones y transformaciones.

6. **Menu.cs**
    - Propósito: Interfaz de usuario y navegación del menú.
    - Responsabilidades:
        - Muestra el menú principal, el menú de opciones y el menú de selección de personajes.
        - Implementa la lógica para navegar entre los diferentes menús.
        - Proporciona una guía de instrucciones detallada utilizando la biblioteca Spectre.Console.
        - Añade un selector de tamaño del mapa.
        - Gestiona la visualización de barras de salud y otros elementos gráficos.

### Flujos Principales del Juego
**Inicialización:**
El juego comienza inicializando los jugadores, la música y mostrando el menú principal.

Se puede seleccionar el tamaño del laberinto y los personajes para cada jugador.

**Generación del Laberinto:**
El laberinto se inicializa y luego se genera utilizando un algoritmo de Recursive Backtracking para luego poder imprimirse en pantalla.

Con otros métodos se colocan: condición de victoria, trampas, obstáculos y beneficios en el laberinto.

**Juego por Turnos:**
Los jugadores se turnan dentro de un bucle principal para moverse en el laberinto 

Pueden interactuar con los elementos del mismo y usar sus habilidades especiales.

El juego termina cuando un jugador alcanza el Trono de Hierro.

**Bibliotecas Utilizadas:**
Spectre.Console: Para mejorar la interfaz de usuario en la consola con tablas, gráficos y menús interactivos.

NAudio: Para la reproducción de música de fondo durante el juego.




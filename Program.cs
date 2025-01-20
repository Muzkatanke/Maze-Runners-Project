
using System;
using Spectre.Console;
using Game.Map;
using Game.Player;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static Player Snow = new Player(0, 0, "🐺", 3, 3, 3, 3, 3, "El Bastardo de Invernalia Jon Snow. (Atributos: Fuerza = 3, Agilidad = 3, Intelecto = 3)");
    public static Player Tyrion = new Player(0, 1, "🦁", 2, 2, 1, 1, 5, "Mano del Rey Tyrion Lannister. Atributos: (Fuerza = 1, Agilidad = 2, Intelecto = 3)");
    public static Player Daenerys = new Player(1, 0, "🐉", 3, 3, 1, 2, 4, "Khaleesi Daenerys Targaryen. Atributos: (Fuerza = 1, Agilidad = 2, Intelecto = 4)");
    public static Player Arya = new Player(1, 1, "🗡️", 4, 4, 2, 5, 4, "La niña ciega Arya Stark. (Atributos: Fuerza = 2, Agilidad = 5, Intelecto = 4)");
    public static Player Brienne = new Player(2, 1, "🛡️", 2, 2, 5, 2, 2, "La Doncella de Tarth Brienne of Tarth. (Atributos: Fuerza-3, Agilidad-3, Intelecto-3)");


    public static List<Player> Players = new List<Player> { };

    public static void Main(string[] args)
    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ConsoleKeyInfo pressedKey;
        pressedKey = Console.ReadKey(true);
        int currentPlayer = 0;
        Menu.PrintMainMenu();

        do
        {
            if (currentPlayer == 0)
            {
                Tyrion.MovesLeft = Tyrion.Speed;
                Console.Clear();
                Map.PrintMaze(Map.maze, Snow, Tyrion);

                AnsiConsole.WriteLine("Current PLayer: Snow");
                AnsiConsole.WriteLine($"Moves Left: {Snow.MovesLeft}");
                AnsiConsole.Write(new Markup("Press [blue]<Start>[/] to use your hability"));

                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Snow.Xpos, Snow.Ypos, 0);
            }
            if (Snow.MovesLeft == 0) currentPlayer = 1;


            if (currentPlayer == 1)
            {
                Snow.MovesLeft = Snow.Speed;
                Console.Clear();
                Map.PrintMaze(Map.maze, Snow, Tyrion);
                AnsiConsole.WriteLine("Current PLayer: Tyrion");
                AnsiConsole.WriteLine($"Moves Left: {Tyrion.MovesLeft}");
                AnsiConsole.Write(new Markup("Press [blue]<Start>[/] to use your hability"));

                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Tyrion.Xpos, Tyrion.Ypos, 1);
            }
            if (Tyrion.MovesLeft == 0) currentPlayer = 0;

        } while (pressedKey.Key != ConsoleKey.Escape);
    }
}
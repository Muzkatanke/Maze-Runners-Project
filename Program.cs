
using System;
using Spectre.Console;
using Game.Map;
using Game.Player;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static Player Snow = new Player(0, 0, "🐺", 3, 3, 3, 3, 3);
    public static Player Tyrion = new Player(0, 1, "🦁", 2, 2, 1, 1, 5);
    public static Player Daenerys = new Player(1, 0, "🐉", 3, 3, 1, 2, 4);
    public static Player Arya = new Player(1, 1, "🗡️", 4, 4, 2, 5, 4);
    public static Player Brienne = new Player(2, 1, "🛡️", 2, 2, 5, 2, 2);

    public static List<Player> Players = new List<Player>();

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
                Players[1].MovesLeft = Players[1].Speed;
                Console.Clear();
                Map.PrintMaze(Map.maze, Players[0], Players[1]);

                AnsiConsole.WriteLine($"Current PLayer: {Players[0].Symbol}");
                AnsiConsole.WriteLine($"Moves Left: {Players[0].MovesLeft}");
                AnsiConsole.Write(new Markup("Press [blue]<Start>[/] to use your hability"));

                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Players[0].Xpos, Players[0].Ypos, 0);
            }
            if (Players[0].MovesLeft == 0) currentPlayer = 1;


            if (currentPlayer == 1)
            {
                Players[0].MovesLeft =Players[0].Speed;
                Console.Clear();
                Map.PrintMaze(Map.maze, Players[0],Players[1]);
                AnsiConsole.WriteLine($"Current PLayer: {Players[1].Symbol}");
                AnsiConsole.WriteLine($"Moves Left: {Players[1].MovesLeft}");
                AnsiConsole.Write(new Markup("Press [blue]<Start>[/] to use your hability"));

                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Players[1].Xpos, Players[1].Ypos, 1);
            }
            if (Players[1].MovesLeft == 0) currentPlayer = 0;

        } while (pressedKey.Key != ConsoleKey.Escape);
    }
}
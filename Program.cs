
using System;
using Spectre.Console;
using Game.Map;
using Game.Player;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static Player Harry = new Player(0, 0, "H", 3, 3);
    public static Player Ron = new Player(0, 1, "R", 2, 2);

    public static List<Player> Players = new List<Player> { Harry, Ron };

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ConsoleKeyInfo pressedKey;
        pressedKey = Console.ReadKey(true);
        int currentPlayer = 0;

        do
        {
            if(currentPlayer == 0)
            {
                Ron.MovesLeft = Ron.Speed;
                Console.Clear();
                Map.PrintMaze(Map.maze, Harry, Ron);

                AnsiConsole.WriteLine($"Current PLayer: Harry");
                AnsiConsole.WriteLine($"Moves Left: {Harry.MovesLeft}");
                AnsiConsole.Write(new Markup("Press [blue]<Start>[/] to use your hability"));

                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Harry.Xpos, Harry.Ypos, 0);
            }
            if(Harry.MovesLeft == 0) currentPlayer = 1;
           

            
            if(currentPlayer == 1)
            {
                 Harry.MovesLeft = Harry.Speed;
                Console.Clear();
                Map.PrintMaze(Map.maze, Harry, Ron);
                AnsiConsole.WriteLine($"Current PLayer: Ron");
                AnsiConsole.WriteLine($"Moves Left: {Ron.MovesLeft}");
                AnsiConsole.Write(new Markup("Press [blue]<Start>[/] to use your hability"));

                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Ron.Xpos, Ron.Ypos, 1);
            }
            if(Ron.MovesLeft == 0) currentPlayer = 0;
            

        } while (pressedKey.Key != ConsoleKey.Escape);


    }
}


       
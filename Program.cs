﻿
using System;
using Spectre.Console;
using Game.Map;
using Game.Player;
using Game.Menu;
using NAudio.Wave;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;


public class Program
{
    public static Player Snow = new Player(0, 0, "🐺", 3, 3, 4, 3, 4, 100);
    public static Player Tyrion = new Player(0, 1, "🦁", 2, 2, 1, 2, 5, 100);
    public static Player Daenerys = new Player(1, 0, "🐉", 3, 3, 3, 3, 4, 100);
    public static Player Arya = new Player(1, 1, "🎭", 4, 4, 2, 5, 4, 100);
    public static Player Robert = new Player(1, 2, "🦌", 2, 2, 5, 2, 2, 100);
    public static Player NightKing = new Player(1, 1, "💀", 2, 2, 5, 4, 4, 100);

    public static List<Player> Players = new List<Player>(2);
    public static int currentPlayer = 0;
    public static IWavePlayer ?waveOutDevice;
    public static AudioFileReader ?audioFileReader;

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string musicRoute = "D:\\proyects\\PRO-YECTO\\proyecto\\Maze-Runners-Project\\music\\Game of Thrones 8-bit(MP3_160K).mp3";
        
        Menu.MainTitle();
        Menu.TurnOnTheMusic(musicRoute);
        ConsoleKeyInfo pressedKey;
        AnsiConsole.MarkupLine($"\t\t\t\t\t\t\t\t\t[bold gold3_1]Press any key to start[/]");
        pressedKey = Console.ReadKey(true);

        Menu.PrintMainMenu();

        //revisar hab de robert

        int[] coolDowns = {Players[0].CD, Players[1].CD};
        
        do
        {
            if (currentPlayer == 0)
            {
                if (Players[0].Health <= 10)
                {
                    Players[0].Health = 10;
                    Players[0].Speed = 1;
                }

                Players[1].MovesLeft = Players[1].Speed;
                Console.Clear();
                Map.PrintMaze(Map.maze, Players);

                AnsiConsole.MarkupLine($"[bold]Current PLayer: {Players[0].Symbol}\t\tMoves Left: {Players[0].MovesLeft}\t\tCD Hability: {Players[0].CD}[/]");
                if (Players[0].CD == 0) AnsiConsole.MarkupLine("Habilidad Disponible!!");

                Menu.PrintHealthBar(Players, 0);
                AnsiConsole.Write(new Markup("[dim]NOTE: Press [slowblink blue]<Start>[/] to use your hability or [rapidblink blue]<Space>[/] to break an obstacle[/]"));

                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Players[0].Xpos, Players[0].Ypos, 0, Players, coolDowns);
            }
            if (Players[0].MovesLeft == 0) currentPlayer = 1;

            if (currentPlayer == 1)
            {
                if (Players[1].Health < 10)
                {
                    Players[1].Health = 10;
                    Players[1].Speed = 1;
                }

                Players[0].MovesLeft = Players[0].Speed;
                Console.Clear();
                Map.PrintMaze(Map.maze, Players);

                AnsiConsole.MarkupLine($"[bold]Current PLayer: {Players[1].Symbol}\t\tMoves Left: {Players[1].MovesLeft}\t\tCD Hability: {Players[1].CD}[/]");
                if (Players[1].CD == 0) AnsiConsole.MarkupLine("Habilidad Disponible!!");

                Menu.PrintHealthBar(Players, 1);
                AnsiConsole.Markup("[dim]NOTE: Press [blue]<Start>[/] to use your hability or [blue]<Space>[/] to break an obstacle[/]");

                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Players[1].Xpos, Players[1].Ypos, 1, Players, coolDowns);
            }
            if (Players[1].MovesLeft == 0) currentPlayer = 0;
           

        } while (pressedKey.Key != ConsoleKey.Escape);
    }
}
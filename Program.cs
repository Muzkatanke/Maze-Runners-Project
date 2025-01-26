
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
    public static Player Snow = new Player(0, 0, "🐺", 3, 3, 4, 3, 3, 100);
    public static Player Tyrion = new Player(0, 1, "🦁", 2, 2, 1, 2, 4, 100);
    public static Player Daenerys = new Player(1, 0, "🐉", 3, 3, 2, 3, 3, 100);
    public static Player Arya = new Player(1, 1, "🎭", 4, 4, 2, 4, 4, 100);
    public static Player Robert = new Player(1, 2, "🦌", 2, 2, 4, 2, 2, 100);
    public static Player NightKing = new Player(1, 1, "💀", 2, 2, 4, 2, 4, 100);

    public static List<Player> Players = new List<Player>(2);
    public static int currentPlayer = 0;
    public static IWavePlayer ?waveOutDevice;
    public static AudioFileReader ?audioFileReader;

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string musicRoute = "D:\\proyects\\PRO-YECTO\\proyecto\\Maze-Runners-Project\\music\\Game of Thrones 8-bit(MP3_160K).mp3";
        
        Menu.MainTitle();
     //   Menu.TurnOnTheMusic(musicRoute);
        ConsoleKeyInfo pressedKey;
        AnsiConsole.MarkupLine($"\t\t\t\t\t\t\t\t\t[bold gold3_1]Press any key to start[/]");
        pressedKey = Console.ReadKey(true);

        Menu.PrintMainMenu();


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

                AnsiConsole.MarkupLine($"[bold]Jugador actual: {Players[0].Symbol}\t\tJugadas restantes: {Players[0].MovesLeft}\t\tCD Habilidad: {Players[0].CD}[/]\t\t");
                if (Players[0].CD == 0) AnsiConsole.MarkupLine("Habilidad Disponible!!");

                Menu.PrintHealthBar(Players, 0);
                AnsiConsole.MarkupLine("[dim]NOTA: Presiona [blue]<Enter>[/] para usar tu habilidad o [blue]<Barra Espaciadora>[/] para romper un obstaculo[/]");
                AnsiConsole.MarkupLine($"[bold]Atributos:[/]\n[bold red]Fuerza - {Players[0].Strength}[/]\n[bold green]Agilidad - {Players[0].Agility}[/]\n[bold blue]Inteligencia - {Players[0].Intellect}[/]");
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

                AnsiConsole.MarkupLine($"[bold]Jugador actual: {Players[1].Symbol}\t\tJugadas restantes: {Players[1].MovesLeft}\t\tCD Habilidad: {Players[1].CD}[/]");
                if (Players[1].CD == 0) AnsiConsole.MarkupLine("Habilidad Disponible!!");

                Menu.PrintHealthBar(Players, 1);
                AnsiConsole.MarkupLine("[dim]NOTA: Presiona [blue]<Enter>[/] para usar tu habilidad o [blue]<Barra Espaciadora>[/] para romper un obstaculo[/]");
                AnsiConsole.MarkupLine($"[bold]Atributos:[/]\n[bold red]Fuerza - {Players[1].Strength}[/]\n[bold green]Agilidad - {Players[1].Agility}[/]\n[bold blue]Inteligencia - {Players[1].Intellect}[/]");
                pressedKey = Console.ReadKey(true);
                Map.MovePlayer(pressedKey.Key, Players[1].Xpos, Players[1].Ypos, 1, Players, coolDowns);
            }
            if (Players[1].MovesLeft == 0) currentPlayer = 0;
           

        } while (pressedKey.Key != ConsoleKey.Escape);
    }
}
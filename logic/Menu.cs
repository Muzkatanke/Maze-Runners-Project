namespace Game.Menu;
using Spectre.Console;
using Game.Player;
using NAudio.Wave;
public class Menu
{
    public static void PrintMainMenu()
    {
        Console.Clear();
        var optionsMainMenu = new[]
        {
            "Jugar",
            "Opciones",
            "Salir",
        };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Selecciona una opción:")
                .AddChoices(optionsMainMenu)
        );

        switch (selection)
        {
            case "Jugar":
                PrintCharacterSelectionMenu();
                break;
            case "Opciones":
                AnsiConsole.MarkupLine("[red]Ups... no hay opciones aun[/]");
                PrintMainMenu();
                break;
            case "Salir":
                AnsiConsole.MarkupLine("[red]Saliendo del juego...[/]");
                Environment.Exit(0);
                break;
        }
    }
    public static void PrintCharacterSelectionMenu()
    {
        Console.Clear();
        List<string> characters = new List<string>
        {
            "[italic]Winterfell's Bastard[/] [bold black][underline]Jon Snow[/][/] 🐺\n",
            "[italic]King's Hand[/] [bold gold1][underline]Tyrion Lannister[/][/] 🦁\n",
            "[italic]The Blind Child[/] [bold blue][underline]Arya Stark[/][/] 🎭\n",
            "[italic]The Usurper[/] [bold yellow][underline]Robert Baratheon[/][/] 🦌\n",
            "[italic]The First White Walker[/] [bold white][underline] El Rey de la Noche[/][/] 💀\n",
            "[italic]The First of Her Name, Breaker of Chains, Mother of Dragons (...)[/] [bold red][underline] Daenerys Targaryen[/][/] 🐉"
        };

        var characterSelection1 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Selecciona un heroe para el primer jugador:")
                .AddChoices(characters)
        );

        characters.Remove(characterSelection1);

        var characterSelection2 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Selecciona un heroe para el segundo jugador:")
                .AddChoices(characters)

        );

        AnsiConsole.MarkupLine($"Habeis seleccionado a {characterSelection1} y {characterSelection2} ¿Deseais continuar?");
        AnsiConsole.MarkupLine($"[bold grey]Presiona [bold blue]<Enter>[/] para continuar o [bold blue]<Escape>[/] para retroceder[/]");
        ConsoleKeyInfo pressedKey;
        pressedKey = Console.ReadKey(true);
        if (pressedKey.Key == ConsoleKey.Escape)
        {
            Console.Clear();
            PrintCharacterSelectionMenu();
        }

        switch (characterSelection1)
        {
            case "[italic]Winterfell's Bastard[/] [bold black][underline]Jon Snow[/][/] 🐺\n":
                Program.Players.Add(Program.Snow);
                break;
            case "[italic]King's Hand[/] [bold gold1][underline]Tyrion Lannister[/][/] 🦁\n":
                Program.Players.Add(Program.Tyrion);
                break;
            case "[italic]The Blind Child[/] [bold blue][underline]Arya Stark[/][/] 🎭\n":
                Program.Players.Add(Program.Arya);
                break;
            case "[italic]The Usurper[/] [bold yellow][underline]Robert Baratheon[/][/] 🦌\n":
                Program.Players.Add(Program.Robert);
                break;
            case "[italic]The First White Walker[/] [bold white][underline] El Rey de la Noche[/][/] 💀\n":
                Program.Players.Add(Program.NightKing);
                break;
            case "[italic]The First of Her Name, Breaker of Chains, Mother of Dragons (...)[/] [bold red][underline] Daenerys Targaryen[/][/] 🐉":
                Program.Players.Add(Program.Daenerys);
                break;
        }
        switch (characterSelection2)
        {
            case "[italic]Winterfell's Bastard[/] [bold black][underline]Jon Snow[/][/] 🐺\n":
                Program.Players.Add(Program.Snow);
                break;
            case "[italic]King's Hand[/] [bold gold1][underline]Tyrion Lannister[/][/] 🦁\n":
                Program.Players.Add(Program.Tyrion);
                break;
            case "[italic]The Blind Child[/] [bold blue][underline]Arya Stark[/][/] 🎭\n":
                Program.Players.Add(Program.Arya);
                break;
            case "[italic]The Usurper[/] [bold yellow][underline]Robert Baratheon[/][/] 🦌\n":
                Program.Players.Add(Program.Robert);
                break;
            case "[italic]The First White Walker[/] [bold white][underline] El Rey de la Noche[/][/] 💀\n":
                Program.Players.Add(Program.NightKing);
                break;
            case "[italic]The First of Her Name, Breaker of Chains, Mother of Dragons (...)[/] [bold red][underline] Daenerys Targaryen[/][/] 🐉":
                Program.Players.Add(Program.Daenerys);
                break;
        }
    }
    public static void PrintHealthBar(List<Player> Players, int currentPlayer)
    {
        if (currentPlayer == 0)
        {
            AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[0].Symbol} [bold]Health[/]", Players[0].Health, Color.Red));
        }

        if (currentPlayer == 1)
        {
            AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[1].Symbol} [bold]Health[/]", Players[1].Health, Color.Blue));
        }
    }
    public static void MainTitle()
    {
        AnsiConsole.Write(
            new FigletText("Maze of Thrones")
                .Centered()
                .Color(Color.Gold1));
    }

    public static void TurnOnTheMusic(string musicRoute)
    {
        Program.waveOutDevice = new WaveOutEvent();
        Program.audioFileReader = new AudioFileReader(musicRoute);
        Program.waveOutDevice.Init(Program.audioFileReader);
        Program.waveOutDevice.Play();
    }

}


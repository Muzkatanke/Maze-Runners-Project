using Spectre.Console;
using Game.Player;
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
            "[italic]Winterfell's Bastard[/] [bold white][underline]Jon Snow[/][/]",
            "[italic]King's Hand[/] [bold grey][underline]Tyrion Lannister[/][/]",
            "[italic]Khaleesi[/] [bold red][underline]Daenerys Targaryen[/][/]",
            "[italic]The Blind Child[/] [bold blue][underline]Arya Stark[/][/]",
            "[italic]The usurper[/] [bold yellow][underline]Robert Baratheon[/][/]",
        };

        var characterSelection1 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Selecciona un heroe para el primer jugador:")
                .PageSize(5)
                .AddChoices(characters)
        );

        characters.Remove(characterSelection1);

        var characterSelection2 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Selecciona un heroe para el segundo jugador:")
                .PageSize(5)
                .AddChoices(characters)

        );

        AnsiConsole.MarkupLine($"Habeis seleccionado a {characterSelection1} y {characterSelection2}. ¿Deseais continuar?");
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
            case "[italic]Winterfell's Bastard[/] [bold white][underline]Jon Snow[/][/]":
                Program.Players.Add(Program.Snow);
                break;
            case "[italic]King's Hand[/] [bold grey][underline]Tyrion Lannister[/][/]":
                Program.Players.Add(Program.Tyrion);
                break;
            case "[italic]Khaleesi[/] [bold red][underline]Daenerys Targaryen[/][/]":
                Program.Players.Add(Program.Daenerys);
                break;
            case "[italic]The Blind Child[/] [bold blue][underline]Arya Stark[/][/]":
                Program.Players.Add(Program.Arya);
                break;
            case "[italic]The usurper[/] [bold yellow][underline]Robert Baratheon[/][/]":
                Program.Players.Add(Program.Robert);
                break;
        }
        switch (characterSelection2)
        {
            case "[italic]Winterfell's Bastard[/] [bold white][underline]Jon Snow[/][/]":
                Program.Players.Add(Program.Snow);
                break;
            case "[italic]King's Hand[/] [bold grey][underline]Tyrion Lannister[/][/]":
                Program.Players.Add(Program.Tyrion);
                break;
            case "[italic]Khaleesi[/] [bold red][underline]Daenerys Targaryen[/][/]":
                Program.Players.Add(Program.Daenerys);
                break;
            case "[italic]The Blind Child[/] [bold blue][underline]Arya Stark[/][/]":
                Program.Players.Add(Program.Arya);
                break;
            case "[italic]The Usurper[/] [bold yellow][underline]Robert Baratheon[/][/]":
                Program.Players.Add(Program.Robert);
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
        else
        {
            AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[1].Symbol} [bold]Health[/]", Players[1].Health, Color.Red));
        }
    }
}

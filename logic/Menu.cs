using Spectre.Console;
using Game.Player;
public class Menu
{

    public static void PrintMainMenu()
    {
        var optionsMainMenu = new[]
        {
            "Jugar",
            "Opciones",
            "Salir"
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
        List<Player> characters = new List<Player>
        {
            Program.Snow,
            Program.Tyrion,
            Program.Daenerys,
            Program.Arya,
            Program.Brienne,
        };

        var characterSelection1 = AnsiConsole.Prompt(
            new SelectionPrompt<Player>()
                .Title("Selecciona un heroe para el primer jugador:")
                .PageSize(5)
                .AddChoices(characters)
        );

        characters.Remove(characterSelection1);

        var characterSelection2 = AnsiConsole.Prompt(
            new SelectionPrompt<Player>()
                .Title("Selecciona un heroe para el segundo jugador:")
                .PageSize(5)
                .AddChoices(characters)
        );

        AnsiConsole.MarkupLine($"Habeis seleccionado a [bold red]{characterSelection1}[/] y [bold yellow]{characterSelection2}[/]. ¿Deseais continuar?");
        AnsiConsole.MarkupLine($"[bold grey]Presiona [bold blue]<Enter>[/] para continuar o [bold blue]<Escape>[/] para retroceder[/]");
        ConsoleKeyInfo pressedKey;
        pressedKey = Console.ReadKey(true);
        if (pressedKey.Key == ConsoleKey.Escape)
        {
            Console.Clear();
            PrintCharacterSelectionMenu();
        }

        List<Player> Players = new List<Player> { characterSelection1, characterSelection2 };

    }
}
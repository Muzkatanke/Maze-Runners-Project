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
            "[bold steelblue]Jugar[/]\n",
            "[bold steelblue]Opciones[/]\n",
            "[bold steelblue]Salir[/]",
        };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[lightskyblue1]Selecciona una opción:[/]")
                .AddChoices(optionsMainMenu)
        );

        switch (selection)
        {
            case "[bold steelblue]Jugar[/]\n":
                PrintCharacterSelectionMenu();
                break;
            case "[bold steelblue]Opciones[/]\n":
                PrintOptionsMenu();
                break;
            case "[bold steelblue]Salir[/]":
                AnsiConsole.MarkupLine("[red]Saliendo del juego...[/]");
                Environment.Exit(0);
                break;
        }
    }
    public static void PrintOptionsMenu()
    {
        Console.Clear();
        var optionsMenu = new[]
        {
            "[bold steelblue]Elegir tamaño del mapa[/]\n",
            "[bold steelblue]Leer las instrucciones[/]\n",
            "🔙[bold steelblue] Atras[/]",
        };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[lightskyblue1]Selecciona una opción:[/]")
                .AddChoices(optionsMenu)
        );

        switch (selection)
        {
            case "[bold steelblue]Elegir tamaño del mapa[/]\n":
                PrintMapSelectorMenu();
                
                break;
            case "[bold steelblue]Leer las instrucciones[/]\n":
                PrintInstructionsMenu();
                break;
            case "🔙[bold steelblue] Atras[/]":
                PrintMainMenu();
                break;
        }
    }

    public static void PrintInstructionsMenu()
    {
        var table = new Table();
        table.Border(TableBorder.Double);
        table.Title("[bold underline gold3]Manual de Instrucciones: Maze of Thrones[/]");

        table.AddColumn(new TableColumn("[bold yellow]Sección[/]"));
        table.AddColumn(new TableColumn("[bold yellow]Descripción[/]"));


        table.AddRow(new Markup("[bold underline blue]Introducción[/]"), new Markup(
            "¡Bienvenido a [italic gold3]Maze of Thrones[/]! En este emocionante juego de estrategia y aventura, " +
            "inspirado en la aclamada serie [italic]Game of Thrones[/], tendrás la oportunidad " +
            "de elegir entre varios personajes icónicos, cada uno con habilidades únicas. " +
            "Sé el primero en llegar al Trono de Hierro y reclama tu lugar como el gobernante supremo.\n"));


        table.AddRow(new Markup("[bold underline blue]Personajes y Habilidades[/]"), new Markup(
            "[bold underline grey]Jon Snow 🐺:[/] [italic cyan](Longclaw)[/], ejecuta un ataque de 360 grados.\n" +
            "[bold underline gold1]Tyrion Lannister 🦁:[/] [italic cyan](WizardTyrion)[/], crea vinos especiales que aumentan sus capacidades.\n" +
            "[bold underline darkred]Daenerys Targaryen 🐲:[/] [italic cyan](Mother of Dragons)[/], se convierte en un imponente dragón.\n" +
            "[bold underline white]Arya Stark 🎭:[/] [italic cyan](Insight)[/], desarma trampas a su alrededor.\n" +
            "[bold underline yellow]Robert Baratheon 🦌:[/] [italic cyan](Deer Vigor)[/], le permite curarse y aumentar su velocidad.\n" +
            "[bold underline teal]El Rey de la Noche 💀:[/] [italic cyan](Curse)[/], maldice a los enemigos en cualquier parte del mapa.\n"));


        table.AddRow(new Markup("[bold underline blue]Mapa del Juego[/]"), new Markup(
            "[bold underline red]La Fortaleza Roja:[/] Un laberinto generado aleatoriamente que cambia con cada partida nueva.\n" +
            "[bold underline]Objetivo:[/] Llegar al Trono de Hierro (👑) antes que tu oponente.\n"));


        table.AddRow(new Markup("[bold underline blue]Elementos del Mapa[/]"), new Markup(
            "[bold underline]Trampas:[/]\n" +
            " - [red]Explosión (🔥):[/] Disminuye tu vida al pasar sobre ella (-40).\n" +
            " - [red]Acertijo (🧩):[/] Adivinanza que te pondrá a prueba. Si fallas, sufrirás daño (-15) y ralentización (-1).\n" +
            " - [red]Aceite (♨️ ):[/] Reduce tu velocidad de movimiento al mínimo (1).\n" +
            "[bold underline]Obstáculos:[/]\n" +
            " - [grey]Ladrillos (🧱):[/] Destructibles y pueden abrir atajos.\n" +
            "[bold underline]Beneficios:[/]\n" +
            " - [green]Dornish Red (🍷):[/] Otorga velocidad adicional (+1) y aumenta tu agilidad al máximo (5).\n" +
            " - [green]Arbor Gold (🍸):[/] Otorga una curación (+10) y aumenta tu inteligencia al máximo (5).\n"));


        table.AddRow(new Markup("[bold underline blue]Mecánica del Juego[/]"), new Markup(
            "[bold underline]Desarrollo del Juego por Turnos:[/]\n" +
            " - Movimiento: Usa las [bold yellow]flechas del teclado[/] para moverte.\n" +
            " - Atributos: Existen atributos como [red]Fuerza[/], [green]Agilidad[/] e [blue]Inteligencia[/], que determinan si eres capaz de evitar una trampa o no.\n" +
            " - Acciones: Rompe obstáculos ([bold magenta]Space[/]) o activa habilidades ([bold magenta]Enter[/]).\n" +
            " - Acciones por Turno: Basadas en las características del personaje, habrá una [italic yellow]X[/] cantidad de movimientos para ejecutar en cada turno.\n"));


        table.AddRow(new Markup("[bold underline blue]Efectos en el Juego[/]"), new Markup(
            "[bold underline]Duración:[/] Los efectos duran hasta el final de la partida o hasta que sean reemplazados.\n" +
            "[bold underline]Barra de Vida:[/] Cada personaje tiene hasta [bold red]100 puntos de vida[/]. Si tu vida baja de 10, " +
            "tu velocidad se reducirá en 1 hasta que encuentres curación.\n"));


        table.AddRow(new Markup("[bold underline blue]Consejos para ganar[/]"), new Markup(
            "Administra bien tus movimientos, evita las trampas, " +
            "traza una ruta y utiliza tus habilidades para superar a tu oponente.\n"));


        AnsiConsole.Write(table);


        AnsiConsole.MarkupLine("[bold magenta]<Escape>[/][bold steelblue] para retroceder[/]");

        ConsoleKeyInfo pressedKey;
        pressedKey = Console.ReadKey(true);

        if (pressedKey.Key == ConsoleKey.Escape)
        {
            Console.Clear();
            PrintOptionsMenu();
        }
        else
        {
            Console.Clear();
            PrintInstructionsMenu();
        }
    }

    public static void PrintMapSelectorMenu()
    {
        Console.Clear();
        var optionsMenu = new[]
        {
            "[bold grey][underline]Pequeño[/] (15x15)[/]\n",
            "[bold green][underline]Mediano[/] (25x25)[/]\n",
            "[bold red][underline]Grande[/] (35x35)[/]\n",
            "🔙[bold steelblue] Atras[/]"
        };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold lightskyblue1]Selecciona una opción:[/]")
                .AddChoices(optionsMenu)
        );

        if (selection == "🔙[bold steelblue] Atras[/]")
        {
            PrintOptionsMenu();
        }

        AnsiConsole.MarkupLine($"[bold steelblue][bold magenta]<Enter>[/] para continuar o [bold magenta]<Escape>[/] para retroceder[/]");

        ConsoleKeyInfo pressedKey;
        pressedKey = Console.ReadKey(true);

        if (pressedKey.Key == ConsoleKey.Escape)
        {
            Console.Clear();
            PrintMapSelectorMenu();
        }
        if (pressedKey.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            PrintMainMenu();
        }

        switch (selection)
        {
            case "[bold grey][underline]Pequeño[/] (15x15)[/]\n":
                Algorithm.maze = new Cell[15, 15];

                break;
            case "[bold green][underline]Mediano[/] (25x25)[/]\n":
                Algorithm.maze = new Cell[25, 25];
                break;
            case "[bold red][underline]Grande[/] (35x35)[/]\n":
                Algorithm.maze = new Cell[35, 35];
                break;
            case "🔙[bold steelblue] Atras[/]":
                PrintOptionsMenu();
                break;
        }
    }

    public static void PrintCharacterSelectionMenu()
    {
        Console.Clear();
        List<string> characters = new List<string>
        {
            "[italic grey7]Winterfell's Bastard[/] [bold black][underline]Jon Snow[/][/] 🐺\n",
            "[italic grey7]King's Hand[/] [bold gold1][underline]Tyrion Lannister[/][/] 🦁\n",
            "[italic grey7]The Blind Child[/] [bold blue][underline]Arya Stark[/][/] 🎭\n",
            "[italic grey7]The Usurper[/] [bold yellow][underline]Robert Baratheon[/][/] 🦌\n",
            "[italic grey7]The First White Walker[/] [bold white][underline]El Rey de la Noche[/][/] 💀\n",
            "[italic grey7]The First of Her Name, Breaker of Chains, Mother of Dragons (...)[/] [bold red][underline] Daenerys Targaryen[/][/] 🐲\n",
        };

        var characterSelection1 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold steelblue]Selecciona un heroe para el primer jugador:[/]")
                .AddChoices(characters)
        );

        characters.Remove(characterSelection1);

        var characterSelection2 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold steelblue]Selecciona un heroe para el segundo jugador:[/]")
                .AddChoices(characters)

        );

        AnsiConsole.MarkupLine($"[bold steelblue][bold blue]<Enter>[/] para continuar o [bold blue]<Escape>[/] para retroceder[/]");

        ConsoleKeyInfo pressedKey;
        pressedKey = Console.ReadKey(true);

        if (pressedKey.Key == ConsoleKey.Escape)
        {
            Console.Clear();
            PrintCharacterSelectionMenu();
        }

        switch (characterSelection1)
        {
            case "[italic grey7]Winterfell's Bastard[/] [bold black][underline]Jon Snow[/][/] 🐺\n":
                Program.Players.Add(Program.Snow);
                break;
            case "[italic grey7]King's Hand[/] [bold gold1][underline]Tyrion Lannister[/][/] 🦁\n":
                Program.Players.Add(Program.Tyrion);
                break;
            case "[italic grey7]The Blind Child[/] [bold blue][underline]Arya Stark[/][/] 🎭\n":
                Program.Players.Add(Program.Arya);
                break;
            case "[italic grey7]The Usurper[/] [bold yellow][underline]Robert Baratheon[/][/] 🦌\n":
                Program.Players.Add(Program.Robert);
                break;
            case "[italic grey7]The First White Walker[/] [bold white][underline]El Rey de la Noche[/][/] 💀\n":
                Program.Players.Add(Program.NightKing);
                break;
            case "[italic grey7]The First of Her Name, Breaker of Chains, Mother of Dragons (...)[/] [bold red][underline] Daenerys Targaryen[/][/] 🐲\n":
                Program.Players.Add(Program.Daenerys);
                break;
        }
        switch (characterSelection2)
        {
            case "[italic grey7]Winterfell's Bastard[/] [bold black][underline]Jon Snow[/][/] 🐺\n":
                Program.Players.Add(Program.Snow);
                break;
            case "[italic grey7]King's Hand[/] [bold gold1][underline]Tyrion Lannister[/][/] 🦁\n":
                Program.Players.Add(Program.Tyrion);
                break;
            case "[italic grey7]The Blind Child[/] [bold blue][underline]Arya Stark[/][/] 🎭\n":
                Program.Players.Add(Program.Arya);
                break;

            case "[italic grey7]The Usurper[/] [bold yellow][underline]Robert Baratheon[/][/] 🦌\n":
                Program.Players.Add(Program.Robert);
                break;
            case "[italic grey7]The First White Walker[/] [bold white][underline]El Rey de la Noche[/][/] 💀\n":
                Program.Players.Add(Program.NightKing);
                break;
            case "[italic grey7]The First of Her Name, Breaker of Chains, Mother of Dragons (...)[/] [bold red][underline] Daenerys Targaryen[/][/] 🐲\n":
                Program.Players.Add(Program.Daenerys);
                break;
        }
    }

    public static void PrintHealthBar(List<Player> Players, int currentPlayer)
    {
        int player = currentPlayer == 0 ? 0 : 1;

        switch(Players[currentPlayer].Symbol)
        {
            case "🐺 ":
                AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[player].Symbol} [bold steelblue]Health[/]", Players[player].Health, Color.Black));
                break;
            case "🦁 ":
                AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[player].Symbol} [bold steelblue]Health[/]", Players[player].Health, Color.Gold1));
                break;
            case "🐲 ":
                AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[player].Symbol} [bold steelblue]Health[/]", Players[player].Health, Color.Red));
                break;
            case "🎭 ":
                AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[player].Symbol} [bold steelblue]Health[/]", Players[player].Health, Color.Blue));
                break;
            case "🦌 ":
                AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[player].Symbol} [bold steelblue]Health[/]", Players[player].Health, Color.Yellow));
                break;
            case "💀 ":
                AnsiConsole.Write(new BarChart()
                .Width(45)
                .AddItem($"{Players[player].Symbol} [bold steelblue]Health[/]", Players[player].Health, Color.White));
                break;
            
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
        do
        {
            Program.waveOutDevice = new WaveOutEvent();
            Program.audioFileReader = new AudioFileReader(musicRoute);
            Program.waveOutDevice.Init(Program.audioFileReader);
            Program.waveOutDevice.Play();

            Thread.Sleep(122000);
        }while (true);


    }

}


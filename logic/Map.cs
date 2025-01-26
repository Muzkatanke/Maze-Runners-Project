using Microsoft.VisualBasic;
using Spectre.Console;
using Game.Player;
using Game.Menu;
namespace Game.Map;

public enum Cell
{
   Wall, Floor, BurstTrap, RiddleTrap, OilTrap, BricksObstacle, Throne, DornishRed, ArborGold
};

public class Map
{
   public static Dictionary<Cell, string> CellSymbols = new Dictionary<Cell, string>
   {
      { Cell.Wall, "🟥" },
      { Cell.Floor, "◾" },
      { Cell.BurstTrap, "🔥" },
      { Cell.RiddleTrap, "🧩" },
      { Cell.OilTrap, "♨️" },
      { Cell.BricksObstacle, "🧱" },
      { Cell.Throne, "👑"},
      { Cell.DornishRed, "🍷"},
      { Cell.ArborGold, "🍸"},
   };

   public static Cell[,] maze = new Cell[10, 10]
   {
      { Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Floor, Cell.Floor, Cell.RiddleTrap, Cell.OilTrap, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.BurstTrap, Cell.BricksObstacle, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.DornishRed, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Floor, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.BurstTrap, Cell.Floor, Cell.Wall, Cell.DornishRed, Cell.Wall,Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall,Cell.Floor, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.ArborGold, Cell.Floor, Cell.BricksObstacle, Cell.Floor,Cell.Floor, Cell.Floor, Cell.ArborGold, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall,Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Floor, Cell.RiddleTrap, Cell.Floor,Cell.BricksObstacle, Cell.BurstTrap, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall,Cell.Wall, Cell.Wall, Cell.Throne, Cell.Wall, Cell.Wall },
   };

   public static void PrintMaze(Cell[,] maze, List<Player.Player> Players)
   {
      Console.Clear();

      int rows = maze.GetLength(0);
      int cols = maze.GetLength(1);

      var table = new Table();
      table.Border(TableBorder.Horizontal);

      for (int j = 0; j < cols; j++)
      {
         table.AddColumn(new TableColumn(""));
      }

      for (int i = 0; i < rows; i++)
      {
         var rowContent = new List<string>();
         for (int j = 0; j < cols; j++)
         {
            if (i == Players[0].Xpos && j == Players[0].Ypos)
            {
               rowContent.Add(Players[0].Symbol);
            }
            else if (i == Players[1].Xpos && j == Players[1].Ypos)
            {
               rowContent.Add(Players[1].Symbol);
            }
            else rowContent.Add(CellSymbols[maze[i, j]].ToString());

         }
         table.AddRow(rowContent.ToArray());
      }
      table.HideHeaders();

      var panel = new Panel(table);
      panel.Border = BoxBorder.Double;
      panel.Header("Maze", Justify.Center);
      AnsiConsole.Write(panel);
   }
   public static void MovePlayer(ConsoleKey pressedKey, int currentPlayerXpos, int currentPlayerYpos, int currentPlayer, List<Player.Player> Players, int[] coolDowns)
   {
      int newCurrentPlayerXpos = currentPlayerXpos;
      int newCurrentPlayerYpos = currentPlayerYpos;

      switch (pressedKey)
      {
         case ConsoleKey.UpArrow:
            newCurrentPlayerXpos--;
            break;
         case ConsoleKey.DownArrow:
            newCurrentPlayerXpos++;
            break;
         case ConsoleKey.RightArrow:
            newCurrentPlayerYpos++;
            break;
         case ConsoleKey.LeftArrow:
            newCurrentPlayerYpos--;
            break;
         case ConsoleKey.Enter:
            if (Players[currentPlayer].CD == 0)
            {
               Skills.WhoPressed(currentPlayer, Players, currentPlayerXpos, currentPlayerYpos, maze);
               Players[currentPlayer].CD = coolDowns[currentPlayer];
            }
            break;
      }


      if (!Collision(newCurrentPlayerXpos, newCurrentPlayerYpos) && !Obstacle(pressedKey, currentPlayerXpos, currentPlayerYpos, newCurrentPlayerXpos, newCurrentPlayerYpos, Players, currentPlayer))
      {
         if (maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.RiddleTrap && Players[currentPlayer].Intellect < 5)
         {
            RiddleTrap(Players, currentPlayer);
         }

         if (maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.OilTrap && Players[currentPlayer].Agility < 5)
         {
            Players[currentPlayer].Speed = 1;
            AnsiConsole.MarkupLine("Oh oh, caiste en una trampa de aceite, tu velocidad ahora solo es de 1");
         }

         if (maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.BurstTrap && Players[currentPlayer].Strength < 5)
         {
            Players[currentPlayer].Health -= 40;
         }

         if (maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.ArborGold)
         {
            Players[currentPlayer].Intellect = 5;
            Players[currentPlayer].Health += 10;
         }

         if (maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.DornishRed)
         {
            Players[currentPlayer].Agility = 5;
            Players[currentPlayer].Speed += 1;
         }

         if (maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.Throne)
         {
            Console.Clear();
            AnsiConsole.Write(new Markup("[bold yellow]Felicidades, te hiciste con el [italic]Trono de Hierro![/][/]\n"));
            Environment.Exit(0);
         }
         if (pressedKey == ConsoleKey.UpArrow || pressedKey == ConsoleKey.DownArrow || pressedKey == ConsoleKey.LeftArrow || pressedKey == ConsoleKey.RightArrow)
         {
            maze[currentPlayerXpos, currentPlayerYpos] = Cell.Floor;
            Players[currentPlayer].Xpos = newCurrentPlayerXpos;
            Players[currentPlayer].Ypos = newCurrentPlayerYpos;
            Players[currentPlayer].MovesLeft--;
            Players[currentPlayer].CD--;
            if (Players[currentPlayer].CD < 0) Players[currentPlayer].CD = 0;

         }
      }
   }

   public static bool Collision(int newCurrentPlayerXpos, int newCurrentPlayerYpos)
   {
      if (newCurrentPlayerXpos < 0 || newCurrentPlayerXpos >= maze.GetLength(0) || newCurrentPlayerYpos < 0 || newCurrentPlayerYpos >= maze.GetLength(1))
      {
         return true;
      }
      return maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.Wall;
   }

   public static bool Obstacle(ConsoleKey pressedKey, int currentPlayerXpos, int currentPlayerYpos, int newCurrentPlayerXpos, int newCurrentPlayerYpos, List<Player.Player> Players, int currentPlayer)
   {
      if (InsideOfBounds(currentPlayerXpos + 1, currentPlayerYpos) && maze[currentPlayerXpos + 1, currentPlayerYpos] == Cell.BricksObstacle
         && pressedKey == ConsoleKey.Spacebar)
      {
         maze[currentPlayerXpos + 1, currentPlayerYpos] = Cell.Floor;
         Players[currentPlayer].MovesLeft--;
         Players[currentPlayer].CD--;
         return false;
      } // Arriba
      else if (InsideOfBounds(currentPlayerXpos - 1, currentPlayerYpos) && maze[currentPlayerXpos - 1, currentPlayerYpos] == Cell.BricksObstacle
         && pressedKey == ConsoleKey.Spacebar)
      {
         maze[currentPlayerXpos - 1, currentPlayerYpos] = Cell.Floor;
         Players[currentPlayer].MovesLeft--;
         Players[currentPlayer].CD--;
         return false;
      }// Abajo
      else if (InsideOfBounds(currentPlayerXpos, currentPlayerYpos + 1) && maze[currentPlayerXpos, currentPlayerYpos + 1] == Cell.BricksObstacle
         && pressedKey == ConsoleKey.Spacebar)
      {
         maze[currentPlayerXpos, currentPlayerYpos + 1] = Cell.Floor;
         Players[currentPlayer].MovesLeft--;
         Players[currentPlayer].CD--;
         return false;
      }  // Izquierda
      else if (InsideOfBounds(currentPlayerXpos, currentPlayerYpos - 1) && maze[currentPlayerXpos, currentPlayerYpos - 1] == Cell.BricksObstacle
         && pressedKey == ConsoleKey.Spacebar)
      {
         maze[currentPlayerXpos, currentPlayerYpos - 1] = Cell.Floor;
         Players[currentPlayer].MovesLeft--;
         Players[currentPlayer].CD--;
         return false;
      } // Derecha 
      return maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.BricksObstacle;
   }

   public static bool InsideOfBounds(int currentPlayerXpos, int currentPlayerYpos)
   {
      if (currentPlayerXpos >= 0 && currentPlayerXpos < maze.GetLength(0) && currentPlayerYpos >= 0 && currentPlayerYpos < maze.GetLength(1))
      {
         return true;
      }
      return false;
   }

   public static void RiddleTrap(List<Player.Player> Players, int currentPlayer)
   {
      Console.Clear();
      Random random = new Random();

      AnsiConsole.MarkupLine("[bold green]Acertijo!![/]");

      switch (random.Next(1, 4))
      {
         case 1:
            var optionsRiddle1 = new[]
            {
                     "Un guante",
                     "Una estrella",
                     "Una marioneta",
                     "Un pie",
               };

            var riddle1 = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
                  .Title("Tengo cuatro dedos y un pulgar, pero no soy una mano. ¿Qué soy?")
                  .AddChoices(optionsRiddle1)
            );

            if (riddle1 == "Un guante") AnsiConsole.MarkupLine("[bold green]Correcto!![/]");
            else
            {
               AnsiConsole.MarkupLine("[bold red]Ups... incorrecto[/]");
               Players[currentPlayer].Health -= 25;
               if (Players[currentPlayer].Speed > 1)
               {
                  Players[currentPlayer].Speed -= 1;
               }
               else Players[currentPlayer].Speed = 1;

            }
            break;

         case 2:
            var optionsRiddle2 = new[]
            {
                  "Un atlas",
                  "Un mapa",
                  "Un globo terráqueo",
                  "Una pintura",
               };

            var riddle2 = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
                  .Title("Tengo ciudades, pero no casas. Tengo montañas, pero no árboles. Tengo agua, pero no peces. ¿Qué soy?")
                  .AddChoices(optionsRiddle2)
            );

            if (riddle2 == "Un mapa") AnsiConsole.MarkupLine("[bold green]Correcto!![/]");
            else
            {
               AnsiConsole.MarkupLine("[bold red]Ups... incorrecto[/]");
               Players[currentPlayer].Health -= 15;
               Players[currentPlayer].Speed -= 1;
            }
            break;

         case 3:
            var optionsRiddle3 = new[]
            {
                  "Todos son caballeros",
                  "A y B son caballeros, C es truhan",
                  "A y C son caballeros, B es truhan",
                  "B y C son caballeros, A es truhan",
                  "No sé, suspendí lógica",
               };

            var riddle3 = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
                  .Title("En una isla hay dos tipos de habitantes: los caballeros, que siempre dicen la verdad, y los truhanes, que siempre mienten.\n" +
                     "Te encuentras con tres habitantes: A, B y C. Sabes lo siguiente:\n" +
                     "A dice: Yo soy un caballero o B es un caballero.\n" +
                     "B dice: Si A es un caballero, entonces yo soy un caballero.\n" +
                     "C dice: B es un caballero o A es un truhan.\n" +
                     "¿Qué tipo de habitantes son A, B y C?")
                  .AddChoices(optionsRiddle3)
            );

            if (riddle3 == "A y B son caballeros, C es truhan") AnsiConsole.MarkupLine("[bold green]Correcto!![/]");
            else
            {
               AnsiConsole.MarkupLine("[bold red]Ups... incorrecto[/]");
               Players[currentPlayer].Health -= 15;
               Players[currentPlayer].Speed -= 1;
            }
            break;
      }
   }
}
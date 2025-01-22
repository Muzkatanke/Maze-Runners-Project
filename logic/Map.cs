using Microsoft.VisualBasic;
using Spectre.Console;
using Game.Player;
namespace Game.Map;

public enum Cell
{
   Wall, Floor, Trap, BricksObstacle, ChainsObstacle, Throne, DornishRed, ArborGold
};

public class Map
{
   public static Dictionary<Cell, string> CellSymbols = new Dictionary<Cell, string>
   {
      { Cell.Wall, "🟥" },
      { Cell.Floor, "◾" },
      { Cell.Trap, "💣" },
      { Cell.BricksObstacle, "🧱" },
      { Cell.ChainsObstacle, "⛓️" },
      { Cell.Throne, "👑"},
      { Cell.DornishRed, "🍷"},
      { Cell.ArborGold, "🍸"},
   };

   public static Cell[,] maze = new Cell[10, 10]
   {
      { Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Floor, Cell.Floor, Cell.Floor, Cell.Trap, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Trap, Cell.BricksObstacle, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.DornishRed, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Floor, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Wall, Cell.DornishRed, Cell.Wall,Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall,Cell.Floor, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.ArborGold, Cell.Floor, Cell.ChainsObstacle, Cell.Floor,Cell.Floor, Cell.Floor, Cell.ArborGold, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall,Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Floor, Cell.Trap, Cell.Floor,Cell.ChainsObstacle, Cell.Trap, Cell.Floor, Cell.Wall, Cell.Wall },
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
   public static void MovePlayer(ConsoleKey pressedKey, int currentPlayerXpos, int currentPlayerYpos, int currentPlayer)
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
      }


      if (!Collision(newCurrentPlayerXpos, newCurrentPlayerYpos) && !Obstacle(pressedKey, currentPlayerXpos, currentPlayerYpos, newCurrentPlayerXpos, newCurrentPlayerYpos))
      {
         if (maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.Throne)
         {
            Console.Clear();
            AnsiConsole.Write(new Markup("[bold yellow]Felicidades, te hiciste con el [italic]Trono de Hierro![/][/]\n"));
            Environment.Exit(0);
         }
         if (currentPlayer == 0)
         {
            maze[currentPlayerXpos, currentPlayerYpos] = Cell.Floor;
            Program.Players[0].Xpos = newCurrentPlayerXpos;
            Program.Players[0].Ypos = newCurrentPlayerYpos;
            Program.Players[0].MovesLeft--;
         }
         else
         {
            maze[currentPlayerXpos, currentPlayerYpos] = Cell.Floor;
            Program.Players[1].Xpos = newCurrentPlayerXpos;
            Program.Players[1].Ypos = newCurrentPlayerYpos;
            Program.Players[1].MovesLeft--;
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

   public static bool Obstacle(ConsoleKey pressedKey, int currentPlayerXpos, int currentPlayerYpos, int newCurrentPlayerXpos, int newCurrentPlayerYpos)
   {
      if (InsideOfBounds(currentPlayerXpos + 1, currentPlayerYpos) && maze[currentPlayerXpos + 1, currentPlayerYpos] == Cell.BricksObstacle
         && pressedKey == ConsoleKey.Spacebar)
         {
            maze[currentPlayerXpos + 1, currentPlayerYpos] = Cell.Floor;
            return false;
         } // Arriba
      else if (InsideOfBounds(currentPlayerXpos - 1, currentPlayerYpos) && maze[currentPlayerXpos - 1, currentPlayerYpos] == Cell.BricksObstacle 
         && pressedKey == ConsoleKey.Spacebar)
         {
            maze[currentPlayerXpos - 1, currentPlayerYpos] = Cell.Floor;
            return false;
         }// Abajo
      else if (InsideOfBounds(currentPlayerXpos, currentPlayerYpos + 1) && maze[currentPlayerXpos, currentPlayerYpos + 1] == Cell.BricksObstacle
         && pressedKey == ConsoleKey.Spacebar)
         {
            maze[currentPlayerXpos, currentPlayerYpos + 1] = Cell.Floor;
            return false;
         }  // Izquierda
      else if (InsideOfBounds(currentPlayerXpos, currentPlayerYpos - 1) && maze[currentPlayerXpos, currentPlayerYpos - 1] == Cell.BricksObstacle
         && pressedKey == ConsoleKey.Spacebar)
         {
            maze[currentPlayerXpos, currentPlayerYpos - 1] = Cell.Floor;
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

}
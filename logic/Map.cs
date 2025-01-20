using Microsoft.VisualBasic;
using Spectre.Console;
using Game.Player;
namespace Game.Map;

public enum Cell
{
   Wall, Floor, Trap, Obstacle, Throne
};

public class Map
{
   public static Dictionary<Cell, string> CellSymbols = new Dictionary<Cell, string>
   {
      { Cell.Wall, "🔲" },
      { Cell.Floor, "🍃" },
      { Cell.Trap, "💣" },
      { Cell.Obstacle, "🚫" },
      { Cell.Throne, "👑"}
   };

   public static Cell[,] maze = new Cell[10, 10]
   {
      { Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Floor, Cell.Floor, Cell.Floor, Cell.Trap, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Trap, Cell.Obstacle, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Floor, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Wall, Cell.Floor, Cell.Wall,Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall,Cell.Floor, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Floor, Cell.Obstacle, Cell.Floor,Cell.Floor, Cell.Floor, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Wall, Cell.Floor, Cell.Wall, Cell.Wall,Cell.Floor, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Floor, Cell.Floor, Cell.Trap, Cell.Floor,Cell.Obstacle, Cell.Trap, Cell.Floor, Cell.Wall, Cell.Wall },
      { Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall, Cell.Wall,Cell.Wall, Cell.Wall, Cell.Throne, Cell.Wall, Cell.Wall },
   };

   public static void PrintMaze(Cell[,] maze, Player.Player player1, Player.Player player2)
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
            if (i == player1.Xpos && j == player1.Ypos)
            {
               rowContent.Add($"[green]{player1.Symbol}[/]");
            }
            else if (i == player2.Xpos && j == player2.Ypos)
            {
               rowContent.Add($"[red]{player2.Symbol}[/]");
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

      if (maze[newCurrentPlayerXpos, newCurrentPlayerYpos] == Cell.Throne)
      {
         Console.Clear();
         AnsiConsole.Write(new Markup("[blue]Game Over![/]"));
         Environment.Exit(0);
      }
      if (!Collision(newCurrentPlayerXpos, newCurrentPlayerYpos))
      {
         if (currentPlayer == 0)
         {
            maze[currentPlayerXpos, currentPlayerYpos] = Cell.Floor;
            Program.Snow.Xpos = newCurrentPlayerXpos;
            Program.Snow.Ypos = newCurrentPlayerYpos;
            Program.Snow.MovesLeft--;
         }
         else
         {
            maze[currentPlayerXpos, currentPlayerYpos] = Cell.Floor;
            Program.Tyrion.Xpos = newCurrentPlayerXpos;
            Program.Tyrion.Ypos = newCurrentPlayerYpos;
            Program.Tyrion.MovesLeft--;
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
}


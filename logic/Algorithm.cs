using Game.Player;
using Game.Menu;
using Game.Map;
using Spectre.Console;

public enum Cell
{
    Wall, Floor, BurstTrap, RiddleTrap, OilTrap, BricksObstacle, Throne, DornishRed, ArborGold
};
public class Algorithm
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

    public static Cell[,] maze = new Cell[25, 25];
    
    public static void InitializeMaze()
    {
        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                maze[i, j] = Cell.Wall;
            }
        }
    }

    public static void GenerateMaze(int PosX, int PosY)
    {
        Random random = new Random();
        maze[PosX, PosY] = Cell.Floor;

        List<(int, int)> Directions = new List<(int, int)>
        {
            (PosX, PosY + 2),
            (PosX, PosY - 2),
            (PosX + 2, PosY),
            (PosX - 2, PosY),
        };
        Directions = Directions.OrderBy(a => random.Next()).ToList();

        foreach (var (newPosX, newPosY) in Directions)
        {
            if (IsValid(newPosX, newPosY) && maze[newPosX, newPosY] == Cell.Wall)
            {
                RemoveWall(PosX, PosY, newPosX, newPosY);
                GenerateMaze(newPosX, newPosY);
            }
        }
    }
    public static void RemoveWall(int PosX, int PosY, int newPosX, int newPosY)
    {
        if (newPosX == PosX && newPosY == PosY + 2)
        {
            maze[PosX, PosY + 1] = Cell.Floor;
        }
        else if (newPosX == PosX && newPosY == PosY - 2)
        {
            maze[PosX, PosY - 1] = Cell.Floor;
        }
        else if (newPosX == PosX + 2 && newPosY == PosY)
        {
            maze[PosX + 1, PosY] = Cell.Floor;
        }
        else if (newPosX == PosX - 2 && newPosY == PosY)
        {
            maze[PosX - 1, PosY] = Cell.Floor;
        }
    }
    public static bool IsValid(int x, int y)
    {
        return x >= 0 && y >= 0 && x < maze.GetLength(0) && y < maze.GetLength(1);
    }


    public static void PrintMaze(List<Player> Players)
    {
        Console.Clear();

        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == Players[0].Xpos && j == Players[0].Ypos)
                {
                    Console.Write(Players[0].Symbol);
                }
                else if (i == Players[1].Xpos && j == Players[1].Ypos)
                {
                    Console.Write(Players[1].Symbol);
                }
                else Console.Write(CellSymbols[maze[i, j]] + " ");

            }
            Console.WriteLine();
        }
    }
}



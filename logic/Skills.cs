using Game.Map;
using Game.Player;
public class Skills
{
    public static void WhoPressed(int currentPlayer, List<Player> Players, int currentPlayerXpos, int currentPlayerYpos, Cell[,] maze)
    {
        switch (Players[currentPlayer].Symbol)
        {
            case "🐺":
                Longclaw(currentPlayer, currentPlayerXpos, currentPlayerYpos, maze, Players);
                break;
            case "🦁":
                WizardTyrion(currentPlayer, currentPlayerXpos, currentPlayerYpos, maze, Players);
                break;
            case "🐲":
                MotherOfDragons(currentPlayer, Players);
                break;
            case "🎭":
                Insight(currentPlayerXpos, currentPlayerYpos, maze);
                break;
            case "🦌":
                DeerVigor(currentPlayer, Players);
                break;
            case "💀":
                Curse(currentPlayer, Players);
                break;
        }
    }

    public static void Longclaw(int currentPlayer, int currentPlayerXpos, int currentPlayerYpos, Cell[,] maze, List<Player> Players)
    {

        int enemyPlayer = currentPlayer == 0 ? 1 : 0;

        List<(int, int)> positionsToCheck = new List<(int, int)>
        {
            (currentPlayerXpos, currentPlayerYpos + 1),
            (currentPlayerXpos, currentPlayerYpos - 1),
            (currentPlayerXpos + 1, currentPlayerYpos),
            (currentPlayerXpos - 1, currentPlayerYpos),
            (currentPlayerXpos - 1, currentPlayerYpos + 1),
            (currentPlayerXpos + 1, currentPlayerYpos - 1),
            (currentPlayerXpos + 1, currentPlayerYpos + 1),
            (currentPlayerXpos - 1, currentPlayerYpos - 1)
        };

        bool attacked = false;

        foreach (var (x, y) in positionsToCheck)
        {
            if (Map.InsideOfBounds(x, y) && maze[x, y] == maze[Players[enemyPlayer].Xpos, Players[enemyPlayer].Ypos])
            {
                Players[enemyPlayer].Health -= 30;
                Players[currentPlayer].MovesLeft--;
                attacked = true;
                break;
            }
        }
        if (!attacked)
        {
            Players[currentPlayer].CD = 0;
        }
    }

    public static void WizardTyrion(int currentPlayer, int currentPlayerXpos, int currentPlayerYpos, Cell[,] maze, List<Player> Players)
    {
        Random random = new Random();
    
        List<(int, int)> positionsToCheck = new List<(int, int)>
        {
            (currentPlayerXpos, currentPlayerYpos + 1),
            (currentPlayerXpos, currentPlayerYpos - 1),
            (currentPlayerXpos + 1, currentPlayerYpos),
            (currentPlayerXpos - 1, currentPlayerYpos),
        };

        foreach (var (x, y) in positionsToCheck)
        {
            if (Map.InsideOfBounds(x, y) && maze[x, y] == Cell.Floor)
            {
                switch(random.Next(1, 3))
                {
                    case 1:
                        maze[x, y] = Cell.ArborGold;
                        break;
                    case 2:
                        maze[x, y] = Cell.DornishRed;
                        break;
                }
                Players[currentPlayer].MovesLeft--;
                break;
            }
        }
    }
    public static void MotherOfDragons(int currentPlayer, List<Player> Players)
    {
        Players[currentPlayer].Symbol = "🐉";
        Players[currentPlayer].Health = 1000;
        Players[currentPlayer].Speed = 6;
        Players[currentPlayer].MovesLeft = Players[currentPlayer].Speed;

    }
    public static void Insight(int currentPlayerXpos, int currentPlayerYpos, Cell[,] maze)
    {
        List<(int, int)> positionsToCheck = new List<(int, int)>
        {
            (currentPlayerXpos, currentPlayerYpos + 1),
            (currentPlayerXpos, currentPlayerYpos - 1),
            (currentPlayerXpos + 1, currentPlayerYpos),
            (currentPlayerXpos - 1, currentPlayerYpos),
        };

        foreach (var (x, y) in positionsToCheck)
        {
            if (Map.InsideOfBounds(x, y) && maze[x, y] == Cell.BurstTrap || maze[x, y] == Cell.RiddleTrap || maze[x, y] == Cell.OilTrap)
            {
                maze[x, y] = Cell.Floor;
                break;
            }
        }
    }
    public static void DeerVigor(int currentPlayer, List<Player> Players)
    {
        Players[currentPlayer].Health += 30;
        if (Players[currentPlayer].Health > 100) Players[currentPlayer].Health = 100;

        if (Players[currentPlayer].Health >= 80)
        {
            Players[currentPlayer].Speed += 1;
            Players[currentPlayer].MovesLeft = Players[currentPlayer].Speed;
        }
        if (Players[currentPlayer].Speed > 4) Players[currentPlayer].Speed = 4;


        Players[currentPlayer].MovesLeft--;
    }
    public static void Curse(int currentPlayer, List<Player> Players)
    {
        int enemyPlayer = currentPlayer == 0 ? 1 : 0;
        Players[enemyPlayer].Health-=25;
        Players[enemyPlayer].Speed--;

        if(Players[enemyPlayer].Speed == 1) Players[enemyPlayer].Speed = 1;

        Players[enemyPlayer].MovesLeft = Players[enemyPlayer].Speed;
        Players[currentPlayer].MovesLeft--;
    }
}
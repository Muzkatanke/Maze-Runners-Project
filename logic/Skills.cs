using Game.Map;
using Game.Player;
public class Skills
{
    public static void WhoPressed(int currentPlayer, List<Player> Players, int currentPlayerXpos, int currentPlayerYpos, Cell[,] maze)
    {
        switch (Players[currentPlayer].Symbol)
        {
            case "🐺":
                Longclaw2(currentPlayer, currentPlayerXpos, currentPlayerYpos, maze, Players);
                break;
            case "🦁":
                Charlatan();
                break;
            case "🐉":
                MotherOfDragons();
                break;
            case "🎭":
                Insight();
                break;
            case "🦌":
                LastBreath(currentPlayer, Players);
                break;
            case "💀":
                Invoke();
                break;
        }
    }

    public static void Longclaw(int currentPlayer, int currentPlayerXpos, int currentPlayerYpos, Cell[,] maze, List<Player> Players)
    {
        bool attacked = false;
        int enemyPlayer = currentPlayer == 0 ? 1 : 0;

        // Check all directions for the presence of another player
        if (Map.InsideOfBounds(currentPlayerXpos + 1, currentPlayerYpos) && maze[currentPlayerXpos + 1, currentPlayerYpos] == maze[Players[enemyPlayer].Xpos, Players[enemyPlayer].Ypos])
        {
            Players[enemyPlayer].Health -= 30;
            Players[currentPlayer].MovesLeft--;
            attacked = true;
        }
        else if (Map.InsideOfBounds(currentPlayerXpos - 1, currentPlayerYpos) && maze[currentPlayerXpos - 1, currentPlayerYpos] == maze[Players[enemyPlayer].Xpos, Players[enemyPlayer].Ypos])
        {
            Players[enemyPlayer].Health -= 30;
            Players[currentPlayer].MovesLeft--;
            attacked = true;
        }
        else if (Map.InsideOfBounds(currentPlayerXpos, currentPlayerYpos + 1) && maze[currentPlayerXpos, currentPlayerYpos + 1] == maze[Players[enemyPlayer].Xpos, Players[enemyPlayer].Ypos])
        {
            Players[enemyPlayer].Health -= 30;
            Players[currentPlayer].MovesLeft--;
            attacked = true;
        }
        else if (Map.InsideOfBounds(currentPlayerXpos, currentPlayerYpos - 1) && maze[currentPlayerXpos, currentPlayerYpos - 1] == maze[Players[enemyPlayer].Xpos, Players[enemyPlayer].Ypos])
        {
            Players[enemyPlayer].Health -= 30;
            Players[currentPlayer].MovesLeft--;
            attacked = true;
        }

        if (!attacked)
        {
            Players[currentPlayer].CD = 0;
        }
    }

    public static void Longclaw2(int currentPlayer, int currentPlayerXpos, int currentPlayerYpos, Cell[,] maze, List<Player> Players)
    {

        int enemyPlayer = currentPlayer == 0 ? 1 : 0;

        List<(int, int)> positionsToCheck = new List<(int, int)>
        {
            (currentPlayerXpos + 1, currentPlayerYpos),
            (currentPlayerXpos - 1, currentPlayerYpos),
            (currentPlayerXpos, currentPlayerYpos + 1),
            (currentPlayerXpos, currentPlayerYpos - 1)
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

    public static void LastBreath(int currentPlayer, List<Player> Players)
    {
        Players[currentPlayer].Strength = 5;
        Players[currentPlayer].Agility = 5;
        Players[currentPlayer].Intellect = 5;
        Players[currentPlayer].Health = 50;
        Players[currentPlayer].MovesLeft--;
    }
    public static void Charlatan()
    {

    }
    public static void MotherOfDragons()
    {

    }
    public static void Insight()
    {

    }
    public static void Invoke()
    {

    }


}/*
else if (Map.InsideOfBounds(currentPlayerXpos - 1, currentPlayerYpos + 1) && maze[currentPlayerXpos - 1, currentPlayerYpos + 1] == maze[Players[1].Xpos, Players[1].Ypos])
            {
                Players[0].Health-=30;
                Players[1].MovesLeft--;
            }
            else if (Map.InsideOfBounds(currentPlayerXpos + 1, currentPlayerYpos - 1) && maze[currentPlayerXpos + 1, currentPlayerYpos - 1] == maze[Players[1].Xpos, Players[1].Ypos])
            {
                Players[0].Health-=30;
                Players[1].MovesLeft--;
            }
            else if (Map.InsideOfBounds(currentPlayerXpos + 1, currentPlayerYpos + 1) && maze[currentPlayerXpos + 1, currentPlayerYpos + 1] == maze[Players[1].Xpos, Players[1].Ypos])
            {
                Players[0].Health-=30;
                Players[1].MovesLeft--;
            }
            else if (Map.InsideOfBounds(currentPlayerXpos - 1, currentPlayerYpos - 1) && maze[currentPlayerXpos - 1, currentPlayerYpos - 1] == maze[Players[1].Xpos, Players[1].Ypos])
            {
                Players[0].Health-=30;
                Players[1].MovesLeft--;
            }
            */
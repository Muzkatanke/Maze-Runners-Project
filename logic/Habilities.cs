using Game.Map;
using Game.Player;
public class Habilities
{
    public static void WhoPressed(int currentPlayer, List<Player> Players, int currentPlayerXpos, int currentPlayerYpos, Cell[,] maze)
    {
        switch (Players[currentPlayer].Symbol)
        {
            case "🐺":
                Longclaw(currentPlayer, currentPlayerXpos, currentPlayerYpos, maze, Players);
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
                Rebelion();
                break;
            case "💀":
                Invoke();
                break;
        }
    }

    public static void Longclaw(int currentPlayerXpos, int currentPlayerYpos, int currentPlayer, Cell[,] maze, List<Player> Players )
    {
    
        if(currentPlayer == 0)
        {
            if (Map.InsideOfBounds(currentPlayerXpos + 1, currentPlayerYpos) && maze[currentPlayerXpos + 1, currentPlayerYpos] == maze[Players[1].Xpos, Players[1].Ypos])
            {
                Players[1].Health-=30;
            } // Arriba
            else if (Map.InsideOfBounds(currentPlayerXpos - 1, currentPlayerYpos) && maze[currentPlayerXpos - 1, currentPlayerYpos] == maze[Players[1].Xpos, Players[1].Ypos])
            {
                Players[1].Health-=30;
            }
            else if (Map.InsideOfBounds(currentPlayerXpos, currentPlayerYpos + 1) && maze[currentPlayerXpos, currentPlayerYpos + 1] == maze[Players[1].Xpos, Players[1].Ypos])
            {
                Players[1].Health-=30;
            }
            else if (Map.InsideOfBounds(currentPlayerXpos, currentPlayerYpos - 1) && maze[currentPlayerXpos, currentPlayerYpos - 1] == maze[Players[1].Xpos, Players[1].Ypos])
            {
                Players[1].Health-=30;
            }
            Players[0].MovesLeft--;
        }

        else if (currentPlayer == 1)
        {
            if (Map.InsideOfBounds(currentPlayerXpos + 1, currentPlayerYpos) && maze[currentPlayerXpos + 1, currentPlayerYpos] == maze[Players[0].Xpos, Players[0].Ypos])
            {
                Players[0].Health-=30;
            }
            else if (Map.InsideOfBounds(currentPlayerXpos - 1, currentPlayerYpos) && maze[currentPlayerXpos - 1, currentPlayerYpos] == maze[Players[0].Xpos, Players[0].Ypos])
            {
                Players[0].Health-=30;
            }
            else if (Map.InsideOfBounds(currentPlayerXpos, currentPlayerYpos + 1) && maze[currentPlayerXpos, currentPlayerYpos + 1] == maze[Players[0].Xpos, Players[0].Ypos])
            {
                Players[0].Health-=30;
            }
            else if (Map.InsideOfBounds(currentPlayerXpos, currentPlayerYpos - 1) && maze[currentPlayerXpos, currentPlayerYpos - 1] == maze[Players[0].Xpos, Players[0].Ypos])
            {
                Players[0].Health-=30;
            }
            Players[1].MovesLeft--;
        }
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
    public static void Rebelion()
    {

    }
    public static void Invoke()
    {

    }


}
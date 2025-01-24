using Game.Player;
public class Habilities
{
    public static void WhoPressed(int currentPlayer, List<Player> Players)
    {
        switch(Players[currentPlayer].Symbol)
        {
            case "🐺":
                Longclaw();
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

    public static void Longclaw()
    {

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
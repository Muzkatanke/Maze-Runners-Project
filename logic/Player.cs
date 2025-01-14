namespace Game.Player;
public class Player
{
    public int Xpos;
    public int Ypos;
    public string Symbol;
    public int Speed;
    public int CD;
    public int MovesLeft;

    public Player(int x, int y, string symbol, int speed, int cd)
    {
        Xpos = x;
        Ypos = y;
        Symbol = symbol;
        Speed = speed;
        CD = cd;
        MovesLeft = speed;
    }
}
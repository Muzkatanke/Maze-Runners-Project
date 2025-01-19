namespace Game.Player;
public class Player
{
    public int Xpos;
    public int Ypos;
    public string Symbol;
    public int Speed;
    public int CD;
    public int MovesLeft;
    public int Strength;
    public int Agility;
    public int Intellect;
    public string Description;

    public Player(int x, int y, string symbol, int speed, int cd, int strength, int agility, int intellect, string description)
    {
        Xpos = x;
        Ypos = y;
        Symbol = symbol;
        Speed = speed;
        CD = cd;
        MovesLeft = speed;
        Strength = strength;
        Agility = agility;
        Intellect = intellect;
        Description = description;
    }
}
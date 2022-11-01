namespace LAB2_Checkers.Models;

public class Player
{
    public string Identifier { get; }
    public string Nickname { get; }

    public Player(string nickname, string identifier)
    {
        Nickname = nickname;
        Identifier = identifier;
    }
        
}
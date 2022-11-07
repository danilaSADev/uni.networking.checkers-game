namespace CheckersServer.Models;

public class Player
{
    public string IpAddress { get; set; }
    public int Port { get; set; }
    public int Score { get; set; }
    public string Identifier { get; set; }
    public string Nickname { get; set; }
}
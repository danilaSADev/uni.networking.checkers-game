using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;

namespace CheckersServer.Models;

public class Player : IDisposable
{
    public int Score { get; set; } = 0;
    public string Identifier { get; set; }
    public string Nickname { get; set; }
    public Socket GameSocket { get; init; }
    
    public void Notify(string command, string message)
    {
        var request = new Request { Command = command, Payload = message };
        GameSocket.Send(UniversalConverter.ConvertObject(request));
    }
    
    public void Dispose()
    {
        GameSocket.Shutdown(SocketShutdown.Both);
        GameSocket.Close();
        GameSocket.Dispose();
    }
}
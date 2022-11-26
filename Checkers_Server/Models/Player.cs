using System.Net;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;
using Domain.Networking.Handlers.Models;

namespace CheckersServer.Models;

public class Player 
{
    public int Score { get; set; } = 0;
    public string Identifier { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
    public string IpAddress { get; set; }
    public int Port { get; set; }
    
    public void Notify(string command, string message)
    {
        // Initializing player socket
        var endpoint = new IPEndPoint(IPAddress.Parse(IpAddress), Port);
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        socket.SendTimeout = ServerInfo.MaxClientResponseTime;
        socket.Connect(endpoint);

        var request = new Request { Command = command, Payload = message };
        socket.Send(UniversalConverter.ConvertObject(request));

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
using System.Net;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using LAB2_Checkers.Models;

namespace LAB2_Checkers;

public class SocketListener
{
    private static List<Player> _players = new();
    private static Socket _socket;
    private static bool _isRunning = true;
    
    public static int Main(String[] args)
    {
        StartServer();
        return 0;
    }

    private static ServerResponse ParseRequest(ClientRequest request)
    {
        ServerResponse response = new ServerResponse
        {
            Status = "UNKNOWN",
            Payload = string.Empty
        };
        
        if (request.Command.Equals(ClientCommands.ConnectToServer))
        {
            Console.WriteLine("Established connection!");
            var id = IdentifierGenerator.GenerateIdentifier(request.Payload);
            var player = new Player(request.Payload, id);
            
            _players.Add(player);
            
            response = new ServerResponse
            {
                Status = "OK",
                Payload = id
            };
            return response;
        }

        if (request.Command.Equals(ClientCommands.DisconnectFromServer))
        {
            Console.WriteLine("Disconnect from server!");
            var player = _players.FirstOrDefault(p => p.Identifier == request.Payload);
            if (player != null)
                _players.Remove(player);
            
            response = new ServerResponse
            {
                Status = "OK",
                Payload = string.Empty
            };
            return response;
        }
        
        return response;
    }
    
    public static void StartServer()
    {
        var ipPoint = new IPEndPoint(
            IPAddress.Parse(ServerInfo.IpAddress), 
            ServerInfo.Port
            );
        
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try {
            _socket.Bind(ipPoint);
            _socket.Listen(10);
            
            while (_isRunning)
            {
                Socket handler = _socket.Accept();

                byte[] data = new byte[256];
                handler.Receive(data);
                
                var request = UniversalConverter.ConvertBytes<ClientRequest>(data);
                Console.WriteLine(DateTime.Now.ToShortTimeString() + " : " + request.Payload);
                
                var response = ParseRequest(request);
                handler.Send(UniversalConverter.ConvertObject(response));
                
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message} {ex.Source}");
        }
    }
}
using System.Net;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models.Server;
using Domain.Networking.Handlers;
using Domain.Networking.Handlers.Models;

namespace CheckersServer;

public class ServerSocketListener
{
    private readonly HandlerBinder _binder;
    private readonly bool _isRunning = true;

    public ServerSocketListener(HandlerBinder binder)
    {
        _binder = binder;
    }

    public void StartServer()
    {
        var ipPoint = new IPEndPoint(
            IPAddress.Parse(ServerInfo.IpAddress),
            ServerInfo.Port
        );

        var socket = ServerInfo.SharedSocket;
        try
        {
            socket.Bind(ipPoint);
            socket.Listen(12);

            Console.WriteLine($"Server now listens on : {(socket.LocalEndPoint as IPEndPoint)?.Address}" );
            
            while (_isRunning)
            {
                var handler = socket.Accept();

                var data = new byte[65536];
                handler.Receive(data);
                var remoteEndpoint = (handler.RemoteEndPoint as IPEndPoint);
                Console.WriteLine($"Received from : {remoteEndpoint?.Address}:{remoteEndpoint?.Port}" );
                
                var request = UniversalConverter.ConvertBytes<Request>(data);
                Console.WriteLine(DateTime.Now.ToShortTimeString() + " : " + request.Payload);

                var response = _binder.Handle(request);
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
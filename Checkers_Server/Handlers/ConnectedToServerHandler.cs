using System.Net;
using System.Net.Sockets;
using CheckersServer.Common;
using CheckersServer.Interfaces;
using CheckersServer.Models;
using Domain.Models;
using Domain.Models.Server;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersServer.Handlers;

public class ConnectedToServerHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;

    public ConnectedToServerHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }

    public Response Handle(string payload)
    {
        Console.WriteLine("Established connection!");
        var deserializedPayload = JsonConvert.DeserializeObject<EstablishConnectionPayload>(payload);

        // TODO : check whether player password matches
        // TODO : write data to database        
        var id = IdentifierGenerator.Generate(deserializedPayload.Username);
                
        // Initializing player socket
        var endpoint = new IPEndPoint(IPAddress.Parse(deserializedPayload.IpAddress), deserializedPayload.Port);
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        socket.SendTimeout = ServerInfo.MaxClientResponseTime;
        socket.Connect(endpoint);
        
        var player = new Player
        {
            Nickname = deserializedPayload.Username,
            Identifier = id,
            GameSocket = socket
        };

        _multiplayerService.AddPlayer(player);

        var responsePayload = new ConnectionEstablishedPayload
        {
            UserIdentifier = id
        };

        var response = new Response
        {
            Status = "OK",
            Payload = JsonConvert.SerializeObject(responsePayload)
        };

        return response;
    }
}
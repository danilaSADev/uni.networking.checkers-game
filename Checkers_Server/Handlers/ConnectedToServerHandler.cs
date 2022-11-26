using CheckersServer.Common;
using CheckersServer.Interfaces;
using CheckersServer.Models;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
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

        var id = IdentifierGenerator.Generate(deserializedPayload.Username);
                
        var player = new Player
        {
            Username = deserializedPayload?.Username ?? string.Empty,
            Identifier = id,
            Password = deserializedPayload.Password,
            IpAddress = deserializedPayload.IpAddress,
            Port = deserializedPayload.Port
        };

        var created = _multiplayerService.TryAddPlayer(player);
        if (!created)
            return Response.Failed;

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
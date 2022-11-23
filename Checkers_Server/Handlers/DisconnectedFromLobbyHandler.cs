using CheckersServer.Interfaces;
using Domain.Models;
using Domain.Models.Server;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersServer.Handlers;

public class DisconnectedFromLobbyHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;

    public DisconnectedFromLobbyHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    
    public Response Handle(string payload)
    {
        var deserializedPayload = JsonConvert.DeserializeObject<DisconnectFromLobbyPayload>(payload);

        var lobbyId = deserializedPayload.LobbyIdentifier;
        var userId = deserializedPayload.UserIdentifier;
        
        _multiplayerService.DisconnectFromRoom(userId, lobbyId);

        var responsePayload = new DisconnectedFromLobbyPayload()
        {
        };

        var response = new Response
        {
            Status = "OK",
            Payload = JsonConvert.SerializeObject(responsePayload)
        };

        return response;
    }
}
﻿using CheckersServer.Interfaces;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersServer.Handlers;

public class ConnectedToLobbyHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;
    
    public ConnectedToLobbyHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    
    public Response Handle(string payload)
    {
        var deserializedPayload = JsonConvert.DeserializeObject<ConnectToLobbyPayload>(payload);

        var lobbyId = deserializedPayload.LobbyIdentifier;
        var userId = deserializedPayload.UserIdentifier;
        
        var roomInformation = _multiplayerService.ConnectToRoom(userId, lobbyId);

        var responsePayload = new ConnectedToLobbyPayload()
        {
            Information = roomInformation
        };

        var response = new Response
        {
            Status = "OK",
            Payload = JsonConvert.SerializeObject(responsePayload)
        };

        return response;
    }
}
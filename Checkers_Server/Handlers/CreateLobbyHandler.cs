﻿using CheckersServer.Interfaces;
using Domain.Models;
using Domain.Models.Server;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersServer.Handlers;

public class CreateLobbyHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;
    
    public CreateLobbyHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }

    public Response Handle(string payload)
    {
        var parsedPayload = JsonConvert.DeserializeObject<CreateLobbyPayload>(payload);

        if (!_multiplayerService.IsUserValid(parsedPayload.HostIdentifier))
            return Response.FailedResponse;
        
        var lobbyInformation = _multiplayerService.CreateRoom(parsedPayload.HostIdentifier, parsedPayload.Settings);
        var responsePayload = new CreatedLobbyPayload()
        {
            Information = lobbyInformation
        };
        
        var response = new Response
        {
            Status = "OK",
            Payload = JsonConvert.SerializeObject(responsePayload)
        };
        
        return response;
    }
}
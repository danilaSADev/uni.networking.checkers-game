﻿using System.Windows.Input;
using CheckersServer.Interfaces;
using Domain.Models;
using Domain.Models.Server;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersServer.Handlers;

public class RetrieveLobbiesHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;

    public RetrieveLobbiesHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }

    public Response Handle(string payload)
    {
        var unpackedPayload = JsonConvert.DeserializeObject<RequestLobbiesPayload>(payload);

        if (!_multiplayerService.UserValid(unpackedPayload.UserIdentifier))
            return Response.Failed;

        var responsePayload = new FetchedLobbiesPayload()
        {
            Lobbies = _multiplayerService.GetLobbies()
        };

        var response = new Response
        {
            Status = "OK",
            Payload = JsonConvert.SerializeObject(responsePayload)
        };
        return response;
    }
}
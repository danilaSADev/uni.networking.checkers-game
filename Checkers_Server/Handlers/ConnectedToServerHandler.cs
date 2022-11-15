﻿using CheckersServer.Common;
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

    public ServerResponse Handle(string payload)
    {
        Console.WriteLine("Established connection!");
        var deserializedPayload = JsonConvert.DeserializeObject<EstablishConnectionPayload>(payload);

        // TODO : check whether player password matches
        // TODO : write data to database        
        var id = IdentifierGenerator.Generate(deserializedPayload.Username);
        var player = new Player
        {
            Nickname = deserializedPayload.Username,
            Identifier = id,
            IpAddress = deserializedPayload.IpAddress,
            Port = deserializedPayload.Port
        };

        _multiplayerService.AddPlayer(player);

        var responsePayload = new ConnectionEstablishedPayload
        {
            UserIdentifier = id
        };

        var response = new ServerResponse
        {
            Status = "OK",
            Payload = JsonConvert.SerializeObject(responsePayload)
        };

        return response;
    }
}
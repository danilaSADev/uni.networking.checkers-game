using System.ComponentModel.DataAnnotations;
using CheckersServer.Interfaces;
using CheckersServer.Services;
using Domain.Models;
using Domain.Models.Server;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersServer.Handlers;

public class RetrieveLeaderboardHandler : ICommandHandler
{
    private readonly IMultiplayerService _service;

    public RetrieveLeaderboardHandler(IMultiplayerService service)
    {
        _service = service;
    }

    public ServerResponse Handle(string payload)
    {
        var unpackedPayload = JsonConvert.DeserializeObject<RequestLeaderboardPayload>(payload);

        if (!_service.IsUserValid(unpackedPayload.UserIdentifier))
            return ServerResponse.FailedResponse;

        var responsePayload = new FetchedLeaderboardPayload()
        {
            Leaderboard = _service.GetLeaderboard()
        };

        var response = new ServerResponse
        {
            Status = "OK",
            Payload = JsonConvert.SerializeObject(responsePayload)
        };
        return response;
    }
}
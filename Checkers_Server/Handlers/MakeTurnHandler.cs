using CheckersServer.Interfaces;
using Domain.Models;
using Domain.Models.Server;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersServer.Handlers;

public class MakeTurnHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;

    public MakeTurnHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    
    public Response Handle(string payload)
    {
        var unpackedPayload = JsonConvert.DeserializeObject<MakeTurnPayload>(payload);

        if (unpackedPayload != null && !_multiplayerService.TryMakeTurn(unpackedPayload))
        {
            return Response.Failed;
        }
        
        return Response.Ok;
    }
}
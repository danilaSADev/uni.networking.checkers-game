using CheckersServer.Interfaces;
using Domain.Models;
using Domain.Models.Server;

namespace CheckersServer.Handlers;

public class DisconnectedFromServerHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;

    public DisconnectedFromServerHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }

    public Response Handle(string payload)
    {
        Console.WriteLine("Disconnect from server!");
        var unpackedPayload = payload;

        _multiplayerService.RemovePlayer(unpackedPayload);
        var response = new Response
        {
            Status = "OK",
            Payload = string.Empty
        };
        return response;
    }
}
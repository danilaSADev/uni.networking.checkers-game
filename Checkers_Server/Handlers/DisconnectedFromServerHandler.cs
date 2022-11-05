using CheckersServer.Interfaces;
using Domain.Models;

namespace CheckersServer.Handlers;

public class DisconnectedFromServerHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;

    public DisconnectedFromServerHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }

    public ServerResponse Handle(string payload)
    {
        Console.WriteLine("Disconnect from server!");
        _multiplayerService.RemovePlayer(payload);
        var response = new ServerResponse
        {
            Status = "OK",
            Payload = string.Empty
        };
        return response;
    }
}
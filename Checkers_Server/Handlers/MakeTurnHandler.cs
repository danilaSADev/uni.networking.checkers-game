using CheckersServer.Interfaces;
using Domain.Models;
using Domain.Models.Server;

namespace CheckersServer.Handlers;

public class MakeTurnHandler : ICommandHandler
{
    private readonly IMultiplayerService _multiplayerService;

    public MakeTurnHandler(IMultiplayerService multiplayerService)
    {
        _multiplayerService = multiplayerService;
    }
    
    public ServerResponse Handle(string payload)
    {
        
        
        throw new NotImplementedException();
    }
}
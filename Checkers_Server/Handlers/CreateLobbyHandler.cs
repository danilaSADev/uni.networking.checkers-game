using CheckersServer.Interfaces;
using Domain.Models;

namespace CheckersServer.Handlers;

public class CreateLobbyHandler : ICommandHandler
{
    private readonly IMultiplayerService _service;


    public CreateLobbyHandler(IMultiplayerService service)
    {
        _service = service;
    }
    
    public ServerResponse Handle(string payload)
    {
        
        
        throw new NotImplementedException();
    }
}
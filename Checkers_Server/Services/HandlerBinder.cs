using CheckersServer.Interfaces;
using Domain.Models;

namespace CheckersServer.Services;

public class HandlerBinder
{
    private readonly IDictionary<string, ICommandHandler> _handlers;

    public HandlerBinder()
    {
        _handlers = new Dictionary<string, ICommandHandler>();
    }

    public ServerResponse Handle(ClientRequest request)
    {
        if (request.Payload.Equals(string.Empty))
            return ServerResponse.FailedResponse;
        
        if (_handlers.Keys.Contains(request.Command))
            return _handlers[request.Command].Handle(request.Payload);

        return ServerResponse.Unknown;
    }

    public void Bind(string command, ICommandHandler handler)
    {
        if (_handlers.Keys.Contains(command))
            throw new Exception("Command already allocated!");

        _handlers.Add(command, handler);
    }
}
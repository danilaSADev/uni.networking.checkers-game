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
        var response = new ServerResponse
        {
            Status = "UNKNOWN",
            Payload = string.Empty
        };

        if (_handlers.Keys.Contains(request.Command))
            response = _handlers[request.Command].Handle(request.Payload);

        return response;
    }

    public void Bind(string command, ICommandHandler handler)
    {
        if (_handlers.Keys.Contains(command))
            throw new Exception("Command already allocated!");

        _handlers.Add(command, handler);
    }
}
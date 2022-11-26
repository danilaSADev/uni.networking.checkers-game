using System;
using System.Collections.Generic;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;

namespace Domain.Networking.Handlers;

public class HandlerBinder
{
    private readonly IDictionary<string, ICommandHandler> _handlers;

    public HandlerBinder()
    {
        _handlers = new Dictionary<string, ICommandHandler>();
    }

    public Response Handle(Request request)
    {
        if (request.Payload.Equals(string.Empty))
            return Response.Empty;
        
        if (_handlers.Keys.Contains(request.Command))
            return _handlers[request.Command].Handle(request.Payload);

        return Response.Unknown;
    }

    public void Unbind(string command)
    {
        _handlers.Remove(command);
    }
    
    public void Bind(string command, ICommandHandler handler)
    {
        if (_handlers.Keys.Contains(command))
            throw new Exception("Command already allocated!");

        _handlers.Add(command, handler);
    }
}
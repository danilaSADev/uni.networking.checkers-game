using Domain.Models;

namespace CheckersServer.Interfaces;

public interface ICommandHandler
{
    ServerResponse Handle(string payload);
}
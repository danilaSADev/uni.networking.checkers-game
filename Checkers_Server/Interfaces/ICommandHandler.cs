using Domain.Models;
using Domain.Models.Server;

namespace CheckersServer.Interfaces;

public interface ICommandHandler
{
    ServerResponse Handle(string payload);
}
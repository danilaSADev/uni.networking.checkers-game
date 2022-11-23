using Domain.Models.Server;
using Domain.Networking.Handlers.Models;

namespace Domain.Networking.Handlers.Interfaces;

public interface ICommandHandler
{
    Response Handle(string payload);
}
using CheckersServer.Models;

namespace CheckersServer.Interfaces;

public interface IMultiplayerService
{
    void AddPlayer(Player player);
    void RemovePlayer(string payload);
}
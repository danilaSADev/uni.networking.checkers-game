using CheckersServer.Models;
using Domain.Models;

namespace CheckersServer.Interfaces;

public interface IMultiplayerService
{
    void AddPlayer(Player player);
    void RemovePlayer(string payload);
    bool IsUserValid(string identifier);
    List<LobbyInformation> GetLobbies();
    Dictionary<string, int> GetLeaderboard();
}
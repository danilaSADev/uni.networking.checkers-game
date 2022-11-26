using CheckersServer.Models;

namespace CheckersServer.Interfaces;

public interface ILeaderboardRepository
{
    void Create(Player player);
    Player Read(string id);
    void Remove(string id);
    bool Exist(string username);
    IEnumerable<Player> ReadAll();
}
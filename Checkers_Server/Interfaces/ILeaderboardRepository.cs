using CheckersServer.Models;

namespace CheckersServer.Interfaces;

public interface ILeaderboardRepository
{
    void Create(Player player);
    Player ReadById(string id);
    Player ReadByUsername(string username);
    void Remove(string id);
    bool Exist(string username);
    IEnumerable<Player> ReadAll();
}
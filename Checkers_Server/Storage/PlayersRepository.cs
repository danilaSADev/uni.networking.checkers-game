using CheckersServer.Models;
using Npgsql;

namespace CheckersServer.Storage;

public class PlayersRepository
{
    private string _connectionString;
    // TODO : create an interface for repository
    public PlayersRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Player> GetPlayers()
    {
        throw new NotImplementedException();   
    }

    public void CreatePlayer(Player player)
    {
        throw new NotImplementedException();
    }
    
    public Player GetPlayerByIdentifier(string identifier)
    {
        throw new NotImplementedException();
    }

    public void RemovePlayer(Player player)
    {
        throw new NotImplementedException();
    }
    
}
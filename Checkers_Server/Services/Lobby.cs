using CheckersServer.Models;
using Domain.Models;

namespace CheckersServer.Services;

public class Lobby
{
    private const int MaxPlayers = 2;
    private GameType _gameType = new();

    private readonly List<Player> _players;
    private readonly Player _host;
 
    private GameSettings _settings;
    public Lobby(Player host)
    {
        _host = host;
        _players = new List<Player>();
        _players.Add(host);
    }

    public void ChangeGameType(GameType type)
    {
    }

    public bool ConnectPlayer(Player player)
    {
        if (_players.Count > MaxPlayers)
            return false;

        _players.Add(player);
        return true;
    }

    public void DisconnectPlayer(Player player)
    {
        if (_players.Contains(player))
            _players.Remove(player);
    }
}

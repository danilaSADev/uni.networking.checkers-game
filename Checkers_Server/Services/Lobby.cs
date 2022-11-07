using CheckersServer.Models;
using Domain.Models;

namespace CheckersServer.Services;

public class Lobby
{
    private static readonly int MaxPlayers = 2;
    private readonly Player _host;
    private readonly List<Player> _players;

    private GameSettings _settings;

    public GameSettings Settings => _settings;
    public string Identifier { get; }
    public string Name { get;  }
    
    public Lobby(Player host)
    {
        _host = host;
        _players = new List<Player>();
        _players.Add(host);
    }

    public void ChangeGameSettings(GameSettings settings)
    {
        _settings = settings;
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
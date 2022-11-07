using CheckersServer.Models;
using Domain.Models;

namespace CheckersServer.Services;

public class Lobby
{
    private static readonly int MaxPlayers = 2;
    private readonly Player _host;
    private readonly List<Player> _players;

    private GameSettings _settings;

    public int PlayersAmount => _players.Count;

    public GameSettings Settings => _settings;
    public string Identifier { get; }
    public string Name => _settings.RoomName;
    
    public Lobby(Player host, string identifier, GameSettings settings)
    {
        _host = host;
        Identifier = identifier;
        _settings = settings;
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

    public LobbyInformation GetInformation()
    {
        return new LobbyInformation()
        {
            IsTournament = _settings.IsTournament,
            Identifier = this.Identifier,
            Name = this.Name
        };
    }
    
    public void DisconnectPlayer(Player player)
    {
        if (_players.Contains(player))
            _players.Remove(player);
    }
}
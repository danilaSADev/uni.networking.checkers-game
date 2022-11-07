using CheckersServer.Interfaces;
using CheckersServer.Models;
using Domain.Models;

namespace CheckersServer.Services;

public class MultiplayerService : IMultiplayerService
{
    private readonly List<Player> _players = new();
    private readonly List<Lobby> _rooms = new();

    public void RemovePlayer(string identifier)
    {
        var player = _players.FirstOrDefault(p => p.Identifier == identifier);

        if (player == null)
            return;

        _players.Remove(player);
    }

    public bool IsUserValid(string identifier)
    {
        return _players.FirstOrDefault(p => p.Identifier == identifier) != null;
    }

    public List<LobbyInformation> GetLobbies()
    {
        return _rooms.Select(room => new LobbyInformation()
        {
            Name = room.Name,
            Identifier = room.Identifier,
            IsTournament = room.Settings.IsTournament
        }).ToList();
    }

    public Dictionary<string, int> GetLeaderboard()
    {
        Dictionary<string, int> fetchedPlayers = new Dictionary<string, int>();
        
        foreach (var player in _players)
        {
            fetchedPlayers.Add(player.Nickname, player.Score);
        }

        return fetchedPlayers;
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }

    public void CreateRoom(Player host)
    {
        var newLobby = new Lobby(host);
        _rooms.Add(newLobby);
    }
}
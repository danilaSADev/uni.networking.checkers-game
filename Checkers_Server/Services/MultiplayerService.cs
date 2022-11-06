using CheckersServer.Interfaces;
using CheckersServer.Models;

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
using CheckersServer.Common;
using CheckersServer.Interfaces;
using CheckersServer.Models;
using CheckersServer.Storage;
using Domain.Models.Server;
using Domain.Models.Shared;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersServer.Services;

public class MultiplayerService : IMultiplayerService
{
    private readonly List<Player> _players = new();
    private readonly List<Lobby> _rooms = new();

    private ILeaderboardRepository _repository;

    public MultiplayerService()
    {
        _repository = new LeaderboardRepository();
    }
    
    public void RemovePlayer(string identifier)
    {
        var player = _players.FirstOrDefault(p => p.Identifier == identifier);

        if (player == null)
            return;
        
        _players.Remove(player);
        var leaderboard = GetLeaderboard();
        var payload = new FetchedLeaderboardPayload()
        {
            Leaderboard = leaderboard
        };
        NotifyAll(ServerCommands.LeaderboardUpdated, JsonConvert.SerializeObject(payload));
    }

    public LobbyInformation GetLobbyInformation(string lobbyIdentifier)
    {
        var lobby = _rooms.First(l => l.Identifier.Equals(lobbyIdentifier));
        return lobby.GetInformation();
    }

    public bool UserValid(string identifier)
    {
        return _players.FirstOrDefault(p => p.Identifier == identifier) != null;
    }

    public void ChangeLobbyState(string userId, string lobbyId, GameState state)
    {
        var lobby = _rooms.FirstOrDefault(r => r.Identifier == lobbyId);

        if (lobby is null || !UserValid(userId))
            return;
        
        lobby.ChangeGameState(userId, state);
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
        var data = _repository.ReadAll();

        Dictionary<string, int> fetchedPlayers = new Dictionary<string, int>();
        
        foreach (var player in data)
        {
            fetchedPlayers.Add(player.Nickname, player.Score);
        }
        
        return fetchedPlayers;
    }

    public void NotifyAll(string command, string message)
    {
        foreach (var player in _players)
        {
            player.Notify(command, message);
        }
    }

    public bool TryMakeTurn(MakeTurnPayload payload) => _rooms
        .First(l => l.Identifier == payload.LobbyId)
        .TryMakeTurn(payload);

    public void AddPlayer(Player player)
    {
        if (!_repository.Exist(player.Nickname))
            _repository.Create(player);

        if (_repository.Read(player.Identifier).Password != player.Password)
            return;
        
        var leaderboard = GetLeaderboard();
        leaderboard.Add(player.Nickname, player.Score);
        var payload = new FetchedLeaderboardPayload()
        {
            Leaderboard = leaderboard
        };
        NotifyAll(ServerCommands.LeaderboardUpdated, JsonConvert.SerializeObject(payload));
        _players.Add(player);
    }

    public LobbyInformation CreateRoom(string hostIdentifier, GameSettings settings)
    {
        var host = _players.First(p => p.Identifier.Equals(hostIdentifier));
        var identifier = IdentifierGenerator.Generate(settings.RoomName);
        var newLobby = new Lobby(host, identifier, settings);
        
        _rooms.Add(newLobby);
        return GetLobbyInformation(identifier);
    }

    public LobbyInformation ConnectToRoom(string userId, string lobbyId)
    {
        var lobby = _rooms.FirstOrDefault(l => l.Identifier == lobbyId);
        var player = _players.FirstOrDefault(p => p.Identifier == userId);

        if (lobby == null || player == null)
            throw new NullReferenceException();

        lobby.ConnectPlayer(player);
        return lobby.GetInformation();
    }
    public void DisconnectFromRoom(string userId, string lobbyId)
    {
        var lobby = _rooms.FirstOrDefault(l => l.Identifier == lobbyId);
        var player = _players.FirstOrDefault(p => p.Identifier == userId);

        if (lobby == null || player == null)
            throw new NullReferenceException();

        lobby.DisconnectPlayer(player);

        if (lobby.PlayersAmount == 0)
            _rooms.Remove(lobby);
    }

}
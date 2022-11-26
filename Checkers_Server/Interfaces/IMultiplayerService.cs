using CheckersServer.Models;
using CheckersServer.Services;
using Domain.Models;
using Domain.Models.Shared;
using Domain.Payloads.Client;

namespace CheckersServer.Interfaces;

public interface IMultiplayerService
{
    bool TryAddPlayer(Player player);
    void RemovePlayer(string payload);
    LobbyInformation GetLobbyInformation(string lobbyIdentifier);
    LobbyInformation CreateRoom(string hostIdentifier, GameSettings settings);
    LobbyInformation ConnectToRoom(string userId, string lobbyId);
    void DisconnectFromRoom(string userId, string lobbyId);
    bool UserValid(string identifier);
    // todo : turn into separated model
    bool TryMakeTurn(MakeTurnPayload payload);
    void ChangeLobbyState(string userId, string lobbyId, GameState state);
    List<LobbyInformation> GetLobbies();
    Dictionary<string, int> GetLeaderboard();
}
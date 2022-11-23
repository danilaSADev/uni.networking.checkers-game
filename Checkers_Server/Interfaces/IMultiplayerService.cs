using CheckersServer.Models;
using CheckersServer.Services;
using Domain.Models;
using Domain.Models.Shared;
using Domain.Payloads.Client;

namespace CheckersServer.Interfaces;

public interface IMultiplayerService
{
    void AddPlayer(Player player);
    void RemovePlayer(string payload);
    LobbyInformation GetLobbyInformation(string lobbyIdentifier);
    LobbyInformation CreateRoom(string hostIdentifier, GameSettings settings);
    LobbyInformation ConnectToRoom(string userId, string lobbyId);
    void DisconnectFromRoom(string userId, string lobbyId);
    bool IsUserValid(string identifier);
    bool TryMakeTurn(MakeTurnPayload payload);
    List<LobbyInformation> GetLobbies();
    Dictionary<string, int> GetLeaderboard();
}
using CheckersServer.Models;
using Domain.Models.Shared;
using Domain.Payloads.Client;

namespace CheckersServer.Interfaces;

public interface IMultiplayerService
{
    /// <summary>
    /// Tries to add player to the server. Could change player's id if it is needed.
    /// </summary>
    /// <param name="player">Player to add</param>
    /// <returns>Returns true or false depending on player added or not</returns>
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
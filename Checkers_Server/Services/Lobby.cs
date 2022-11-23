using Domain.Models.Shared;
using Domain.Payloads.Client;
using Domain.Models.Server;
using Domain.Payloads.Server;
using Newtonsoft.Json;
using CheckersServer.Models;

namespace CheckersServer.Services;

public class Lobby
{
    private static readonly int MaxPlayers = 2;
    private readonly Dictionary<Side, Player> _players;
    private GameSettings _settings;
    private readonly Player _host;
    private int _roundsLeft = 1;
    
    private Side TurnSide => (Side)(_turnCounter % 2);
    private Side OppositeSide => (Side)((_turnCounter + 1) % 2);
    
    private int _firstSideValue = 0;
    private int _secondSideValue = 1;
    private int _turnCounter = 0;

    public int PlayersAmount => _players.Count;
    public string Identifier { get; }
    public GameSettings Settings => _settings;
    public string Name => _settings.RoomName;
    
    public Lobby(Player host, string identifier, GameSettings settings)
    {
        Identifier = identifier;
        _settings = settings;
        _players = new Dictionary<Side, Player>();
        _host = host;
        
        if (_settings.IsTournament)
            _roundsLeft = 3;
        
        var rand = new Random();
        _firstSideValue = 1;//rand.Next(0, 2);
        _secondSideValue = (_firstSideValue + 1) % 2;
        var firstSide = (Side)_firstSideValue;
        _players.Add(firstSide, host);
    }

    public void ChangeGameSettings(GameSettings settings) => _settings = settings;
    public bool TryMakeTurn(MakeTurnPayload payload)
    {
        if (payload.TurnSide.Equals(TurnSide) && _players[TurnSide].Identifier == payload.UserId)
        {
            _players[OppositeSide].Notify(ServerCommands.MakeTurn, JsonConvert.SerializeObject(payload));
            if (payload.FinishedTurn)
            {
                _turnCounter++;
            }
            return true;
        }
        return false;
    }

    private void FinishGame()
    {
        
    }
    /// <summary>
    /// Logic for starting an actual game process.
    /// </summary>
    private void StartGame()
    {
        foreach (var pair in _players)
        {
            var payload = new GameStartedPayload
            {
                PlayerSide = pair.Key
            };
            pair.Value.Notify(
                ServerCommands.GameStarted, 
                JsonConvert.SerializeObject(payload)
                );
        }
    }
    
    public void ConnectPlayer(Player player)
    {
        if (_players.Count == MaxPlayers)
            return;

        _players.Add((Side)_secondSideValue, player);
        StartGame();
    }

    public LobbyInformation GetInformation()
    {
        return new LobbyInformation
        {
            Difficulty = _settings.Difficulty,
            IsTournament = _settings.IsTournament,
            TimeToMakeTurn = _settings.TimeOut,
            Identifier = Identifier,
            Name = Name
        };
    }
    
    public void DisconnectPlayer(Player player)
    {
        if (_players.ContainsValue(player))
        {
            var key = _players.FirstOrDefault(pair => pair.Value.Identifier == player.Identifier).Key;
            _players.Remove(key);
        }
    }
}

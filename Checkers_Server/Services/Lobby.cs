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
    private Side TurnSide => (Side)((_turnCounter + (int)_startSide) % 2);
    private Side OppositeSide => (Side)((_turnCounter + (int)_startSide + 1) % 2);
    private Side _startSide = Side.White;
    private Side SecondSideOption => (Side)(((int)_players.Keys.First() + 1) % 2);
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
        var firstSide = (Side)rand.Next(0, 2);
        _players.Add(firstSide, host);
    }

    public void ChangeGameSettings(GameSettings settings) => _settings = settings;
    public bool TryMakeTurn(MakeTurnPayload payload)
    {
        if (payload.TurnSide.Equals(TurnSide) && _players.Any(p => p.Value.Identifier == payload.UserId))
        {
            _players[OppositeSide].Notify(ServerCommands.MakeTurn, JsonConvert.SerializeObject(payload));
            if (payload.FinishedTurn)
            {
                _turnCounter++;
            }
            // TODO : send changes to another player
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Logic for starting an actual game process.
    /// </summary>
    private void StartGame()
    {
        foreach (var pair in _players)
        {
            var payload = new GameStartedPayload()
            {
                PlayerSide = pair.Key
            };
            pair.Value.Notify(ServerCommands.GameStarted, JsonConvert.SerializeObject(payload));
        }
    }
    
    public void ConnectPlayer(Player player)
    {
        if (_players.Count == MaxPlayers)
            return;

        _players.Add(SecondSideOption, player);
        
        if (_players.Count == MaxPlayers)
            StartGame();
    }

    public LobbyInformation GetInformation()
    {
        return new LobbyInformation
        {
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

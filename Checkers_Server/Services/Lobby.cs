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

    private Dictionary<Player, int> _localScore;

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
        _firstSideValue = rand.Next(0, 2);
        _secondSideValue = (_firstSideValue + 1) % 2;
        var firstSide = (Side)_firstSideValue;
        _players.Add(firstSide, host);
        _localScore = new Dictionary<Player, int>();
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
        var orderedScore = _localScore.OrderByDescending(p => p.Value).ToArray();

        // if both players has same score then draw
        if (orderedScore[0].Value == orderedScore[1].Value)
        {
            var payload = new ServerMessagePayload
            {
                Message = "It's a draw! Nobody gained points.",
                Type = ServerMessageType.GameTermination
            };
            foreach (var player in _players)
            {
                player.Value.Notify(ServerCommands.ServerMessage, JsonConvert.SerializeObject(payload));                
            }
            return;
        }
        
        // else decide who is the winner
        var winner = orderedScore[0].Key;
        var loser = orderedScore[1].Key;
            
        var winnerPayload = new ServerMessagePayload()
        {
            Message = "Congratulations! You won the game and gained 1 extra point!",
            Type = ServerMessageType.GameTermination
        };
            
        var loserPayload = new ServerMessagePayload()
        {
            Message = "You lost! Better start a new game and take a revenge!",
            Type = ServerMessageType.GameTermination
        };
        
        winner.Score++;
        
        winner.Notify(ServerCommands.ServerMessage, JsonConvert.SerializeObject(winnerPayload));
        loser.Notify(ServerCommands.ServerMessage, JsonConvert.SerializeObject(loserPayload));
    }

    private void FinishRound()
    {
        SwapSides();
        StartGame();
    }

    private void SwapSides()
    {
        var players = _players.Values.ToArray();
        _players.Clear();

        _firstSideValue = (_firstSideValue + 1) % 2;
        _secondSideValue = (_firstSideValue + 1) % 2;

        _players.Add((Side)_firstSideValue, players[0]);
        _players.Add((Side)_secondSideValue, players[1]);
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

        foreach (var p in _players.Values)
        {
            _localScore.Add(p, 0);   
        }
        
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

    public void ChangeGameState(string userId, GameState state)
    {
        if (state == GameState.NoTurns)
        {
            var payload = new ServerMessagePayload()
            {
                Message = "It's a draw! Nobody gained points.",
                Type = ServerMessageType.Notification
            };
            foreach (var player in _players)
            {
                player.Value.Notify(ServerCommands.ServerMessage, JsonConvert.SerializeObject(payload));                
            }
        }

        if (state == GameState.Winner)
        {
            var winner = _players.Values.First(p => p.Identifier == userId);
            var loser = _players.Values.Except(new[] { winner }).First();
    
            var winnerPayload = new ServerMessagePayload()
            {
                Message = "Congratulations! You won the game and gained 1 extra point!",
                Type = ServerMessageType.Notification
            };
            var loserPayload = new ServerMessagePayload()
            {
                Message = "You lost! Better start a new game and take a revenge!",
                Type = ServerMessageType.Notification
            };
            
            winner.Notify(ServerCommands.ServerMessage, JsonConvert.SerializeObject(winnerPayload));
            loser.Notify(ServerCommands.ServerMessage, JsonConvert.SerializeObject(loserPayload));
            _localScore[winner]++;
        }

        _roundsLeft--;
        if (_roundsLeft == 0)
        {
            FinishGame();
            return;
        }
        FinishRound();
    }
    
    public void DisconnectPlayer(Player player)
    {
        if (_players.ContainsValue(player))
        {
            var key = _players.FirstOrDefault(pair => pair.Value.Identifier == player.Identifier).Key;
            _players.Remove(key);
            var payload = new ServerMessagePayload
            {
                Message = "Second player has disconnected! Game will be terminated!",
                Type = ServerMessageType.GameTermination
            };
            if (_players.Count > 0)
                _players.First().Value.Notify(ServerCommands.ServerMessage, JsonConvert.SerializeObject(payload));
        }
    }
}

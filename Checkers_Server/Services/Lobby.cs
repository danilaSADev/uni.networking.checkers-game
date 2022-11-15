using System.Net;
using System.Net.Sockets;
using Domain.Models;
using Domain.Models.Shared;
using Domain.Payloads.Client;
using CheckersServer.Models;
using Domain.Converters;
using Domain.Models.Server;

namespace CheckersServer.Services;

public class Lobby
{
    private static readonly int MaxPlayers = 2;
    private readonly List<Player> _players;
    private GameSettings _settings;
    private readonly Player _host;
    private int _rounds = 1;
    private Side TurnSide => (Side)((_turnCounter + (int)_startSide) % 2);
    private Side _startSide = Side.White;
    private int _turnCounter = 0;

    public int PlayersAmount => _players.Count;
    public string Identifier { get; }
    public GameSettings Settings => _settings;
    public string Name => _settings.RoomName;
    
    public Lobby(Player host, string identifier, GameSettings settings)
    {
        Identifier = identifier;
        _settings = settings;
        _players = new List<Player>();
        _host = host;
        
        if (_settings.IsTournament)
            _rounds = 3;
        
        ConnectPlayer(host);
    }

    public void ChangeGameSettings(GameSettings settings) => _settings = settings;
    public bool TryMakeTurn(MakeTurnPayload payload)
    {
        // TODO : check identifiers
        if (payload.TurnSide.Equals(TurnSide))
        {
            // TODO : send changes to another player
            return true;
        }
        return false;
    }

    private void SendDataToPlayer(Player player)
    {
        
    }
    
    private void StartGame()
    {
        var rand = new Random();

        var firstSide = (Side)rand.Next(0, 2);
        var secondSide = (Side)(1 - (int)firstSide);
        
        // TODO : for each player create socket
        // TODO : send information about side and number of games
    }
    
    public void ConnectPlayer(Player player)
    {
        if (_players.Count == MaxPlayers)
            return;
        
        _players.Add(player);
        
        
        var endpoint = new IPEndPoint(IPAddress.Parse(player.IpAddress), player.Port); 
        // TODO : test in pair with client
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.SendTimeout = ServerInfo.MaxClientResponseTime;

        socket.Bind(endpoint);
        var request = new ServerRequest { Payload = "Experimental message"};
        socket.Send(UniversalConverter.ConvertObject(request));
        
        player.GameSocket = socket;

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
        if (_players.Contains(player))
            _players.Remove(player);
    }
}
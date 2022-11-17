using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using CheckersClient.ClientActions;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;
using Domain.Models.Shared;
using Domain.Payloads.Client;
using Domain.Payloads.Server;

namespace CheckersClient.Main
{
    public class GameSession
    {
        public static IPEndPoint EndPoint => new IPEndPoint(
            Dns.GetHostEntry(Dns.GetHostName()).AddressList[1],
            ConnectionEstablisher.Port
            );

        public Side PlayerSide => _playerSide;
        public bool IsGameRunning => _isRunning;
        private readonly string _usedId;
        private readonly GameSettings _settings;
        
        private Side _playerSide;
        private Side _opponentSide; 
        
        private Side _turnSide = Side.White;
        private bool _isRunning = false;

        private GameStats _gameStats;
        
        public GameSession(string usedId, GameSettings settings)
        {
            _usedId = usedId;
            _settings = settings;
        }

        public void MakeTurn(TurnInformation turnInfo)
        {
            if (_turnSide.Equals(_playerSide))
            {
                var action = new MakeTurnAction(turnInfo);
                
                // TODO : run action
            }
        }

        private void StartTimer()
        {
            
        }

        public void StartGame(GameStartedPayload payload)
        {
            _playerSide = payload.PlayerSide;
            _opponentSide = (Side)(((int)_playerSide + 1) % 2);
        }
    }
}
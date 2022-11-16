using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;
using Domain.Models.Shared;

namespace CheckersClient.Main
{
    public class GameSession
    {
        public static IPEndPoint EndPoint => new IPEndPoint(
            Dns.GetHostEntry(Dns.GetHostName()).AddressList[1],
            ConnectionEstablisher.Port
            );

        public Side PlayerSide => _side;
        public bool IsGameRunning => _isRunning;

        private readonly GameSettings _settings;
        private readonly Side _side = Side.White;
        
        private Thread _thread;
        private Side _turnSide = Side.White;
        
        private bool _isRunning = false;
        
        public GameSession(GameSettings settings)
        {
            _settings = settings;
        }

        
        public void StopListeningToServer()
        {
            _thread.Interrupt();
        }

        public void MakeTurn()
        {
            if (_turnSide.Equals(_side))
            {
                // var action = new MakeTurnAction();
            }
        }
    }
}
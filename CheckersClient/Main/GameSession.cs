using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using CheckersClient.ClientActions;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;
using Domain.Models.Shared;

namespace CheckersClient.Main
{
    public class GameSession
    {
        public static readonly IPEndPoint _endPoint = new IPEndPoint(
            Dns.GetHostEntry(Dns.GetHostName()).AddressList[1],
            ServerInfo.Port
            );

        public Side PlayerSide => _side;
        
        private readonly Side _side;
        private Socket _gameSocket;
        private readonly GameSettings _settings;
        private Side _turnSide;
        private Thread _thread;
        
        private bool _isRunning = true;
        
        public GameSession(Side side, GameSettings settings)
        {
            _side = side;
            _gameSocket = ServerInfo.SharedSocket;
            _settings = settings;
        }

        public void StartListeningToServer()
        {
            _thread = new Thread(o =>
            {
                _gameSocket.Bind(_endPoint);
                _gameSocket.Listen(12);
                try
                {
                    while (_isRunning)
                    {
                        var handler = _gameSocket.Accept();
            
                        var data = new byte[65536];
                        handler.Receive(data);
                    
                        var request = UniversalConverter.ConvertBytes<ServerRequest>(data);
                    
                        // TODO : parse data with request
                    
                        var response = new ClientResponse();
                        // TODO : check whether player is still connected to the game
                        // handler.Send(UniversalConverter.ConvertObject(response));
            
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
            _thread.Start();
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
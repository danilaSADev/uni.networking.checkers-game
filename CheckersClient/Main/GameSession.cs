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
            ConnectionEstablisher.FindFreePort()
            );

        public Side PlayerSide => _side;
        public bool IsGameRunning => _isRunning;

        private readonly GameSettings _settings;
        private readonly Side _side = Side.White;
        
        private Socket _gameSocket;
        private Thread _thread;
        private Side _turnSide = Side.White;
        
        private bool _isRunning = false;
        
        public GameSession(GameSettings settings)
        {
            _gameSocket = ServerInfo.SharedSocket;
            _settings = settings;
        }

        public void StartListeningToServer()
        {
            _thread = new Thread(o =>
            {
                _gameSocket.Bind(EndPoint);
                _gameSocket.Listen(12);
                try
                {
                    while (_isRunning)
                    {
                        var handler = _gameSocket.Accept();
            
                        var data = new byte[65536];
                        handler.Receive(data);
                        var request = UniversalConverter.ConvertBytes<ServerRequest>(data);
                        var response = new ClientResponse();
                        
                        // TODO : actions that could happen
                        // - game is starting
                        // - 
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
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Domain.Converters;
using Domain.Models.Shared;
using Domain.Networking.Handlers;
using Domain.Networking.Handlers.Models;
using CheckersClient.Main;

namespace CheckersClient.Services
{
    public delegate void ServerMessageHandler(string command, string message);
    public class ClientSocketListener
    {
        private Thread _thread;
        private static Socket _gameSocket;
        private bool _isListeningToServer = false;
        private HandlerBinder _binder;
        private Board _board;

        public bool IsLive => _isListeningToServer;
        public Board GameBoard => _board;

        private string _userId;
        public string UserId
        {
            get => _userId;
            set
            {
                _board.UserId = value;
                _userId = value;
            }
        }

        public ClientSocketListener(HandlerBinder binder)
        {
            _binder = binder;
            _board = new Board(new GameSettings());
        }

        public void TryStartListeningToServer()
        {
            if (_isListeningToServer)
                return;

            _gameSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _isListeningToServer = true;
            
            _thread = new Thread(async o =>
            {
                _gameSocket.Bind(new IPEndPoint(IPAddress.Any, ConnectionEstablisher.Port));
                _gameSocket.Listen(12);
                try
                {
                    while (_isListeningToServer)
                    {
                        var handler = _gameSocket.Accept();
                        var data = new byte[65536];
                        
                        handler.Receive(data);

                        if (!_isListeningToServer)
                            break;
                        
                        var request = UniversalConverter.ConvertBytes<Request>(data);
                        Console.WriteLine($"Message from server: {request.Command} {request.Payload}");
                        _binder.Handle(request);
                        // TODO : check whether player is still connected to the game
            
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

                _gameSocket.Close();
            });
            _thread.Start();
        }
        
        public void StopListeningToServer()
        {
            _gameSocket.Close();
            _thread.Abort();
            _isListeningToServer = false;
        }

    }
}
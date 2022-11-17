using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using CheckersClient.Main;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;

namespace CheckersClient
{
    public delegate void ServerMessageHandler(string command, string message);
    public class ClientSocketListener
    {
        private Thread _thread;
        private Socket _gameSocket;
        private bool _isListeningToServer = false;
        public bool IsLive => _isListeningToServer;

        public event ServerMessageHandler OnServerMessageRecieved;
        
        public ClientSocketListener()
        {
            _gameSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void StopListeningToServer()
        {
            _thread.Abort();
            _isListeningToServer = false;
        }
        
        public void TryStartListeningToServer()
        {
            if (_isListeningToServer)
                return;

            _isListeningToServer = true;
            
            _thread = new Thread(async o =>
            {
                _gameSocket.Bind(GameSession.EndPoint);
                _gameSocket.Listen(12);
                try
                {
                    Console.WriteLine($"Client is now listening on: {GameSession.EndPoint.Address}:{GameSession.EndPoint.Port}");
                    while (true)
                    {
                        var handler = await _gameSocket.AcceptAsync();
            
                        var data = new byte[65536];
                        handler.Receive(data);
                        
                        var request = UniversalConverter.ConvertBytes<Request>(data);

                        Console.WriteLine($"Message from server: {request.Payload}");
                        if (OnServerMessageRecieved != null)
                            OnServerMessageRecieved.Invoke(request.Command, request.Payload);

                        // TODO : actions that could happen
                        // - game is starting
                        // -
                        // TODO : work on how to implement doing actions on whenever any action is requested
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

    }
}
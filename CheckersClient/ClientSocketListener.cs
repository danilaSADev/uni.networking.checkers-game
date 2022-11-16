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
    public class ClientSocketListener
    {
        private Thread _thread;
        private Socket _gameSocket;
        private bool _isListeningToServer = false;

        public ClientSocketListener()
        {
            _gameSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void StopListeningToServer()
        {
            _thread.Interrupt();
            _isListeningToServer = false;
        }
        
        public void StartListeningToServer()
        {
            if (_isListeningToServer)
                return;

            _isListeningToServer = true;
            
            _thread = new Thread(o =>
            {
                _gameSocket.Bind(GameSession.EndPoint);
                _gameSocket.Listen(12);
                try
                {
                    Console.WriteLine($"Client is listening to server on: {GameSession.EndPoint.Address}:{GameSession.EndPoint.Port}");
                    while (true)
                    {
                        var handler = _gameSocket.Accept();
            
                        var data = new byte[65536];
                        handler.Receive(data);
                        var request = UniversalConverter.ConvertBytes<ServerRequest>(data);

                        Console.WriteLine($"Message from server: {request.Payload}");
                        var response = new ClientResponse();
                        
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
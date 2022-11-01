using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Domain.Converters;
using Domain.Models;

namespace CheckersClient.Main
{
    public class ConnectionEstablisher
    {
        private string _identifier;
        private readonly string _serverAddress;
        private readonly int _port;

        public ConnectionEstablisher()
        {
            _serverAddress = ServerInfo.IpAddress;
            _port = ServerInfo.Port;
        }

        public void ConnectToServer()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_serverAddress), _port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                
                var request = new ClientRequest
                {
                    Command = ClientCommands.ConnectToServer,
                    Payload = "DanilaSADev"
                };
                    
                byte[] data = UniversalConverter.ConvertObject(request);
                socket.Send(data);
 
                data = new byte[256]; 
                
                socket.Receive(data, data.Length, 0);

                var response = UniversalConverter.ConvertBytes<ServerResponse>(data);
                
                Console.WriteLine("Client identifier: " + response.Payload);
                _identifier = response.Payload;
                
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisconnectFromServer()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_serverAddress), _port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                
                var request = new ClientRequest
                {
                    Command = ClientCommands.DisconnectFromServer,
                    Payload = _identifier
                };
                    
                byte[] data = UniversalConverter.ConvertObject(request);
                socket.Send(data);
 
                data = new byte[256]; 
                
                socket.Receive(data, data.Length, 0);

                var response = UniversalConverter.ConvertBytes<ServerResponse>(data);
                
                Console.WriteLine("Server response: " + response.Status);
                _identifier = response.Payload;
                
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
    }
}
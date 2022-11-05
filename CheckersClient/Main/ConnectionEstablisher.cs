using System;
using System.Net;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using Domain.Payloads;
using Newtonsoft.Json;

namespace CheckersClient.Main
{
    public class ConnectionEstablisher
    {
        private string _identifier;
        private readonly IPEndPoint _ipPoint;

        public ConnectionEstablisher()
        {
            _ipPoint = new IPEndPoint(
                IPAddress.Parse(ServerInfo.IpAddress), 
                ServerInfo.Port
            );
        }
        
        public static int FindFreePort()
        {
            int port = 0;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
                socket.Bind(localEP);
                localEP = (IPEndPoint)socket.LocalEndPoint;
                port = localEP.Port;
            }
            finally
            {
                socket.Close();
            }
            return port;
        }

        public void ConnectToServer(string nickname, string password)
        {
            try
            {
                Socket socket = ServerInfo.SharedSocket;
                socket.Connect(_ipPoint);
                
                var payload = new EstablishConnectionPayload()
                {
                    Username = "danilaSADev",
                    Password = "1234",
                    IpAddress = "",
                    Port = FindFreePort()
                };
                
                var request = new ClientRequest
                {
                    Command = ClientCommands.ConnectToServer,
                    Payload = JsonConvert.SerializeObject(payload)
                };
                    
                byte[] data = UniversalConverter.ConvertObject(request);
                socket.Send(data);
 
                data = new byte[256]; 
                
                socket.Receive(data, data.Length, 0);

                var response = UniversalConverter.ConvertBytes<ServerResponse>(data);

                Console.WriteLine($@"Client identifier: {response.Payload}");
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
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(_ipPoint);
                
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
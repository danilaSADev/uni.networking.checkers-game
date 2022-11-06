using System;
using System.Net;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.Main
{
    public class ConnectionEstablisher
    {
        private readonly IPEndPoint _ipPoint;
        private string _identifier;

        public ConnectionEstablisher()
        {
            _ipPoint = new IPEndPoint(
                IPAddress.Parse(ServerInfo.IpAddress),
                ServerInfo.Port
            );
        }

        public static int FindFreePort()
        {
            var port = 0;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                var localEP = new IPEndPoint(IPAddress.Any, 0);
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

        public ConnectionEstablishedPayload ConnectToServer(string nickname, string password)
        {
            ConnectionEstablishedPayload responsePayload = null;
            try
            {
                var socket = ServerInfo.SharedSocket;
                socket.Connect(_ipPoint);

                var payload = new EstablishConnectionPayload
                {
                    Username = nickname,
                    Password = password,
                    IpAddress = "",
                    Port = FindFreePort()
                };

                var request = new ClientRequest
                {
                    Command = ClientCommands.ConnectToServer,
                    Payload = JsonConvert.SerializeObject(payload)
                };

                var data = UniversalConverter.ConvertObject(request);
                socket.Send(data);

                data = new byte[256];

                socket.Receive(data, data.Length, 0);

                var response = UniversalConverter.ConvertBytes<ServerResponse>(data);
                responsePayload = JsonConvert.DeserializeObject<ConnectionEstablishedPayload>(response.Payload);

                if (responsePayload != null)
                {
                    Console.WriteLine($@"Client identifier: {responsePayload.UserIdentifier}");
                    _identifier = responsePayload.UserIdentifier;
                }

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (responsePayload == null)
                throw new Exception("Failed to establish connection with server!");

            return responsePayload;
        }

        public void DisconnectFromServer()
        {
            try
            {
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(_ipPoint);

                var request = new ClientRequest
                {
                    Command = ClientCommands.DisconnectFromServer,
                    Payload = _identifier
                };

                var data = UniversalConverter.ConvertObject(request);
                socket.Send(data);

                data = new byte[256];

                socket.Receive(data, data.Length, 0);

                var response = UniversalConverter.ConvertBytes<ServerResponse>(data);

                // var responsePayload = JsonConvert.DeserializeObject<Di>(response.Payload)

                Console.WriteLine("Server response: " + response.Status);
                _identifier = response.Payload;

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
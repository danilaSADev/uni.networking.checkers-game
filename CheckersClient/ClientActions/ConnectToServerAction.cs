using System;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersClient.ClientActions
{
    public class ConnectToServerAction : AbstractAction
    {
        private readonly string _password;
        private readonly string _username;

        public ConnectToServerAction(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public override ServerResponse Request()
        {
            ServerResponse response = null;
            try
            {
                var socket = ServerInfo.SharedSocket;
                socket.Connect(_ipPoint);

                var payload = new EstablishConnectionPayload
                {
                    Username = _username,
                    Password = _password,
                    IpAddress = "",
                    Port = 0
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

                response = UniversalConverter.ConvertBytes<ServerResponse>(data);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }
    }
}
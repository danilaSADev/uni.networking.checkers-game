using System;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersClient.ClientActions
{
    public class GetLobbiesAction : AbstractAction
    {
        private readonly string _identifier;

        public GetLobbiesAction(string identifier)
        {
            _identifier = identifier;
        }
        
        public override ServerResponse Request()
        {
            ServerResponse response = null;
            try
            {
                var socket = ServerInfo.SharedSocket;
                socket.Connect(_ipPoint);

                var payload = new RequestLobbiesPayload()
                {
                    UserIdentifier = _identifier
                };

                var request = new ClientRequest
                {
                    Command = ClientCommands.RetrieveLobbies,
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
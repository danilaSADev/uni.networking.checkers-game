using System;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;

namespace CheckersClient.ClientActions
{
    public class DisconnectFromServerAction : AbstractAction
    {
        private readonly string _identifier;
     
        public DisconnectFromServerAction(string identifier)
        {
            _identifier = identifier;
        }
        
        public override ServerResponse Request()
        {
            ServerResponse response = null;
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

                data = new byte[MAX_BYTES];

                socket.Receive(data, data.Length, 0);

                response = UniversalConverter.ConvertBytes<ServerResponse>(data);
                
                Console.WriteLine("Server response: " + response.Status);

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
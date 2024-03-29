﻿using System;
using System.Net;
using System.Net.Sockets;
using Domain.Models;
using Domain.Converters;
using Domain.Models.Server;
using Domain.Networking.Handlers.Models;
using Newtonsoft.Json;

namespace CheckersClient.Actions
{
    public abstract class AbstractAction
    {
        protected readonly IPEndPoint _ipPoint;
        protected readonly int MAX_BYTES = 65536;
        
        public AbstractAction()
        {
            _ipPoint = new IPEndPoint(
                IPAddress.Parse(ServerInfo.IpAddress),
                ServerInfo.Port
            );
        }

        protected Response ExecuteAction<TPayload>(string command, TPayload payload) 
        {
            Response response = null;
            try
            {
                var socket = ServerInfo.SharedSocket;
                socket.Connect(_ipPoint);
                
                var request = new Request
                {
                    Command = command,
                    Payload = JsonConvert.SerializeObject(payload)
                };

                var data = UniversalConverter.ConvertObject(request);
                socket.Send(data);

                data = new byte[MAX_BYTES];

                socket.Receive(data, data.Length, 0);

                response = UniversalConverter.ConvertBytes<Response>(data);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public abstract Response Request();
    }
}
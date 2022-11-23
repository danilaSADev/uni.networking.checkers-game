using System;
using Domain.Payloads.Server;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Newtonsoft.Json;
using CheckersClient.Main;
using CheckersClient.Services;

namespace CheckersClient.Handlers
{
    public class GameStartedHandler : ICommandHandler
    {
        private readonly Board _board;

        public GameStartedHandler(Board board)
        {
            _board = board;
        }
        
        public Response Handle(string payload)
        {
            var unpackedPayload = JsonConvert.DeserializeObject<GameStartedPayload>(payload);
            _board.StartGame(unpackedPayload);
            Console.WriteLine("Game is started!");
            return Response.Ok;
        }
    }
}
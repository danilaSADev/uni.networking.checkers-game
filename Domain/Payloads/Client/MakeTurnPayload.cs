using System.Dynamic;
using Domain.Models;
using Domain.Models.Shared;

namespace Domain.Payloads.Client
{
    public class MakeTurnPayload
    {
        public Vector FromPosition { get; set; }
        public Vector ToPosition { get; set; }
        public Side TurnSide { get; set; }
    }
}
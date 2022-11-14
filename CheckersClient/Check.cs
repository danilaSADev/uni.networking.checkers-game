using System.Collections.Generic;
using Domain.Models.Shared;

namespace CheckersClient
{
    public class Check
    {
        public bool IsKing { get; set; }

        public Check(Side side, Vector position)
        {
            Side = side;
            Position = position;
            IsKing = false;
        }

        public Side Side { get; }

        public Vector Position { get; private set; }

        public void Move(Vector newPosition)
        {
            Position = newPosition;
        }
    }
}
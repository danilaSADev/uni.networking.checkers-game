﻿using System.Collections.Generic;
using Domain.Models.Shared;

namespace CheckersClient
{
    public class Checker
    {
        public bool IsKing { get; set; }

        public List<Vector> MovementOptions { get; set; }
        
        public Checker(Side side, Vector position)
        {
            Side = side;
            Position = position;
            IsKing = false;
        }
        
        public Side Side { get; }

        public Vector Position { get; private set; }

        public bool IsMoveValid(Vector position) => MovementOptions.Contains(position);

        public void Move(Vector newPosition)
        {
            if (MovementOptions == null)
                return;
            
            if (!MovementOptions.Contains(newPosition))
                return;
            
            if (newPosition.Y == (Side.Equals(Side.White) ? 7 : 0))
                IsKing = true;
            
            Position = newPosition;
        }

        public void OpponentMove(Vector newPosition)
        {
            Position = newPosition;
        }
    }
}
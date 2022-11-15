﻿using System;
using System.Linq;
using System.Collections.Generic;
using Domain.Models.Shared;
using CheckersClient.Main;

namespace CheckersClient
{
    public class Board
    {
        private readonly List<Checker> _checkers;
        private List<Vector> _directions = new List<Vector>();
        private readonly GameSession _gameSession;

        public Board()
        {
            _directions.Add(new Vector(1,1));
            _directions.Add(new Vector(-1,1));
            // TODO : replace with side retrieved from the server
            _gameSession = new GameSession( new GameSettings());
            _gameSession.StartListeningToServer();
            _checkers = new List<Checker>();
        }

        public Checker Selected { get; private set; }

        public void InitializeBoard()
        {
            for (var y = 0; y < 3; y++)
                for (var x = 0; x < 7; x += 2)
                {
                    var check = new Checker(Side.White, new Vector(x + y % 2, y));
                    _checkers.Add(check);
                }

            for (var y = 0; y < 3; y++)
                for (var x = 0; x < 7; x += 2)
                {
                    var check = new Checker(Side.Black, new Vector(x + (y + 1) % 2, 7 - y));
                    _checkers.Add(check);
                }
        }

        private Checker CastRay(Vector startPosition, Vector direction, int distance)
        {
            var vec = startPosition;
            var counter = 0;
            while (IsOnBoard(vec) && counter < distance)
            {
                var obstacle = _checkers.FirstOrDefault(c => c.Position.Equals(vec));

                if (obstacle != null)
                    return obstacle;
                
                vec += direction;
                counter++;
            }
            return null;
        }

        private bool AnyObstacle(Vector position)
        {
            return _checkers.FirstOrDefault(c => c.Position.Equals(position)) != null;
        }

        public void CalculateAllPossibleDirections()
        {
            if (!_gameSession.IsGameRunning)
                return;
            
            Dictionary<Checker, List<Vector>> toBeatOptions = new Dictionary<Checker, List<Vector>>();
            Dictionary<Checker, List<Vector>> toMoveOptions = new Dictionary<Checker, List<Vector>>();

            var playerCheckers = _checkers.Where(c => c.Side == _gameSession.PlayerSide).ToArray();

            bool isOnlyBeating = false;
            
            foreach (var checker in playerCheckers)
            {
                var result = CalculatePossibleDirections(checker);
                toBeatOptions.Add(checker, result.Item1);
                toMoveOptions.Add(checker, result.Item2);
                isOnlyBeating = isOnlyBeating || result.Item1.Count > 0;
            }

            foreach (var checker in playerCheckers)
            {
                if (isOnlyBeating)
                {
                    checker.MovementOptions = toBeatOptions[checker];
                    continue;
                }
                checker.MovementOptions = toMoveOptions[checker];
            }
        }
        
        private Tuple<List<Vector>, List<Vector>> CalculatePossibleDirections(Checker checker)
        {
            Vector startPosition = checker.Position;
            List<Vector> options = new List<Vector>();
            List<Vector> beatingOptions = new List<Vector>();
            
            var oppositeSide = (Side)(((int) checker.Side + 1) % 2); 
            var sign = checker.Side == Side.Black ? -1 : 1;
            
            var newDirections = _directions.Select(d => d * sign).ToList();
            
            foreach (var direction in newDirections)
            {
                bool anyObstacle = false;
                int distance = checker.IsKing ? 8 : 1;
                var reverseDirection = new Vector(direction.X, direction.Y * -1);
                
                // TODO : refactor in one piece of code
                // extra beating options (doing in reverse)
                Checker extraObstacle = CastRay(startPosition + reverseDirection, reverseDirection, distance);
                if (extraObstacle != null)
                {
                    var positionAfterBeating =  extraObstacle.Position + reverseDirection;
                    var isAnyAfterBeatingObstacle = AnyObstacle(positionAfterBeating);
                    
                    if (IsOnBoard(positionAfterBeating) && !isAnyAfterBeatingObstacle && extraObstacle.Side.Equals(oppositeSide))
                    {
                        beatingOptions.Add(positionAfterBeating);
                        anyObstacle = true;
                    }
                }
                
                Checker obstacle = CastRay(startPosition + direction, direction, distance);
                // if any obstacle on the way
                if (obstacle != null)
                {
                    var positionAfterBeating = obstacle.Position + direction;
                    var isAnyAfterBeatingObstacle = AnyObstacle(positionAfterBeating);
                    
                    if (IsOnBoard(positionAfterBeating) && !isAnyAfterBeatingObstacle && obstacle.Side.Equals(oppositeSide))
                    {
                        beatingOptions.Add(positionAfterBeating);
                        anyObstacle = true;
                    }
                }
                
                if (anyObstacle)
                    continue;
                
                // TODO : Implement feature "only beating"
                // TODO: Execute this method automatically
                // adding new possible positions
                int counter = 0;

                for (Vector vec = startPosition + direction; IsOnBoard(vec) && counter < distance && !AnyObstacle(vec); vec += direction)
                {
                    options.Add(vec);
                    counter++;
                }

                if (checker.IsKing)
                {
                    for (Vector vec = startPosition + reverseDirection; IsOnBoard(vec) && counter < distance && !AnyObstacle(vec); vec += + reverseDirection)
                    {
                        options.Add(vec);
                        counter++;
                    }
                }
            }
            
            return new Tuple<List<Vector>, List<Vector>>(beatingOptions, options);
        }

        private bool IsOnBoard(Vector vec)
        {
            return vec.X >= 0 && vec.X < 8 && vec.Y >= 0 && vec.Y < 8;
        }

        public void ClickedAt(Vector pos)
        {
            if (!_gameSession.IsGameRunning)
                return;
            
            if (Selected == null)
            {
                Selected = _checkers.FirstOrDefault(x => 
                    x.Position.X == pos.X && 
                    x.Position.Y == pos.Y && 
                    x.Side == _gameSession.PlayerSide); 
                return;
            }

            if (pos.X == Selected.Position.X && pos.Y == Selected.Position.Y)
            {
                Selected = null;
                return;
            }

            var any = _checkers.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);

            if (any == null)
            {
                Selected.Move(pos);
            }
            
            Selected = null;
        }

        public IEnumerable<Checker> GetAllCheckers()
        {
            CalculateAllPossibleDirections();
            return _checkers.Except(new[] { Selected }).ToList();
        }
    }
}
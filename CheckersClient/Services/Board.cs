using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Models.Shared;

namespace CheckersClient.Services
{
    public partial class Board
    {
        private int _turnCreditsLeft = 0;
        private readonly List<Checker> _checkers;
        private bool _isGameRunning;
        private List<Vector> _directions = new List<Vector>();
        public string UserId { get; set; }

        public Board(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _directions.Add(new Vector(1,1));
            _directions.Add(new Vector(-1,1));
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
            if (!_isGameRunning)
                return;
            
            Dictionary<Checker, List<Vector>> toBeatOptions = new Dictionary<Checker, List<Vector>>();
            Dictionary<Checker, List<Vector>> toMoveOptions = new Dictionary<Checker, List<Vector>>();

            var playerCheckers = _checkers.Where(c => c.Side == _playerSide).ToArray();

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

        private bool TryAddBeatingOptions(Checker checker, Vector direction, int distance, ref List<Vector> options)
        {
            Vector startPosition = checker.Position;
            Checker extraObstacle = CastRay(startPosition + direction, direction, distance);
            if (extraObstacle != null)
            {
                var positionAfterBeating =  extraObstacle.Position + direction;
                var isAnyAfterBeatingObstacle = AnyObstacle(positionAfterBeating);
                    
                if (IsOnBoard(positionAfterBeating) && !isAnyAfterBeatingObstacle && extraObstacle.Side.Equals(_opponentSide))
                {
                    options.Add(positionAfterBeating);
                    
                    if (checker.IsKing)
                    {
                        for (Vector vec = positionAfterBeating + direction; IsOnBoard(vec) && !AnyObstacle(vec); vec += direction)
                        {
                            options.Add(vec);
                        }
                    }
                    
                    return true;
                }
            }
            return false;
        }
        
        private Tuple<List<Vector>, List<Vector>> CalculatePossibleDirections(Checker checker)
        {
            Vector startPosition = checker.Position;
            List<Vector> options = new List<Vector>();
            List<Vector> beatingOptions = new List<Vector>();
             
            var sign = checker.Side == Side.Black ? -1 : 1;
            var newDirections = _directions.Select(d => d * sign).ToList();
            
            foreach (var direction in newDirections)
            {
                int distance = checker.IsKing ? 8 : 1;
                var reverseDirection = new Vector(direction.X, direction.Y * -1);
                
                // all beating options (extra in reverse)
                var anyObstacle = TryAddBeatingOptions(checker, reverseDirection, distance, ref beatingOptions);
                anyObstacle = anyObstacle || TryAddBeatingOptions(checker, direction, distance, ref beatingOptions);
                
                if (anyObstacle)
                    continue;
                
                // TODO : could be merged but I'm lazy
                int counter = 0;
                for (Vector vec = startPosition + direction; IsOnBoard(vec) && counter < distance && !AnyObstacle(vec); vec += direction)
                {
                    options.Add(vec);
                    counter++;
                }

                if (checker.IsKing)
                {
                    counter = 0;
                    for (Vector vec = startPosition + reverseDirection; IsOnBoard(vec) && counter < distance && !AnyObstacle(vec); vec += reverseDirection)
                    {
                        options.Add(vec);
                        counter++;
                    }
                }
            }
            
            return new Tuple<List<Vector>, List<Vector>>(beatingOptions, options);
        }

        /// <summary>
        /// Checks whether specified position lays on board
        /// </summary>
        /// <param name="vec">Position to check</param>
        /// <returns>Return true if position is on board</returns>
        private bool IsOnBoard(Vector vec)
        {
            return vec.X >= 0 && vec.X < 8 && vec.Y >= 0 && vec.Y < 8;
        }

        /// <summary>
        /// Returns true if there's only checkers of player side
        /// </summary>
        /// <returns></returns>
        public bool HasWon()
        {
            return _checkers.All(c => c.Side == _playerSide);
        }

        /// <summary>
        /// Return true if player has no possible movements
        /// </summary>
        /// <returns></returns>
        public bool NoExtraTurns()
        {
            CalculateAllPossibleDirections();
            foreach (var checker in _checkers)
            {
                if (checker.MovementOptions != null && checker.MovementOptions.Count > 0)
                    return false;
            }
            return true;
        }

        public void ClickedAt(Vector pos)
        {
            if (!_isGameRunning || _turnSide != _playerSide || _turnCreditsLeft == 0)
                return;
            
            if (Selected == null)
            {
                Selected = _checkers.FirstOrDefault(x => 
                    x.Position.X == pos.X && 
                    x.Position.Y == pos.Y && 
                    x.Side == _playerSide); 
                return;
            }

            if (pos.X == Selected.Position.X && pos.Y == Selected.Position.Y)
            {
                Selected = null;
                return;
            }

            var any = _checkers.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);

            if (any != null || !Selected.IsMoveValid(pos))
                return;

            var start = Selected.Position;
            var end = pos;
            
            var notBeating = !CheckIfBeatingAndBeat(start, end);
            Selected.Move(pos);

            bool notNextMoveGuaranteed = CalculatePossibleDirections(Selected).Item1.Count == 0;

            var turnType = notBeating ? TurnType.Movement : TurnType.Beating;
            _turnCreditsLeft--;

            var turnFinished = (notBeating || notNextMoveGuaranteed) && _turnCreditsLeft == 0;

            if (turnFinished)
            {
                _turnSide = _opponentSide;
            }
            
            SendTurnToServer(turnFinished, start, end, turnType);
            Selected = null;
        }

        public IEnumerable<Checker> GetAllCheckers()
        {
            CalculateAllPossibleDirections();
            return _checkers.Except(new[] { Selected }).ToList();
        }
    }
}
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using CheckersClient.Main;
using Domain.Models.Shared;

namespace CheckersClient
{
    public class Board
    {
        private readonly List<Check> _checkers;
        private List<Vector> _directions = new List<Vector>();
        private List<Vector> _hitDirections = new List<Vector>();
        private readonly GameSession _gameSession;

        public Board()
        {
            _directions.Add(new Vector(1,1));
            _directions.Add(new Vector(-1,1));
            _gameSession = new GameSession(Side.White, new GameSettings());
            _gameSession.StartListeningToServer();
            _checkers = new List<Check>();
        }

        public Check Selected { get; private set; }

        public void InitializeBoard()
        {
            for (var y = 0; y < 3; y++)
                for (var x = 0; x < 7; x += 2)
                {
                    var check = new Check(Side.White, new Vector(x + y % 2, y));
                    _checkers.Add(check);
                }

            for (var y = 0; y < 3; y++)
                for (var x = 0; x < 7; x += 2)
                {
                    var check = new Check(Side.Black, new Vector(x + (y + 1) % 2, 7 - y));
                    _checkers.Add(check);
                }
        }

        // private IEnumerable<Check> ChecksThatCouldDoMove()
        // {
        //     var haveToBeat = new List<Check>();
        //
        //     foreach (var VARIABLE in COLLECTION)
        //     {
        //         
        //     }
        // }

        private Check CastRay(Vector startPosition, Vector direction, int distance)
        {
            var vec = startPosition;
            var counter = 0;

            while (IsOnBoard(vec) || counter < distance)
            {
                var obstacle = _checkers.FirstOrDefault(c => c.Position.Equals(vec));

                if (obstacle != null)
                    return obstacle;
                
                vec += direction;
                counter++;
            }

            return null;
        }
        
        public List<Vector> CalculatePossibleDirections()
        {
            Check check = Selected;
            var startPosition = check.Position;
            var options = new List<Vector>();
            var beatingOptions = new List<Vector>();
            
            var oppositeSide = (Side)(((int)check.Side + 1) % 2); 
            var sign = check.Side == Side.Black ? -1 : 1;
            
            var newDirections = _directions.Select(d => d * sign);
            
            foreach (var direction in newDirections)
            {
                Vector newPosition = startPosition + direction;
                
                int distance = check.IsKing ? 8 : 1;
                Check obstacle = CastRay(newPosition, direction, distance);

                if (obstacle != null && obstacle.Side.Equals(oppositeSide))
                {
                    var positionAfterBeating = obstacle.Position + direction;
                    var afterBeatingObstacle = _checkers.FirstOrDefault(c => c.Position.Equals(positionAfterBeating));
                    if (IsOnBoard(positionAfterBeating) && afterBeatingObstacle == null)
                    {
                        beatingOptions.Add(positionAfterBeating);
                        continue;
                    }
                }
                 // TODO : Fix issue with not counting obstacles
                 // TODO : Implement feature "only beating"
                 // TODO: execute this method automatically
                 
                
                // adding new possible positions
                int counter = 0;
                for (Vector vec = newPosition; IsOnBoard(vec) && counter < distance; vec += direction)
                {
                    options.Add(vec);
                    counter++;
                }
            }

            // if there's any beating option then 
            if (beatingOptions.Count > 0)
                return beatingOptions;

            return options;
        }

        private bool IsOnBoard(Vector vec)
        {
            return vec.X >= 0 && vec.X < 8 && vec.Y >= 0 && vec.Y < 8;
        }

        public void ClickedAt(Vector pos)
        {
            if (Selected == null)
            {
                Selected = _checkers.FirstOrDefault(
                    x => x.Position.X == pos.X 
                    && x.Position.Y == pos.Y 
                    && x.Side != _gameSession.PlayerSide);
                return;
            }

            if (pos.X == Selected.Position.X && pos.Y == Selected.Position.Y)
            {
                Selected = null;
                return;
            }

            var any = _checkers.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);
            if (any == null) Selected.Move(pos);
            Selected = null;
        }

        public IEnumerable<Check> GetAllCheckers()
        {
            return _checkers.Except(new[] { Selected }).ToList();
        }
    }
}
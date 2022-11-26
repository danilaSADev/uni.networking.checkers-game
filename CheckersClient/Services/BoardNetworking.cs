using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CheckersClient.Actions;
using CheckersClient.ClientActions;
using CheckersClient.Models;
using Domain.Models.Shared;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.Services
{
    public class GameStateChangedArgs : EventArgs
    {
    }
    
    public delegate void GameStateChanged(object source, GameStateChangedArgs args);
    public partial class Board
    {
        private GameSettings _gameSettings;
        public Side PlayerSide => _playerSide;
        
        private Side _playerSide;
        private Side _opponentSide;
        private Side _turnSide = Side.White;

        public event GameStateChanged StateChanged;

        public void SetTimer(Side sideTimer)
        {
            // TODO : Kills other timer and starts new timer
        }

        public void SendTurnToServer(bool turnFinished, Vector start, Vector end, TurnType turnType)
        {
            var turnInformation = new TurnInformation
            {
                TurnSide = _playerSide,
                // checking if any turn credits left
                FinishedTurn = turnFinished,
                FromPosition = start,
                ToPosition = end,
                Type = turnType,
                UserId = UserId,
                LobbyId = _gameSettings.LobbyId
            };
            MakeTurnAction action = new MakeTurnAction(turnInformation);
            var response = action.Request();
            
            if (HasWon())
                new HasWonAction(UserId, _gameSettings.LobbyId).Request();
        }
        
        public bool CheckIfBeatingAndBeat(Vector start, Vector end)
        {
            var difference = end - start;
            var direction = new Vector(Math.Sign(difference.X), Math.Sign(difference.Y));

            bool removedObstacle = false;

            for (Vector i = start + direction; !i.Equals(end); i += direction)
            {
                var obstacle = _checkers.FirstOrDefault(c => c.Position.Equals(i));
                if (obstacle != null)
                {
                    _checkers.Remove(obstacle);
                    removedObstacle = true;
                    break;
                }
            }

            return removedObstacle;
        }

        public void ResetTurnCredits()
        {
            _turnCreditsLeft = _gameSettings.Difficulty == GameDifficulty.Hard ? 2 : 1;
        }
        
        public void MakeOpponentTurn(MakeTurnPayload payload)
        {
            var start = payload.FromPosition;
            var end = payload.ToPosition;

            var checker = _checkers.FirstOrDefault(c => c.Position.Equals(start));

            if (checker == null)
                return;

            CheckIfBeatingAndBeat(start, end);
            
            checker.OpponentMove(end);
            if (payload.FinishedTurn)
                _turnSide = _playerSide;

            if (StateChanged != null) 
                StateChanged.Invoke(this, new GameStateChangedArgs());

            if (NoExtraTurns())
                new HasNoTurnsAction( UserId, _gameSettings.LobbyId).Request();
        }
        
        public void SetLobbyInformation(LobbyInformation information)
        {
            _gameSettings.IsTournament = information.IsTournament;
            _gameSettings.TimeOut = information.TimeToMakeTurn;
            _gameSettings.Difficulty = information.Difficulty;
            _gameSettings.LobbyId = information.Identifier;
            _gameSettings.RoomName = information.Name;
        }
        
        public void StartGame(GameStartedPayload payload)
        {
            _playerSide = payload.PlayerSide;
            _opponentSide = (Side)(((int)_playerSide + 1) % 2);
            _turnCreditsLeft = _playerSide == Side.White ? 1 : 2;
            _isGameRunning = true;
            
            if (StateChanged != null) 
                StateChanged.Invoke(this, new GameStateChangedArgs());
        }
        
        public void Reinitialize()
        {
            _checkers.Clear();
            _isGameRunning = false;
        }
        
    }
}
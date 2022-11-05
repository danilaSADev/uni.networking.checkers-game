using System.Net.Sockets;

namespace CheckersClient.Main
{
    public class GameSession
    {
        private readonly Side _side;
        private Socket _gameSocket;
        private Side _turnSide;

        public GameSession(Side side, Socket gameSocket)
        {
            _side = side;
            _gameSocket = gameSocket;
        }

        public void RetrieveTurnSide()
        {
            
        }

        public void MakeTurn()
        {
            if (_turnSide.Equals(_side))
            {
                
            }
        }
    }
}
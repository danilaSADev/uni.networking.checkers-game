using System.Net;
using Domain.Models;

namespace CheckersClient.ClientActions
{
    public abstract class AbstractAction
    {
        protected readonly IPEndPoint _ipPoint;

        public AbstractAction()
        {
            _ipPoint = new IPEndPoint(
                IPAddress.Parse(ServerInfo.IpAddress),
                ServerInfo.Port
            );
        }

        public abstract ServerResponse Request();
    }
}
using CheckersServer.Handlers;
using CheckersServer.Services;
using Domain.Models;

namespace CheckersServer;

public class Program
{
    public static int Main(string[] args)
    {
        HandlerBinder binder = new();
        var multiplayerService = new MultiplayerService();
        var listener = new ServerSocketListener(binder);

        listener.StartServer();

        binder.Bind(ClientCommands.ConnectToServer, new ConnectedToServerHandler(multiplayerService));
        binder.Bind(ClientCommands.DisconnectFromServer, new DisconnectedFromServerHandler(multiplayerService));
        binder.Bind(ClientCommands.CreateLobby, new CreateLobbyHandler(multiplayerService));
        
        return 0;
    }
}
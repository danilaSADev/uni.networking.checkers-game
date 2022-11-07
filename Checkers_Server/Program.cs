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

        binder.Bind(ClientCommands.ConnectToServer, new ConnectedToServerHandler(multiplayerService));
        binder.Bind(ClientCommands.DisconnectFromServer, new DisconnectedFromServerHandler(multiplayerService));
        binder.Bind(ClientCommands.CreateLobby, new CreateLobbyHandler(multiplayerService));
        binder.Bind(ClientCommands.RetrieveLeaderboard, new RetrieveLeaderboardHandler(multiplayerService));
        binder.Bind(ClientCommands.RetrieveLobbies, new RetrieveLobbiesHandler(multiplayerService));
        
        listener.StartServer();
        return 0;
    }
}
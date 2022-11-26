using CheckersServer.Handlers;
using CheckersServer.Services;
using Domain.Models;
using Domain.Networking.Handlers;

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
        binder.Bind(ClientCommands.ConnectToLobby, new ConnectedToLobbyHandler(multiplayerService));
        binder.Bind(ClientCommands.DisconnectFromLobby, new DisconnectedFromLobbyHandler(multiplayerService));
        binder.Bind(ClientCommands.MakeTurn, new MakeTurnHandler(multiplayerService));
        binder.Bind(ClientCommands.ChangeGameStats, new GameStateHandler(multiplayerService));
        
        listener.StartServer();
        return 0;
    }
}
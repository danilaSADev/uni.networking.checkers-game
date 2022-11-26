using System.Data;
using CheckersServer.Interfaces;
using CheckersServer.Models;
using Npgsql;

namespace CheckersServer.Storage;

public class LeaderboardRepository : ILeaderboardRepository
{
    private const string _connectionString = "Host=localhost;Username=postgres;Password=rootPass@1234;Database=checkers-leaderboards";
    private NpgsqlConnection _connection;
    public LeaderboardRepository()
    {
        _connection = new NpgsqlConnection(_connectionString);
        _connection.Open();
        var sql = "SELECT version()";

        using var command = new NpgsqlCommand(sql, _connection);
        Console.WriteLine(command.ExecuteScalar().ToString());
    }
    
    public void Create(Player player)
    {
        var sql = "INSERT INTO players(identifier, username, password, score) VALUES(@identifier, @username, @password, @score)";
        using var command = new NpgsqlCommand(sql, _connection);
        
        command.Parameters.AddWithValue("identifier", player.Identifier);
        command.Parameters.AddWithValue("username", player.Username);
        command.Parameters.AddWithValue("password", player.Password);
        command.Parameters.AddWithValue("score", player.Score);
        
        command.Prepare();
        
        command.ExecuteNonQuery();
    }

    public Player ReadByUsername(string username)
    {
        string sql = "SELECT id, username, password, score FROM players WHERE username=@username";
        using var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("username", username);
        
        using NpgsqlDataReader rdr = cmd.ExecuteReader();

        Player result = new Player();
        while (rdr.Read())
        {
            result = new Player()
            {
                Identifier = rdr.GetString(0),
                Username = rdr.GetString(1),
                Password = rdr.GetString(2),
                Score = rdr.GetInt32(3)
            };
        }
        return result;
        
    }

    public Player ReadById(string id)
    {
        string sql = "SELECT id, username, password, score FROM players WHERE id=@id";
        using var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("id", id);
        
        using NpgsqlDataReader rdr = cmd.ExecuteReader();

        Player result = new Player();
        while (rdr.Read())
        {
            result = new Player()
            {
                Identifier = rdr.GetString(0),
                Username = rdr.GetString(1),
                Password = rdr.GetString(2),
                Score = rdr.GetInt32(3)
            };
        }
        return result;
    }

    public bool Exist(string username)
    {
        string sql = "SELECT EXISTS(SELECT 1 FROM players WHERE username=@username)";
        using var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("username", username);

        cmd.Prepare();
        
        return (bool)cmd.ExecuteScalar();
    }

    public void Remove(string id)
    {
        string sql = "REMOVE FROM players WHERE id=@id";
        using var cmd = new NpgsqlCommand(sql, _connection);

        cmd.Parameters.AddWithValue("id", id);

        cmd.Prepare();

        cmd.ExecuteNonQuery();
    }

    public IEnumerable<Player> ReadAll()
    {
        string sql = "SELECT id, username, password, score FROM players";
        using var cmd = new NpgsqlCommand(sql, _connection); 
        using NpgsqlDataReader rdr = cmd.ExecuteReader();

        List<Player> result = new List<Player>();
        while (rdr.Read())
        {
            result.Add(new Player()
            {
                Identifier = rdr.GetString(0),
                Username = rdr.GetString(1),
                Password = rdr.GetString(2),
                Score = rdr.GetInt32(3)
            });
        }
        return result;
    }
}
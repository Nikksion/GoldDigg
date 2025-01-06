using System.Data;
using UnityEngine;
using IDbCommand = System.Data.IDbCommand;
using IDbConnection = System.Data.IDbConnection;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour {
    string conn;
    string sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    string DATABASE_NAME = "/mydatabase.db";

    void Start() {
        string filepath = Application.persistentDataPath + DATABASE_NAME;
        Debug.Log($"filepath={filepath}");
        conn = "Data Source=" + filepath;
        CreateTable();
        List<Player> topPlayers = GetTopPlayers(5);
        foreach (var player in topPlayers) {
            Debug.Log($"Player: {player.Name}, Score: {player.Score}");
        }
    }

    private void CreateTable() {
        using (dbconn = new SqliteConnection(conn)) {
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            sqlQuery = "CREATE TABLE IF NOT EXISTS [players] (" +
                       "[id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                       "[name] VARCHAR(5) NOT NULL UNIQUE," + 
                       "[score] INTEGER NOT NULL)";
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteNonQuery();
            dbconn.Close();
        }
    }

    public void AddPlayer(string name, int score) {
        using (dbconn = new SqliteConnection(conn)) {
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            sqlQuery = "INSERT OR IGNORE INTO players (name, score) VALUES (@name, @score)";
            dbcmd.CommandText = sqlQuery;
            dbcmd.Parameters.Add(new SqliteParameter("@name", name));
            dbcmd.Parameters.Add(new SqliteParameter("@score", score));
            dbcmd.ExecuteNonQuery();
            dbconn.Close();
        }
    }

    public List<Player> GetTopPlayers(int limit) {
        List<Player> players = new List<Player>();

        using (var dbconn = new SqliteConnection(conn)) {
            dbconn.Open();
            using (var dbcmd = dbconn.CreateCommand()) {
                string sqlQuery = "SELECT name, score FROM players ORDER BY score DESC LIMIT @limit";
                dbcmd.CommandText = sqlQuery;
                dbcmd.Parameters.Add(new SqliteParameter("@limit", limit));

                using (IDataReader reader = dbcmd.ExecuteReader()) {
                    while (reader.Read()) {
                        string name = reader.GetString(0);
                        int score = reader.GetInt32(1);
                        players.Add(new Player(name, score));
                    }
                }
            }
            dbconn.Close();
        }

        return players;
    }
}

public class Player {
    public string Name {
        get; private set;
    }
    public int Score {
        get; private set;
    }

    public Player(string name, int score) {
        Name = name;
        Score = score;
    }
}
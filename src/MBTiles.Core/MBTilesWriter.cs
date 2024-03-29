﻿using System;
using System.Data;
using System.Data.SQLite;
using Tiles.Tools;

namespace MBTiles.Core;

public static class MBTilesWriter
{

    private static string sqlSchema = @"BEGIN;

CREATE TABLE IF NOT EXISTS tiles (
   zoom_level integer,
   tile_column integer,
   tile_row integer,
   tile_data blob
);

CREATE TABLE IF NOT EXISTS metadata (
    name text,
    value text
);

CREATE UNIQUE INDEX IF NOT EXISTS tile_index ON tiles (zoom_level, tile_column, tile_row);
CREATE UNIQUE INDEX IF NOT EXISTS name ON metadata (name);

COMMIT;";

    public static SQLiteConnection CreateDatabase(string db, Metadata metadata)
    {
        var sqliteConnection = CreateDatabase(db, sqlSchema);
        sqliteConnection.Open();
        InsertMetadata(sqliteConnection, metadata);
        sqliteConnection.Close();
        return sqliteConnection;
    }

    public static int WriteTile(SQLiteConnection sqliteConnection, Tile t, byte[] data)
    {
        // mbtiles uses tms format so reverse y-axis...
        var tmsY = Math.Pow(2, t.Z) - 1 - t.Y;
        var cmdInsert = sqliteConnection.CreateCommand();
        cmdInsert.CommandType = CommandType.Text;
        cmdInsert.CommandText = $"INSERT INTO tiles (zoom_level, tile_column, tile_row, tile_data) VALUES ({t.Z}, {t.X}, {tmsY}, @bytes)";
        cmdInsert.Parameters.AddWithValue("@bytes", data);
        int rowsAffected = cmdInsert.ExecuteNonQuery();
        return rowsAffected;
    }

    private static SQLiteConnection CreateDatabase(string name, string schema)
    {
        string connectionString = $"Data Source={name}";
        var sqliteConnection = new SQLiteConnection(connectionString);
        sqliteConnection.Open();
        Sqlite.ExecuteCmd(sqliteConnection, schema);
        sqliteConnection.Close();
        return sqliteConnection;
    }
    private static void InsertMetadata(SQLiteConnection conn, Metadata metadata)
    {
        var sql = string.Join(
        "; "
        , $"INSERT INTO metadata (name, value) VALUES ('name', '{metadata.name}');"
        , $"INSERT INTO metadata (name, value) VALUES ('description', '{metadata.description}');"
        , $"INSERT INTO metadata (name, value) VALUES ('bounds', '{metadata.bounds}');"
        , $"INSERT INTO metadata (name, value) VALUES ('center', '{metadata.center}');"
        , $"INSERT INTO metadata (name, value) VALUES ('minzoom', '{metadata.minzoom}');"
        , $"INSERT INTO metadata (name, value) VALUES ('maxzoom', '{metadata.maxzoom}');"
        , $"INSERT INTO metadata (name, value) VALUES ('json', '{metadata.json}');"
        , $"INSERT INTO metadata (name, value) VALUES ('version', '{metadata.version}');"
        , $"INSERT INTO metadata (name, value) VALUES ('type', '{metadata.type}');"
        , "INSERT INTO metadata (name, value) VALUES ('format', 'pbf');"
        );
        Sqlite.ExecuteCmd(conn, sql);
    }
}

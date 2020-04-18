using System.Data;
using System.Data.SQLite;

namespace MBTiles.Core
{
    public static class Sqlite
    {
        public static bool ExecuteCmd(SQLiteConnection sqliteConnection, string cmdSql)
        {
            using (var cmd = sqliteConnection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdSql.ToString();
                int rowsAffected = cmd.ExecuteNonQuery();
            }
            return true;
        }
    }
}

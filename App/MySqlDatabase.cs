using System;
using MySqlConnector;

namespace App
{
    public class MySqlDatabase : IDisposable
    {
        public MySqlConnection Connection { get; set; }

        public MySqlDatabase(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
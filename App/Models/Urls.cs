using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Models;
using MySqlConnector;

namespace App.Models
{
    public class Urls
    {
        private MySqlDatabase db;
        
        public Urls(MySqlDatabase mySqlDatabase)
        {
            if (db == null)
            {
                db = mySqlDatabase;
            }
        }
        
        public async Task<List<Url>> GetUrls()
        {
            var ret = new List<Url>();

            var cmd = db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = "SELECT id, url, clicks FROM urls ORDER BY ID DESC";

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var t = new Url()
                    {
                        Id = reader.GetFieldValue<int>(0),
                        UrlData = reader.GetFieldValue<string>(1),
                        Clicks = reader.GetFieldValue<int>(2)
                    };
                    ret.Add(t);
                }
            return ret;
        }

        public void IncrementUrl(string url)
        {
            var cmd = db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"UPDATE urls SET clicks = clicks + 1 WHERE url='{url}'";
            var recs = cmd.ExecuteNonQuery();
        }

        public void AddUrl(string url)
        {
            var cmd = db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"INSERT INTO urls(url) VALUES ('{url}')";
            var recs = cmd.ExecuteNonQuery();
        }

        public void RemoveUrlById(int id)
        {
            var cmd = db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"DELETE FROM urls WHERE id={id}";
            var recs = cmd.ExecuteNonQuery();
        }
    }
}
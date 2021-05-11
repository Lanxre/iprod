using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Server.Entity;
using Server.Interface;

namespace Server.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly string _connectionString;
        private const string SqlGetAll = "Select * From services";
        
        public ServiceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ServiceRepository()
        {
            _connectionString = "Data Source=socialhelp.db";
        }

        public string GetAll()
        {
            var services = new List<Service>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(SqlGetAll, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                
                services.Add(new Service() {ServiceId = id, ServiceName = name});
                
            }
            
            return JsonSerializer.Serialize(services);
        }
    }
}
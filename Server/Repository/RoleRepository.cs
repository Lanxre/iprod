using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Server.Entity;
using Server.Interface;

namespace Server.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        private const string SqlGetAll = "Select * From roles";

        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RoleRepository()
        {
            _connectionString = "Data Source=socialhelp.db";
        }

        public string GetAll()
        {
            var roles = new List<Role>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(SqlGetAll, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                
                roles.Add(new Role()
                {
                    RoleId = id,
                    RoleName = name
                });
            }
            return JsonSerializer.Serialize(roles);
        }
    }
}
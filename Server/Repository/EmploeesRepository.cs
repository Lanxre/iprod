using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Server.Entity;
using Server.Interface;

namespace Server.Repository
{
    public class EmploeesRepository : IEmployeesRepository
    {
        private readonly string _connectionString;
        private const string SqlGetAllCuratorName = "Select employees.employees_name  From employees " + 
                                                    "where emplouees_status = 'Curator'";

        private const string SqlGetAll = "Select * From employees";

        public EmploeesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public EmploeesRepository()
        {
            _connectionString = "Data Source=socialhelp.db";
        }


        public string GetAll()
        {
            var curators = new List<Employees>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(SqlGetAll, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var empl_id = reader.GetInt32(0);
                var empl_role_id = reader.GetInt32(1);
                var wage = reader.GetInt32(2);
                var status = reader.GetString(3);
                var name = reader.GetString(4);
                var mail = reader.GetString(5);
                
                curators.Add(new Employees()
                {
                    EmployeesMail = mail,
                    EmployeesName = name,
                    EmployeesStatus = status,
                    EmployeesId = empl_id,
                    EmployeesWage = wage,
                    EmployeesRoleId = empl_role_id
                });
                
            }
            
            return JsonSerializer.Serialize(curators);
        }

        public string GetAllCuratorName()
        {
            var curatorsName = new List<string>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(SqlGetAllCuratorName, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                
                curatorsName.Add(name);
                
            }
            
            return JsonSerializer.Serialize(curatorsName);
        }
    }
}
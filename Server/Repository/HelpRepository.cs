using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Server.Entity;
using Server.Interface;

namespace Server.Repository
{
    public class HelpRepository : IHelpRepository
    {
        private readonly string _connectionString;
        private const string SqlAddHelp = "INSERT INTO help(services_id, help_status, " + 
                                           " employees_name, needy_name, help_date) "
                                           + "VALUES (@service, @help, @ename, @nname, @date)";

        private const string SqlGetHelp = "SELECT * From help where needy_name = @name";
        private const string SqlGetHelpCurator = "SELECT * From help where employees_name = @name";
        private const string SqlGetAll = "Select * From help";
        private const string Sqlupdate = "Update help Set help_status = @param where needy_name = @name";

        public HelpRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HelpRepository()
        {
            _connectionString = "Data Source=socialhelp.db";
        }

        public void Add(Help help)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand(SqlAddHelp, connection);
            var serviceParam = new SqliteParameter("@service", help.HelpServiceId);
            command.Parameters.Add(serviceParam);
            var helpParam = new SqliteParameter("@help", help.HelpMark);
            command.Parameters.Add(helpParam);
            var enameParam = new SqliteParameter("@ename", help.HelpEmployeesName);
            command.Parameters.Add(enameParam);
            var nnameParam = new SqliteParameter("@nname", help.HelpNeedyName);
            command.Parameters.Add(nnameParam);
            var dateParam = new SqliteParameter("@date", help.HelpDate);
            command.Parameters.Add(dateParam);
                
            command.ExecuteNonQuery();
        }

        public string GetHelpNeedy(string data)
        {
            var helps = new List<Help>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(SqlGetHelp, connection);
            var nameParam = new SqliteParameter("@name", data);
            command.Parameters.Add(nameParam);
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var serv_id = reader.GetInt32(0);
                var status = reader.GetString(1);
                var ename = reader.GetString(2);
                var nname = reader.GetString(3);
                var date = reader.GetDateTime(4);
                
                helps.Add(new Help()
                {
                    HelpEmployeesName = ename, 
                    HelpDate = date, 
                    HelpMark = status, 
                    HelpNeedyName = nname, 
                    HelpServiceId = serv_id
                });
                
            }
            
            return JsonSerializer.Serialize(helps);
        }

        public string GetHelpEmployee(string data)
        {
            var helps = new List<Help>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(SqlGetHelpCurator, connection);
            var nameParam = new SqliteParameter("@name", data);
            command.Parameters.Add(nameParam);
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var serv_id = reader.GetInt32(0);
                var status = reader.GetString(1);
                var ename = reader.GetString(2);
                var nname = reader.GetString(3);
                var date = reader.GetDateTime(4);
                
                helps.Add(new Help()
                {
                    HelpEmployeesName = ename, 
                    HelpDate = date, 
                    HelpMark = status, 
                    HelpNeedyName = nname, 
                    HelpServiceId = serv_id
                });
                
            }
            
            return JsonSerializer.Serialize(helps);
        }

        public string GetAll()
        {
            var helps = new List<Help>();
            
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(SqlGetAll, connection);
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var serv_id = reader.GetInt32(0);
                var status = reader.GetString(1);
                var ename = reader.GetString(2);
                var nname = reader.GetString(3);
                var date = reader.GetDateTime(4);
                
                helps.Add(new Help()
                {
                    HelpEmployeesName = ename, 
                    HelpDate = date, 
                    HelpMark = status, 
                    HelpNeedyName = nname, 
                    HelpServiceId = serv_id
                });
                
            }
            
            return JsonSerializer.Serialize(helps);
        }

        public void Update(Help help,string data)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(Sqlupdate, connection);
            var setParam = new SqliteParameter("@param", data);
            command.Parameters.Add(setParam);
            var nameParam = new SqliteParameter("@name", help.HelpNeedyName);
            command.Parameters.Add(nameParam);
            

            command.ExecuteNonQuery();
        }
    }
}
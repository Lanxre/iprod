using Microsoft.Data.Sqlite;
using Server.Entity;
using Server.Interface;

namespace Server.Repository
{
    public class NeedysRepository : INeedysRepository
    {
        private readonly string _connectionString;
        private const string SqlGetNeedy = "INSERT INTO needys(needy_role_id, needy_help, " + 
                                           " needy_status, needy_name, needy_service_id) "
                                           + "VALUES (@role, @help, @status, @name, @service)";

        private const string SqlExitNeedy = "SELECT needys.needy_name From needys where needys.needy_name = @name";
        

        public NeedysRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public NeedysRepository()
        {
            _connectionString = "Data Source=socialhelp.db";
        }
        
        public void Add(Needys needy)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand(SqlGetNeedy, connection);
            var roleParam = new SqliteParameter("@role", needy.NeedysRoleId);
            command.Parameters.Add(roleParam);
            var helpParam = new SqliteParameter("@help", needy.NeedysHelp);
            command.Parameters.Add(helpParam);
            var statusParam = new SqliteParameter("@status", needy.NeedysStatus);
            command.Parameters.Add(statusParam);
            var nameParam = new SqliteParameter("@name", needy.NeedysName);
            command.Parameters.Add(nameParam);
            var serviceParam = new SqliteParameter("@service", needy.NeedysServiceId);
            command.Parameters.Add(serviceParam);
                
            command.ExecuteNonQuery();
        }

        public bool ExistNeedy(Needys needy)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand(SqlExitNeedy, connection);
            var nameParam = new SqliteParameter("@name", needy.NeedysName);
            command.Parameters.Add(nameParam);
            using var reader = command.ExecuteReader();
            return !reader.HasRows;
        }
    }
}
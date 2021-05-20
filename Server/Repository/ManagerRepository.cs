using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Server.Entity;

namespace Server.Repository
{
    public class ManagerRepository
    {
        public UsersRepository Users { get; }

        public ServiceRepository Service { get; }
        
        public EmploeesRepository Emploees { get; }
        
        public NeedysRepository Needys { get; }
        
        public HelpRepository Help { get; }
        
        public RoleRepository Roles { get; }

        public ManagerRepository(UsersRepository usersRepository, ServiceRepository serviceRepository, 
            EmploeesRepository emploees, NeedysRepository needys, HelpRepository help, RoleRepository roles)
        {
            Users = usersRepository;
            Service = serviceRepository;
            Emploees = emploees;
            Needys = needys;
            Help = help;
            Roles = roles;
        }

        public string GetStats()
        {
            var stats = new Dictionary<string,int>(); 
            const string sqlExpression = "SELECT employees_name, count(employees_name) as 'Количество помощи' " +
                                         "from help where help_status = 'Работа выполнена' group by employees_name";
            
            
            using var connection = new SqliteConnection("Data Source=socialhelp.db");
            var command = new SqliteCommand(sqlExpression, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var count = reader.GetInt32(1);
                
                stats.Add(name,count);
            }
            
            return JsonSerializer.Serialize(stats);
        }

       

    }
}
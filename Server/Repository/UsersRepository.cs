using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Server.Entity;
using Server.Interface;

namespace Server.Repository
{
    public class UsersRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UsersRepository()
        {
            _connectionString = "Data Source=socialhelp.db";
        }
        
        public bool SqlMailChek(string mail)
        {
            const string sqlExpression = "SELECT user_mail FROM users where user_mail = 'asdfg@gmail.com'";
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand(sqlExpression, connection);
            using var reader = command.ExecuteReader();
            return !reader.HasRows;
        }

        public void Add(Users user)
        {
            const string sqlExpression = "INSERT INTO users(user_role_id,user_login,user_password,user_mail) VALUES (@role, @login, @passw, @mail)";
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand(sqlExpression, connection);
            var roleParam = new SqliteParameter("@role", user.UserRoleId);
            command.Parameters.Add(roleParam);
            var loginParam = new SqliteParameter("@login", user.UserLogin);
            command.Parameters.Add(loginParam);
            var passwParam = new SqliteParameter("@passw", user.UserPassword);
            command.Parameters.Add(passwParam);
            var mailParam = new SqliteParameter("@mail", user.UserMail);
            command.Parameters.Add(mailParam);
                
            command.ExecuteNonQuery();
        }

        public string ExistUser(Users user)
        {
            const string sqlExpression = "SELECT * FROM users where user_mail = @mail and user_password = @pass";
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = new SqliteCommand(sqlExpression, connection);
            var passwParam = new SqliteParameter("@pass", user.UserPassword);
            command.Parameters.Add(passwParam);
            var mailParam = new SqliteParameter("@mail", user.UserMail);
            command.Parameters.Add(mailParam);
            using var reader = command.ExecuteReader();
            int    role     = 0 ;
            string login    = string.Empty ;
            string password = string.Empty ;
            string mail     = string.Empty ;
            while (reader.Read())
            {
                 role = reader.GetInt32(0);
                 login = reader.GetString(1);
                 password = reader.GetString(3);
                 mail = reader.GetString(2);
            }

            var userSend = new Users()
            {
                UserLogin = login,
                UserMail = mail,
                UserPassword = password,
                UserRoleId = role
            };

            Console.WriteLine(JsonSerializer.Serialize(userSend));
            return JsonSerializer.Serialize(userSend);
        }

        public string GetAll()
        {
            var users = new List<Users>();
            const string sqlExpression = "SELECT * FROM users";
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = new SqliteCommand(sqlExpression, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                var mail = reader.GetString(2);
                var pass = reader.GetString(3);
                
                users.Add(new Users()
                {
                   UserRoleId = id,
                   UserLogin = name,
                   UserMail = mail,
                   UserPassword = pass
                });
            }
            return JsonSerializer.Serialize(users);
            
        }
    }
}
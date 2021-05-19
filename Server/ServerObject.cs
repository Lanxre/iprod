using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Server.Entity;
using Server.Repository;

namespace Server
{
    
    public class ServerObject
    {
        private static NetworkStream   Stream            {get; set;}
        
        private static TcpListener     _tcpListener;
        private static ServerMetaDats  _metaDats;

        private static ManagerRepository _manager;
        
        protected internal static void Listen()
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Any, 8888);
                _tcpListener.Start();
                _metaDats = new ServerMetaDats();
                _manager = new ManagerRepository(new UsersRepository(), new ServiceRepository(),
                    new EmploeesRepository(), new NeedysRepository(), new HelpRepository(), new RoleRepository());
                ConstString.Sendler = "Server running";
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                

                while (true)
                {
                    var tcpClient = _tcpListener.AcceptTcpClient();
                    Stream = tcpClient.GetStream();
                    var data =  GetMessage();
                    if (!_metaDats.SenderStatus)
                    {
                        _metaDats.SenderStatus = true;
                        _metaDats.GetMetaDats(data);
                    }
                    else
                    {
                        _metaDats.SenderStatus = false;
                        AddItem(data);
                        GetItem(data);
                        UpdateItem(data);
                    }
                    Console.WriteLine(data);

                    if (_metaDats.SendMessage) 
                        SendMessage(ConstString.Sendler);

                    
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }
        private static string GetMessage()
        {
            var data = new byte[64]; 
            var builder = new StringBuilder();
            do
            {
                var bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);
 
            return builder.ToString();
        }
        
        
        protected internal static void Disconnect()
        {
            _tcpListener.Stop(); 
            Environment.Exit(0); 
        }
        
        private static void SendMessage(string senderObject)
        {
            var data = Encoding.UTF8.GetBytes(senderObject);
            Stream.Write(data, 0, data.Length);
        }

        private static void AddItem(string data)
        {
            if (_metaDats.FuncAddBd != "Add") return;
            switch (_metaDats.TypeClassSend)
            {
                    case "Users":
                        var user = JsonSerializer.Deserialize<Users>(data);
                        if (user != null && !_manager.Users.SqlMailChek(user.UserMail))
                        {
                            _manager.Users.Add(user);
                            MailOperation.SendMail(user);
                            ConstString.Sendler = ConstString.CompleteRegister;
                        }
                        break;
                    case "Needys":
                        var needy = JsonSerializer.Deserialize<Needys>(data);
                        if (needy != null && _manager.Needys.ExistNeedy(needy))
                        {
                            _manager.Needys.Add(needy);
                        }
                        break;
                    case "Help":
                        var help = JsonSerializer.Deserialize<Help>(data);
                        if (help != null)
                        {
                            _manager.Help.Add(help);
                        }
                        break;
            }
        }

        private static void GetItem(string data)
        {
            if (!_metaDats.GetItem) return;
            switch (_metaDats.TypeClassSend)
            {
                case "Users":
                    switch (_metaDats.GetType)
                    {
                        case 1:
                            var user = JsonSerializer.Deserialize<Users>(data);
                            ConstString.Sendler = _manager.Users.ExistUser(user);
                            break;
                        case 2:
                            ConstString.Sendler = _manager.Users.GetAll();
                            break;
                    }
                    break;
                case "Service":
                    ConstString.Sendler = _manager.Service.GetAll();
                    Console.WriteLine("Услуги -> " +  ConstString.Sendler);
                    break;
                case "Employees":
                    ConstString.Sendler = _metaDats.GetType switch
                    {
                        1 => _manager.Emploees.GetAllCuratorName(),
                        2 => _manager.Emploees.GetAll(),
                        _ => ConstString.Sendler
                    };
                    Console.WriteLine("Кураторы -> " + ConstString.Sendler);
                    break;
                case "Help":
                    switch (_metaDats.GetType)
                    {
                        case 1:
                            var needys = JsonSerializer.Deserialize<string>(data);
                            ConstString.Sendler = _manager.Help.GetHelpNeedy(needys);
                            break;
                        case 2:
                            var curators = JsonSerializer.Deserialize<string>(data);
                            ConstString.Sendler = _manager.Help.GetHelpEmployee(curators);
                            break;
                        case 3:
                            ConstString.Sendler = _manager.Help.GetAll();
                            break;
                    }
                    break;
                case "Roles":
                    ConstString.Sendler = _manager.Roles.GetAll();
                    Console.WriteLine("Роли -> " +  ConstString.Sendler);
                    break;
                
                          
                        
            }
        }

        private static void UpdateItem(string data)
        {
            if (!_metaDats.UpdateItem) return;

            switch (_metaDats.TypeClassSend)
            {
                case "Help":
                    var help = JsonSerializer.Deserialize<Help>(data);
                    switch (_metaDats.GetType)
                    {
                        case 1:
                            _manager.Help.Update(help,"Выехал");
                            break;
                        case 2:
                            _manager.Help.Update(help,"Работа выполнена");
                            break;
                    }
                    
                    break;
            }
        }




    }

    
}

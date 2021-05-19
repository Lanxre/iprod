using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Server;
using Server.Entity;

namespace iprod
{

    public partial class AdminWindow : Window
    {
        private static Employees _employees;
        private static Dictionary<string, int> _services;
        private static Dictionary<string, int> _roles;
 
        public AdminWindow()
        {
            InitializeComponent();
        }

        public AdminWindow(Users users, Employees employees)
        {
            InitializeComponent();
            _employees = employees;
            Login_User.Text = users.UserLogin;
            empl_name.Text = employees.EmployeesName;
        }

        private void Проверка_нуждающихся_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BG.Visibility = Visibility.Hidden;
            data_service.Visibility = Visibility.Hidden;
            grid_h.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;
            web_site.Visibility = Visibility.Hidden;
            service_add.Visibility = Visibility.Hidden;
            add_users.Visibility = Visibility.Hidden;
            //
            search_needy.Visibility = Visibility.Visible;


        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_Услуги.Visibility = Visibility.Collapsed;
                tt_Информация.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
                tt_Подпись_на_услугу.Visibility = Visibility.Collapsed;
                tt_Проверка_нуждающихся.Visibility = Visibility.Collapsed;
                tt_addUser.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_Услуги.Visibility = Visibility.Visible;
                tt_Информация.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
                tt_Подпись_на_услугу.Visibility = Visibility.Visible;
                tt_Проверка_нуждающихся.Visibility = Visibility.Visible;
                tt_addUser.Visibility = Visibility.Visible;
            }

        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BG.Visibility = Visibility.Visible;
            grid_h.Visibility = Visibility.Visible;
            CloseBtn.Visibility = Visibility.Visible;
            // 
            web_site.Visibility = Visibility.Hidden;
            data_service.Visibility = Visibility.Hidden;
            service_add.Visibility = Visibility.Hidden;
            search_needy.Visibility = Visibility.Hidden;
            add_users.Visibility = Visibility.Hidden;
        }

        private void AddUsers_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BG.Visibility = Visibility.Hidden;
            web_site.Visibility = Visibility.Hidden;
            grid_h.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;
            service_add.Visibility = Visibility.Hidden;
            search_needy.Visibility = Visibility.Hidden;
            data_service.Visibility = Visibility.Hidden;
            //
            add_users.Visibility = Visibility.Visible;
            
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Roles",
                SendMessage = true,
                GetItem = true
            };
            
            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(string.Empty));
            
            _roles = GetDict(JsonSerializer.Deserialize<List<Role>>(DataSender.Message)
                .Select(x => x.RoleName).ToList());
            
            roles.ItemsSource = JsonSerializer.Deserialize<List<Role>>(DataSender.Message)
                .Select(x => x.RoleName);
        }

        private void Услуги_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BG.Visibility = Visibility.Hidden;
            web_site.Visibility = Visibility.Hidden;
            grid_h.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;
            service_add.Visibility = Visibility.Hidden;
            search_needy.Visibility = Visibility.Hidden;
            add_users.Visibility = Visibility.Hidden;
            //
            data_service.Visibility = Visibility.Visible;
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Service",
                SendMessage = true,
                GetItem = true
            };
            var services = string.Empty;

            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(services));
            data_service.ItemsSource = JsonSerializer.Deserialize<List<Service>>(DataSender.Message);

        }
        private void Информация_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BG.Visibility = Visibility.Hidden;
            data_service.Visibility = Visibility.Hidden;
            grid_h.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;
            service_add.Visibility = Visibility.Hidden;
            search_needy.Visibility = Visibility.Hidden;
            add_users.Visibility = Visibility.Hidden;
            //
            web_site.Navigate(new Uri(AppDomain.CurrentDomain.BaseDirectory + "web/info.html"));
            web_site.Visibility = Visibility.Visible;
        }
        private void Out_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void Подпись_на_услугу_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BG.Visibility = Visibility.Hidden;
            data_service.Visibility = Visibility.Hidden;
            grid_h.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;
            web_site.Visibility = Visibility.Hidden;
            search_needy.Visibility = Visibility.Hidden;
            add_users.Visibility = Visibility.Hidden;
            //
            service_add.Visibility = Visibility.Visible;

            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Employees",
                SendMessage = true,
                GetItem = true
            };
            var curator = string.Empty;

            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(curator));
            GetDict(JsonSerializer.Deserialize<List<string>>(DataSender.Message));
            curator_list.ItemsSource = JsonSerializer.Deserialize<List<string>>(DataSender.Message);

            var settings1 = new ServerMetaDats()
            {
                TypeClassSend = "Service",
                SendMessage = true,
                GetItem = true
            };
            var services = string.Empty;

            DataSender.OperationDataSend(JsonSerializer.Serialize(settings1));
            DataSender.OperationDataSend(JsonSerializer.Serialize(services));
            _services = GetDict(JsonSerializer.Deserialize<List<Service>>(DataSender.Message)
                .Select(x => x.ServiceName)
                .ToList());
            service_list.ItemsSource = JsonSerializer.Deserialize<List<Service>>(DataSender.Message).
                Select(x => x.ServiceName);

        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validations.IsNull(needy_name.Text, needy_status.Text, needy_help.Text,
                service_list.SelectedItem.ToString())) return;

            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Needys",
                SendMessage = true,
                FuncAddBd = "Add"
            };
            var needy = new Needys()
            {
                NeedysName = needy_name.Text,
                NeedysStatus = needy_status.Text,
                NeedysRoleId = 2,
                NeedysServiceId = _services[service_list.SelectedItem.ToString()],
                NeedysHelp = needy_help.Text

            };
            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(needy));


            var settings1 = new ServerMetaDats()
            {
                TypeClassSend = "Help",
                SendMessage = true,
                FuncAddBd = "Add"
            };

            var helpSend = new Help()
            {
                HelpServiceId = _services[service_list.SelectedItem.ToString()],
                HelpDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                HelpMark = "Ожидается",
                HelpEmployeesName = curator_list.SelectedItem.ToString(),
                HelpNeedyName = needy_name.Text
            };
            DataSender.OperationDataSend(JsonSerializer.Serialize(settings1));
            DataSender.OperationDataSend(JsonSerializer.Serialize(helpSend));



        }

        private static Dictionary<string, int> GetDict(List<string> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var cDictionary = new Dictionary<string, int>();
            for (var i = 0; i < items.Count; i++)
            {
                cDictionary.Add(items[i], i + 1);
            }

            return cDictionary;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (Validations.IsNull(needy_name.Text)) return;

            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Help",
                SendMessage = true,
                GetItem = true,
                GetType = 1
            };

            var helpNeedyName = needy_name.Text;

            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(helpNeedyName));

            data_subscribe_service.ItemsSource = JsonSerializer.Deserialize<List<Help>>(DataSender.Message);

        }

        private void Check_Needy_Btn1(object sender, RoutedEventArgs e)
        {
            datagrid_search_needy.Visibility = Visibility.Hidden;
            datagrid_search_needy_curator.Visibility = Visibility.Visible;
            f1_btn.Visibility = Visibility.Visible;
            f2_btn.Visibility = Visibility.Visible;
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Help",
                SendMessage = true,
                GetItem = true,
                GetType = 2
            };

            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(_employees.EmployeesName));

            datagrid_search_needy_curator.ItemsSource = JsonSerializer.Deserialize<List<Help>>(DataSender.Message);


        }

        private void Check_Needy_Btn2(object sender, RoutedEventArgs e)
        {
            datagrid_search_needy.Visibility = Visibility.Visible;
            datagrid_search_needy_curator.Visibility = Visibility.Hidden;
            f1_btn.Visibility = Visibility.Hidden;
            f2_btn.Visibility = Visibility.Hidden;
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Help",
                SendMessage = true,
                GetItem = true,
                GetType = 3
            };

            var empty = string.Empty;

            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(empty));

            datagrid_search_needy.ItemsSource = JsonSerializer.Deserialize<List<Help>>(DataSender.Message);

        }

        private void Out_Click(object sender, RoutedEventArgs e)
        {
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Help",
                UpdateItem = true,
                SendMessage = true,
                GetType = 1

            };
            var needy = datagrid_search_needy_curator.SelectedItem as Help;

            var help = new Help()
            {
                HelpNeedyName = needy.HelpNeedyName,
                HelpMark = "Ожидается"
            };


            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(help));
            Check_Needy_Btn1(sender, e);
        }
        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Help",
                UpdateItem = true,
                SendMessage = true,
                GetType = 2

            };
            var needy = datagrid_search_needy_curator.SelectedItem as Help;

            var help = new Help()
            {
                HelpNeedyName = needy.HelpNeedyName,
                HelpMark = "Ожидается"
            };


            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(help));

            Check_Needy_Btn1(sender, e);
        }
        private void Add_Users(object sender, RoutedEventArgs e)
        {
            if (Validations.IsNull(roles.SelectedItem.ToString(),add_login.Text,
                add_mail.Text,add_pass.Password)) return;
            
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Users",
                SendMessage = true,
                FuncAddBd = "Add"
            };
            var user = new Users()
            {
                UserRoleId = _roles[roles.SelectedItem.ToString()],
                UserLogin = add_login.Text,
                UserMail = add_mail.Text,
                UserPassword = add_pass.Password
            };
            
            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(user));
            
            Reload_Users(sender,e);
        }
        private void Reload_Users(object sender, RoutedEventArgs e)
        {
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Users",
                SendMessage = true,
                GetItem = true,
                GetType = 2
            };
            
            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(string.Empty));

            data_roles.ItemsSource = JsonSerializer.Deserialize<List<Users>>(DataSender.Message);
        }




        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}

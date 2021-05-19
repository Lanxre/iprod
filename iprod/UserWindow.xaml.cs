using Server;
using Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace iprod
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly Users _user;
        private static Dictionary<string, int> _curators;
        private static Dictionary<string, int> _services;
        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(Users user)
        {
            InitializeComponent();
            _user = user;

            Login_User.Text = _user.UserLogin;
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
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_Услуги.Visibility = Visibility.Visible;
                tt_Информация.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
                tt_Подпись_на_услугу.Visibility = Visibility.Visible;
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

        }
        private void Услуги_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BG.Visibility = Visibility.Hidden;
            web_site.Visibility = Visibility.Hidden;
            grid_h.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;
            service_add.Visibility = Visibility.Hidden;
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

            service_add.Visibility = Visibility.Visible;
            
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Employees",
                SendMessage = true,
                GetItem = true,
                GetType = 1
            };
            var curator = string.Empty;

            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(curator));
            _curators = GetDict(JsonSerializer.Deserialize<List<string>>(DataSender.Message));
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
                Select(x =>x.ServiceName);
            
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

        private static Dictionary<string,int> GetDict(List<string> items)
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

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}

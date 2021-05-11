using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Server;
using Server.Entity;
using System.Linq;

namespace iprod 
{
    public partial class MainWindow
    {
        
        private bool _checker = true;
        public MainWindow()
        {
            InitializeComponent();
        }
       

        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MiniButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var settings = new ServerMetaDats()
            {
                TypeClassSend = "Users",
                SendMessage = true,
                GetItem = true
            };
            var userSend = new Users()
            {
                UserMail = Mail.Text,
                UserPassword = Password.Password,
            };
            
            DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
            DataSender.OperationDataSend(JsonSerializer.Serialize(userSend));
            var userGet = JsonSerializer.Deserialize<Users>(DataSender.Message);
            switch (userGet?.UserRoleId)
            {
                case 1:
                    Close();
                    break; 
                case 2:
                    var userWindow = new UserWindow(userGet);
                    userWindow.Show();
                    Close();
                    break;
                case 3:
                    var guestWindow = new GuestWindow();
                    guestWindow.Show();
                    Close();
                    break;
                case 4:
                    var settings1 = new ServerMetaDats()
                    {
                        TypeClassSend = "Employees",
                        SendMessage = true,
                        GetItem = true,
                        GetType = 2

                    };
                    var curator = string.Empty;

                    DataSender.OperationDataSend(JsonSerializer.Serialize(settings1));
                    DataSender.OperationDataSend(JsonSerializer.Serialize(curator));

                    var eml = (JsonSerializer.Deserialize<List<Employees>>(DataSender.Message))
                        .First(x => x.EmployeesMail == userGet.UserMail);

                    var employeesWindow = new EmployeeWindow(userGet,eml);
                    employeesWindow.Show();
                    Close();
                    break;
            }

            
        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SwitchMenu(_checker);
        }

        private void SwitchMenu(bool pressed)
        {
            switch (pressed)
            {
                case true:
                    SignLogin.Text = "Регестрация";
                    Login.Visibility = Visibility.Visible;
                    buttonJoin.Visibility = Visibility.Hidden;
                    buttonRegister.Visibility = Visibility.Visible;
                    guest_join.Visibility = Visibility.Hidden;
                    buttonChoise.Content = "Хотите войти?";
                    _checker = false;
                    break;
                default:
                    SignLogin.Text = "Вход";
                    Login.Visibility = Visibility.Hidden;
                    buttonJoin.Visibility = Visibility.Visible;
                    buttonRegister.Visibility = Visibility.Hidden;
                    guest_join.Visibility = Visibility.Visible;
                    buttonChoise.Content = "Не зарегестрированы?";
                    _checker = true;
                    break;
            }
        }


        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            var messageBox = new CustomMessgeBox();
            if (Validations.ValidRegister(Login.Text, Password.Password))
            {
                var user = new Users()
                {
                    UserLogin = Login.Text,
                    UserMail = Mail.Text,
                    UserPassword = Password.Password,
                    UserRoleId = 2
                };
                var settings = new ServerMetaDats()
                {
                    TypeClassSend = "Users",
                    SendMessage = true,
                    FuncAddBd = "Add"
                };
                
                DataSender.OperationDataSend(JsonSerializer.Serialize(settings));
                DataSender.OperationDataSend(JsonSerializer.Serialize(user));
                
                messageBox.Title = "Вы зарегестрированы";
                messageBox.Info.Text = DataSender.Message;
            }
            else
            {
                messageBox.Title = "Ошибка";
                messageBox.Info.Text = "Не правильные данные";
            }
            
            messageBox.ShowDialog();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start("explorer.exe", e.Uri.ToString());
            e.Handled = true;
        }

        private void GuestJoin_Click(object sender, RoutedEventArgs e)
        {
            var gw = new GuestWindow();
            gw.Show();
            Close();
        }




    }

    
}

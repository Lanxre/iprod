using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Server;
using Server.Entity;

namespace iprod
{
    /// <summary>
    /// Логика взаимодействия для GuestWindow.xaml
    /// </summary>
    public partial class GuestWindow : Window
    {
        public GuestWindow()
        {
            InitializeComponent();
        }
        

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {

            if(Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_Услуги.Visibility = Visibility.Collapsed;
                tt_Информация.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_Услуги.Visibility = Visibility.Visible;
                tt_Информация.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
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
            web_site.Visibility = Visibility.Hidden;
            data_service.Visibility = Visibility.Hidden;
        }
        private void Услуги_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BG.Visibility = Visibility.Hidden;
            web_site.Visibility = Visibility.Hidden;
            data_service.Visibility = Visibility.Visible;
            grid_h.Visibility = Visibility.Hidden;
            CloseBtn.Visibility = Visibility.Hidden;
            
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
            web_site.Navigate(new Uri(AppDomain.CurrentDomain.BaseDirectory + "web/info.html"));
            web_site.Visibility = Visibility.Visible;
        }
        private void Out_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        
    }
}

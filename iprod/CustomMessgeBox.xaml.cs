using System.Windows;
using System.Windows.Input;

namespace iprod
{
    public partial class CustomMessgeBox : Window
    {
        public CustomMessgeBox()
        {
            InitializeComponent();
        }

        private void Button_Attempt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Header_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatWindowManager.UserControls
{
    /// <summary>
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : UserControl
    {
        public event EventHandler<src.UIEvent> createUserEvent;

        public CreateNewUser()
        {
            InitializeComponent();
        }

        private void repeatPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string repeatPassword, password;
            repeatPassword = repeatPasswordBox.Password;
            password = passwordBox.Password;
            if (!repeatPassword.Equals(password))
            {
                repeatPasswordMsg.Visibility = Visibility.Visible;
            }
            else
            {
                repeatPasswordMsg.Visibility = Visibility.Hidden;
            }
        }

        private void repeatPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pvb = (PasswordBox)sender;
            pvb.Clear();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<src.UIEvent> handler = createUserEvent;
            if (createUserEvent != null)
            {
                handler(this, new src.UIEvent("UserLogIn"));
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<src.UIEvent> handler = createUserEvent;
            if (createUserEvent != null)
            {
                handler(this, new src.UIEvent("UserLogIn"));
            }
        }
    }
}

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
using GoDaddyClient.ServiceReference;

namespace ChatWindowManager.UserControls
{
    /// <summary>
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : UserControl
    {
        public event EventHandler<src.UIEvent> createUserEvent;

        private GoDaddyClient.Client clientLogic;

        public CreateNewUser(GoDaddyClient.Client clientLogic)
        {
            InitializeComponent();
            this.clientLogic = clientLogic;
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

            if (passwordBox.Password.Equals(repeatPasswordBox.Password))
            {
                User user = new User()
                {
                    firstName = this.txtFirstName.Text,
                    lastName = this.txtLastName.Text,
                    userName = this.txtUserName.Text,
                    password = this.passwordBox.Password
                };
                string confirmation = clientLogic.register(user);
                confirmRegsiter(confirmation);
            }
            else
            {
                displayPasswordError();
            }     
        }

        private void confirmRegsiter(string confirmation)
        {
            switch (confirmation)
            {
                case "SUCCES":
                    clearFields();
                    EventHandler<src.UIEvent> handler = createUserEvent;
                    if (createUserEvent != null)
                    {
                        handler(this, new src.UIEvent("UserLogIn"));
                    }
                    break;

                case "FAIL":
                    accountRegiserFail();
                    break;
            }
        }

        private void clearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            passwordBox.Clear();
            repeatPasswordBox.Clear();
            txtUserName.Clear();
        }

        private void accountRegiserFail()
        {
            MessageBox.Show("An error occured, check your enteret connection or try another user name"
                , "Rigister Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void displayPasswordError()
        {
            MessageBox.Show("Entered password does not mach reentered password."
                , "Rigister Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            repeatPasswordBox.Clear();
            repeatPasswordBox.Focus();
        }
    }
}

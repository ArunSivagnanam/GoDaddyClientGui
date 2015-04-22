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
using System.Drawing;



namespace ChatWindowManager.UserControls

{
    /// <summary>
    /// Interaction logic for UserLogIn.xaml
    /// </summary>
    public partial class UserLogIn : UserControl
    {
        public event EventHandler<src.UIEvent> userEvent;
        public static src.Client loggedInClient;

        public UserLogIn()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            rtb.Document.Blocks.Clear();
            rtb.FontWeight = FontWeights.Normal;
            rtb.FontStyle = FontStyles.Normal; 
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            string text = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
            if (String.IsNullOrWhiteSpace(text))
            {
                txtUsernameImage.Source = new BitmapImage(new Uri("../Images/bullet_red.png", UriKind.RelativeOrAbsolute));
                rtb.FontWeight = FontWeights.Light;
                rtb.FontStyle = FontStyles.Italic;
                flowDocumentUsername.Blocks.Add(paragraphUsername);
            }
            else
            {
                txtUsernameImage.Source = new BitmapImage(new Uri("../Images/bullet_green.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void ptxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pwb = (PasswordBox)sender;
            string password = pwb.Password;
            if (String.IsNullOrWhiteSpace(password))
            {
                ptxtPasswordImage.Source = new BitmapImage(new Uri("../Images/bullet_red.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                ptxtPasswordImage.Source = new BitmapImage(new Uri("../Images/bullet_green.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void ptxtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pwb = (PasswordBox)sender;
            pwb.Clear();
        }

        private void ptxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Console.CapsLock)
            {
                capsLockWarning.Visibility = Visibility.Visible;
            }
            else
            {
                capsLockWarning.Visibility = Visibility.Hidden;
            }
        }

        private void newUserHyperLink_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<src.UIEvent> handler = userEvent;
            if(userEvent != null)
            {
                handler(this, new src.UIEvent("CreateNewUser"));
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<src.UIEvent> handler = userEvent;
            if (userEvent != null)
            {
                handler(this, new src.UIEvent("Login"));
            }
        }
    }
}

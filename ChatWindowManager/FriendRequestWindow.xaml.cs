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
using System.Windows.Shapes;

namespace ChatWindowManager
{
    /// <summary>
    /// Interaction logic for FriendRequestWindow.xaml
    /// </summary>
    public partial class FriendRequestWindow : Window
    {
        private GoDaddyClient.Client clientLogic;

        public FriendRequestWindow(GoDaddyClient.Client clientLogic)
        {
            this.clientLogic = clientLogic;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            txtFriendName.Clear();
            this.Close();
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            string friendName = txtFriendName.Text;
            if (!friendName.Equals(String.Empty))
            {
                clientLogic.AddFriend(friendName);
                txtFriendName.Clear();
                MessageBoxResult result = MessageBox.Show("Request send to user " + friendName, "Request Confirmation");  
                this.Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            txtFriendName.Clear();
            this.Close();
        }
    }
}

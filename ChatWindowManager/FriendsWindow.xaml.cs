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

namespace ChatWindowManager.UserControls
{
    public partial class FriendsWindow : Window
    {
        GoDaddyClient.Client clientLogic;
  



        public FriendsWindow(GoDaddyClient.Client clientLogic)
        {
            InitializeComponent();
            DisplayFriendsUS displayFriendsUS = new DisplayFriendsUS(clientLogic);
            friendsWindowContent.Content = displayFriendsUS;
            this.clientLogic = clientLogic;
            this.Show();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clientLogic.logOut();
        }

    }
}

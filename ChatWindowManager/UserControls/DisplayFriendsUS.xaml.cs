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
    /// Interaction logic for DisplayFriendsUS.xaml
    /// </summary>
    public partial class DisplayFriendsUS : UserControl
    {
        private GoDaddyClient.Client clientLogic;

        public DisplayFriendsUS(GoDaddyClient.Client clientLogic)
        {
            InitializeComponent();
            fillFriendsList();
            this.clientLogic = clientLogic;
        }

        private void fillFriendsList()
        {
            List<User> items = new List<User>();
            items.Add(new User() { UserName = "Batman", Status = Availability.Offline });
            items.Add(new User() { UserName = "SuperMan", Status = Availability.Offline });
            items.Add(new User() { UserName = "Sexy-98675", Status = Availability.Online });
            items.Add(new User() { UserName = "Mom", Status = Availability.FriendRequest });
            friendsList.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(friendsList.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            view.GroupDescriptions.Add(groupDescription);
        }



        public enum Availability { Online, Offline, FriendRequest };

        public class User
        {
            public string UserName { get; set; }

            public Availability Status { get; set; }

            public override string ToString()
            {
                return this.UserName;
            }
        }

        private void friendsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChatWindow cw = new ChatWindow(clientLogic);
            cw.Show();
        }
    }
}

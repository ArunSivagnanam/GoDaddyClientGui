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
    /// Interaction logic for DisplayFriendsUS.xaml
    /// </summary>
    public partial class DisplayFriendsUS : UserControl
    {
        private GoDaddyClient.Client clientLogic;

        public DisplayFriendsUS(GoDaddyClient.Client clientLogic)
        {
            InitializeComponent();           
            this.clientLogic = clientLogic;
            userNameLabel.Content = clientLogic.currentUser.userName;
            fillFriendsList();
        }

        private void fillFriendsList()
        {
                           
               List<User> FriendList = clientLogic.RecieveFriendList();
               List<Usera> items = new List<Usera>();
               List<User> pendingFriends = clientLogic.ReciveFriendsToAccept();

                //Online/Offline Friends added to list
                foreach (User u in FriendList) {
                        switch(u.status){
                            case 0:
                             items.Add(new Usera() { UserName = u.userName, Status = Availability.Offline });
                        break;

                            case 1:
                             items.Add(new Usera() { UserName = u.userName, Status = Availability.Online });
                        break;            
                    }
                }
                
                //Pending friends list added
                foreach (User u in pendingFriends)
                {
                   items.Add(new Usera() { UserName = u.userName, Status = Availability.FriendRequest });

                }

            friendsList.ItemsSource = items;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(friendsList.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            view.GroupDescriptions.Add(groupDescription);
        }



        public enum Availability { Online, Offline, FriendRequest };

        public class Usera
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
            //Click event som åbner Channel til specifikke brugere
            ChatWindow cw = new ChatWindow(clientLogic);
            cw.Show();
        }
    }
}

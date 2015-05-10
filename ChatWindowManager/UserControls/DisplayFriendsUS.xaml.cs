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
        Dictionary<String, GuiUser> guiFriends = new Dictionary<string, GuiUser>();
        

        public DisplayFriendsUS(GoDaddyClient.Client clientLogic)
        {
            InitializeComponent();           
            this.clientLogic = clientLogic;
            userNameLabel.Content = clientLogic.currentUser.userName;
            clientLogic.RecieveFriendList();
            clientLogic.ReciveFriendsToAccept();
            updateGuiFriendList();

            clientLogic.friendListEvent += updateFriendList; // sat til at catche update userevent 
        }

        public void updateFriendList(object sender, GoDaddyClient.FriendListEvent e)
        {
            foreach (User u in clientLogic.friendsList)
            {
                if (u.ID == e.u.ID)
                {
                    u.status = 1;
                }
            }
            updateGuiFriendList();
        }


        public void updateGuiFriendList() // updatere gui
        {
            
            List<GuiUser> guiUserList = new List<GuiUser>();

            //Online/Offline Friends added to list
            foreach (User u in clientLogic.friendsList)
            {
                GuiUser newUser = new GuiUser()
                {
                    UserName = u.userName
                };
                switch (u.status)
                {
                    case 0:
                        newUser.Status = Availability.Offline;
                        guiUserList.Add(newUser);
                        break;

                    case 1:
                        newUser.Status = Availability.Online;
                        guiUserList.Add(newUser);
                        break;
                }
            }

            //Pending friends list added
            foreach (User u in clientLogic.friendsToAccept)
            {
                GuiUser pendingUser = new GuiUser()
                {
                    UserName = u.userName,
                    Status = Availability.FriendRequest
                };
                guiUserList.Add(new GuiUser() { UserName = u.userName, Status = Availability.FriendRequest });

            }
            friendsListView.ItemsSource = guiUserList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(friendsListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            view.GroupDescriptions.Add(groupDescription);

        }

        public enum Availability { Online, Offline, FriendRequest };

        public class GuiUser
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

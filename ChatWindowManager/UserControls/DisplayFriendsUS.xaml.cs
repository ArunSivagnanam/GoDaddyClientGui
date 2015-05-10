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
        private CollectionView view;
        Dictionary<string, ChatWindow> windows = new Dictionary<string, ChatWindow>();
       
        public DisplayFriendsUS(GoDaddyClient.Client clientLogic)
        {
            InitializeComponent();           
            this.clientLogic = clientLogic;
            userNameLabel.Content = clientLogic.currentUser.userName;
            clientLogic.RecieveFriendList();
            clientLogic.ReciveFriendsToAccept();
            initFriendList();
          
            clientLogic.friendListEvent += updateGuiFriendList; // sat til at catche update userevent 
        }

        public void updateGuiFriendList(object sender, GoDaddyClient.FriendListEvent e)
        {
            
           
            List<ListUser> listUserSouce = new List<ListUser>();

            foreach (User u in clientLogic.friendsList)
            {
                listUserSouce.Add(new ListUser(u.userName, u.Status));
            }

            foreach (User u in clientLogic.friendsToAccept)
            {
                ListUser user = new ListUser(u.userName, Availability.FriendRequest);
                listUserSouce.Add(user);
            }

           
            setListViewProperty(listUserSouce);
            friendsListView.UpdateLayout();
        }

        
        public void initFriendList(){

            List<ListUser> listUserSouce = new List<ListUser>();

            foreach (User u in clientLogic.friendsList)
            {
                listUserSouce.Add(new ListUser(u.userName, u.Status));
            }

            foreach (User u in clientLogic.friendsToAccept)
            {
                ListUser user = new ListUser(u.userName, Availability.FriendRequest);
                listUserSouce.Add(user);
            }


            setListViewProperty(listUserSouce);
        }

        private void setListViewProperty(List<ListUser> listUserSouce)
        {
            friendsListView.ItemsSource = listUserSouce;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(friendsListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            view.GroupDescriptions.Add(groupDescription);
        }


        public override string ToString()
        {
            return base.ToString();
        }
       

        private void friendsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ListView view = (ListView)sender;
            ListUser userItem = (ListUser)(view.SelectedValue); // selected item?
            User receiverUser = new User();

            foreach (User u in clientLogic.friendsList)
            {
                if (u.userName == userItem.username)
                {
                    receiverUser = u;
                }
            }

            Console.WriteLine("Hej " + userItem.username);
            //Click event som åbner Channel til specifikke brugere

            if (!(windows.ContainsKey(receiverUser.userName)))
            {
                // vindue ikke åben
                ChatWindow cw = new ChatWindow(clientLogic, receiverUser);
                windows.Add(receiverUser.userName, cw);
                cw.Show();
            }
           
        }


        class ListUser
        {
            public string username;
            public Availability Status { get; set;}

            public ListUser(string username,Availability status)
            {
                this.username = username;
                this.Status = status;
            }

            public override string ToString()
            {
                return username;
            }
        }

    }
}

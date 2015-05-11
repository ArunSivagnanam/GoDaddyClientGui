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
            clientLogic.msgEvent += clientLogic_msgEvent;
          
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


       
       

        private void friendsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ListView view = (ListView)sender;
            ListUser userItem = (ListUser)(view.SelectedValue); // selected item?
            User receiverUser = new User();
            bool found = false;

            foreach (User u in clientLogic.friendsList)
            {
                if (u.userName == userItem.username)
                {
                    receiverUser = u;
                    found = true;
                }
            }

            if (found==false)
            {
                foreach (User u in clientLogic.friendsToAccept)
                {
                    if (u.userName == userItem.username)
                    {
                        receiverUser = u;
                        
                    }
                }
            }

            Console.WriteLine("Hej " + userItem.username);
            //Click event som åbner Channel til specifikke brugere

            if (receiverUser.Status == Availability.FriendRequest)
            {
                // pop yes no
                MessageBoxResult msgBoxResult = MessageBox.Show("Accept friend request from " + receiverUser.userName, "Request", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (msgBoxResult)
                {
                    case MessageBoxResult.Yes:
                        clientLogic.AcceptFriend(receiverUser.userName);
                        Console.WriteLine("sent accept to " + receiverUser.userName);
                        clientLogic.friendsToAccept.Remove(receiverUser); 
                        break;
                    case MessageBoxResult.No:
                        clientLogic.friendsToAccept.Remove(receiverUser);
                        break;
                }
                
            }
            else
            {
                if (!(windows.ContainsKey(receiverUser.userName)))
                {
                    // vindue ikke åben
                    ChatWindow cw = new ChatWindow(clientLogic, receiverUser.userName, windows);
                    windows.Add(receiverUser.userName, cw);
                    cw.Show();
                }
            }

            
           
        }

        private void Button_Click_SendFriendRequest(object sender, RoutedEventArgs e)
        {
            Window sendFriendRequestWindow = new FriendRequestWindow(this.clientLogic);
            sendFriendRequestWindow.Show();
        }

        void clientLogic_msgEvent(object sender, GoDaddyClient.MessageEvent e)
        {

            if ((windows.ContainsKey(e.message.senderUserName)))
            {

                ChatWindow cw = windows[e.message.senderUserName];

                // vindue ikke åben
                cw.chatWindowsUS.DisplayText(e.message);
            }
            else
            {
                ChatWindow newWind = new ChatWindow(clientLogic, e.message.senderUserName, windows);
                windows.Add(e.message.senderUserName, newWind);
                newWind.chatWindowsUS.DisplayText(e.message);
                newWind.Show();
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

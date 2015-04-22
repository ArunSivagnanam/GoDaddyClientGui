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
using ChatWindowManager.UserControls;

namespace ChatWindowManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ChatRoom chatroom = new ChatRoom();
            //MainWindowContent.Content = chatroom;
            UserLogIn loginUI = new UserLogIn();
            MainWindowContent.Content = loginUI;
            loginUI.userEvent += eventOccurred;
        }

        void eventOccurred(object sender, src.UIEvent e)
        {
            switch(e.window)
            {
                case "CreateNewUser":
                    CreateNewUser createNewUserUI = new CreateNewUser();
                    createNewUserUI.createUserEvent += eventOccurred;
                    MainWindowContent.Content = createNewUserUI; 
                    break;
                case "UserLogIn":
                    UserLogIn loginUI = new UserLogIn();
                    loginUI.userEvent += eventOccurred;
                    MainWindowContent.Content = loginUI;
                    break;
                case "ChatRoom":
                    ChatRoom chatRoom = new ChatRoom();
                    chatRoom.userEvent += eventOccurred;
                    MainWindowContent.Content = chatRoom;
                    break;
                case "Login":
                    FriendsWindow friendsWindow = new FriendsWindow();
                    this.Close();
                    break;
            }
        }
    }
}

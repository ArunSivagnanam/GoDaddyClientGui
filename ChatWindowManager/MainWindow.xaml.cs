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
using GoDaddyClient;

namespace ChatWindowManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GoDaddyClient.Client clientLogic;

        public MainWindow()
        {
            InitializeComponent();
            UserLogIn loginUI = new UserLogIn();
            MainWindowContent.Content = loginUI;
            loginUI.userEvent += eventOccurred;
            clientLogic = new GoDaddyClient.Client();   
        }

        void eventOccurred(object sender, src.UIEvent e)
        {
            switch(e.window)
            {
                case "CreateNewUser":
                    CreateNewUser createNewUserUI = new CreateNewUser(clientLogic);
                    createNewUserUI.createUserEvent += eventOccurred;
                    MainWindowContent.Content = createNewUserUI; 
                    break;
                case "UserLogIn":
                    UserLogIn loginUI = new UserLogIn();
                    loginUI.userEvent += eventOccurred;
                    MainWindowContent.Content = loginUI;
                    break;
                case "Login":
                    FriendsWindow friendsWindow = new FriendsWindow(clientLogic);
                    this.Close();
                    break;
            }
        }
    }
}

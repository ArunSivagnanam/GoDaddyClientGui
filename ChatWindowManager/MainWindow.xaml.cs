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
using System.Diagnostics;

namespace ChatWindowManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xamlfg
    /// </summary>
    public partial class MainWindow : Window
    {
        GoDaddyClient.Client clientLogic;

        public MainWindow()
        {
            InitializeComponent();
            clientLogic = new GoDaddyClient.Client();
            UserLogIn loginUI = new UserLogIn(clientLogic);
            MainWindowContent.Content = loginUI;
            loginUI.userEvent += eventOccurred;
            
            
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
                    UserLogIn loginUI = new UserLogIn(clientLogic);
                    loginUI.userEvent += eventOccurred;
                    MainWindowContent.Content = loginUI;
                    break;
                case "Login":
                    Debug.Write("Hej");
                    FriendsWindow friendsWindow = new FriendsWindow(clientLogic);
                    this.Close();
                    break;
            }
        }
    }
}

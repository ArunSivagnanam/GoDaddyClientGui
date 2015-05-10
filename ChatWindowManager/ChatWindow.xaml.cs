using ChatWindowManager.UserControls;
using GoDaddyClient.ServiceReference;
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
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private GoDaddyClient.Client clientLogic;
        Dictionary<string, ChatWindow> windows;
        string receiverUser;
        public ChatWindowUS chatWindowsUS;

        public ChatWindow(GoDaddyClient.Client clientLogic, string receiverUser, Dictionary<string, ChatWindow> windows)
        {
            InitializeComponent();
            chatWindowsUS = new UserControls.ChatWindowUS(clientLogic, receiverUser);
            ChatWindowContent.Content = chatWindowsUS;
            this.clientLogic = clientLogic;
            this.receiverUser = receiverUser;
            this.windows = windows;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            windows.Remove(receiverUser);
        }

        
    }
}

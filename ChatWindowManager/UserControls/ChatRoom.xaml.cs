using System;
using System.Collections;
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
    /// Interaction logic for ChatRoom.xaml
    /// </summary>
    public partial class ChatRoom : UserControl
    {
        public event EventHandler<src.UIEvent> userEvent;
        private Dictionary<string, TextBox> chatBoxes = new Dictionary<string, TextBox>();
        private TextBox activeChatTexBox;


        private TextBox activeChat
        {
            get { return activeChatTexBox; }
            set { activeChatTexBox = value; }
        }

        public ChatRoom()
        {
            InitializeComponent();
            chatBox.Text = "<<Welcome>>";
            chatBox.Text += DateTime.Now.ToString("HH:mm:ss");
            chatBox.Text += Environment.NewLine;
            //chatBoxes.Add(UserLogIn.loggedInClient.UserName, activeChatTexBox);
            activeChatTexBox = chatBox;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler<src.UIEvent> handler = userEvent;
            if (userEvent != null)
            {
                chatBoxes.Clear();
                handler(this, new src.UIEvent("UserLogIn"));
            }
        }

        private void sendTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string time = DateTime.Now.ToString("HH:mm:");
                TextBox tb = (TextBox)sender;
                string textToSend = tb.Text;
                tb.Clear();
                activeChatTexBox.Text += "<" + time + ">" + textToSend;
                activeChatTexBox.Text += Environment.NewLine;
                e.Handled = true;
            }
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm:");
            TextBox tb = sendTextBox;
            string textToSend = tb.Text;
            tb.Clear();
            activeChatTexBox.Text += "<" + time + ">" + textToSend;
            activeChatTexBox.Text += Environment.NewLine;
            e.Handled = true;
        }

    }
}

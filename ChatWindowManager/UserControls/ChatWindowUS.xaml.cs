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
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindowUS : UserControl
    {
        public event EventHandler<src.UIEvent> userEvent;

        public ChatWindowUS()
        {
            InitializeComponent();
            chatBox.Text = "<<Welcome>>";
            chatBox.AppendText(DateTime.Now.ToString("HH:mm:ss"));
            chatBox.AppendText(Environment.NewLine);

        }

        private void sendTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string time = DateTime.Now.ToString("HH:mm:");
                TextBox tb = (TextBox)sender;
                string textToSend = tb.Text;
                tb.Clear();
                chatBox.AppendText("<" + time + ">" + textToSend);
                chatBox.AppendText(Environment.NewLine);
                e.Handled = true;
            }
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = sendTextBox;
            string time = DateTime.Now.ToString("HH:mm:");
            string textToSend = tb.Text;
            tb.Clear();
            chatBox.Text += "<" + time + ">" + textToSend;
            chatBox.Text += Environment.NewLine;
            e.Handled = true;
        }

        private void chatBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.SelectionStart = tb.Text.Length;
            tb.ScrollToEnd();
        }

    }
}

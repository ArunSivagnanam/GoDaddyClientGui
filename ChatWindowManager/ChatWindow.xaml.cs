﻿using GoDaddyClient.ServiceReference;
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
        

        public ChatWindow(GoDaddyClient.Client clientLogic, User receiverUser)
        {
            InitializeComponent();
            UserControls.ChatWindowUS chatWindowsUS = new UserControls.ChatWindowUS(clientLogic, receiverUser);
            ChatWindowContent.Content = chatWindowsUS;
            this.clientLogic = clientLogic;
          

        }

        
    }
}

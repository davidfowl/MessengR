using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MessengR.Client.Hubs;
using MessengR.Client.Model;
using MessengR.Client.View;
using MessengR.Client.ViewModel;
using MessengR.Models;
using SignalR.Client.Hubs;

namespace MessengR.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //private HubConnection _connection;
        //private Chat _chat;

        //private readonly SynchronizationContext _syncContext;

        public MainWindow()
        {
            InitializeComponent();

            // Store the sync context from the ui thread so we can post to it
            //_syncContext = SynchronizationContext.Current;
        }

        //private void InitializeConnection(string url, Cookie authCookie)
        //{
        //    _connection = new HubConnection(url);
        //    _connection.CookieContainer = new CookieContainer();
        //    _connection.CookieContainer.Add(authCookie);

        //    // Get a reference to the chat proxy
        //    _chat = new Chat(_connection);

        //    // Start the connection
        //    _connection.Start().ContinueWith(task =>
        //    {
        //        if (task.IsFaulted)
        //        {
        //            // Show a connection error here
        //        }
        //        else
        //        {
        //            // Get a list of users and add it to the view model
        //        }
        //    });

        //    // Fire events on the ui thread
        //    _chat.UserOffline += user => _syncContext.Post(_ => OnUserOffline(user), null);
        //    _chat.UserOnline += user => _syncContext.Post(_ => OnUserOnline(user), null);
        //    _chat.Message += message => _syncContext.Post(_ => OnMessage(message), null);
        //}

        //private void OnMessage(Message message)
        //{
        //    // Starts a new conversation with message.From if not started,
        //    // otherwise, it will add a message to the conversation window with message.From.
        //}

        //private void OnUserOnline(User user)
        //{
        //    // Mark user as online
        //}

        //private void OnUserOffline(User user)
        //{
        //    // Mark user as offline
        //}
    }
}

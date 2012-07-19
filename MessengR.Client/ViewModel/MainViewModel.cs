using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;
using MessengR.Client.Hubs;
using MessengR.Client.Model;
using MessengR.Models;
using Microsoft.Practices.Prism.Commands;
using SignalR.Client.Hubs;

namespace MessengR.Client.ViewModel
{
    public class MainViewModel
    {
        private HubConnection _connection;
        private Chat _chat;

        private readonly SynchronizationContext _syncContext;

        public UserModel User { get; set; }


        public MainViewModel() { }

        public MainViewModel(UserModel user)
        {
            User = user;

            // Store the sync context from the ui thread so we can post to it
            _syncContext = SynchronizationContext.Current;
            InitializeConnection(ConfigurationManager.AppSettings["HostURL"], user.Authentication.AuthCookie);
        }

        private void InitializeConnection(string url, Cookie authCookie)
        {
            _connection = new HubConnection(url);
            _connection.CookieContainer = new CookieContainer();
            _connection.CookieContainer.Add(authCookie);

            // Get a reference to the chat proxy
            _chat = new Chat(_connection);
            
            // Start the connection
            _connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Show a connection error here
                }
                else
                {
                    // Get a list of users and add it to the view model
                    _chat.GetUsers().ContinueWith(getUserTask =>
                    {
                        if (getUserTask.IsFaulted)
                        {
                            // Show a connection error here
                        }
                        else
                        {
                            // Exclude current user from Contact list
                            User.Contacts = new ObservableCollection<UserModel>(
                                getUserTask.Result.Where(u => !u.Name.Equals(User.Name, StringComparison.OrdinalIgnoreCase)).Select(u => new UserModel(u))
                            );
                        }
                    });
                }
            });

            // Fire events on the ui thread
            _chat.UserOffline += user => _syncContext.Post(_ => OnUserOffline(user), null);
            _chat.UserOnline += user => _syncContext.Post(_ => OnUserOnline(user), null);
            _chat.Message += message => _syncContext.Post(_ => OnMessage(message), null);
        }

        private void OnMessage(Message message)
        {
            // Starts a new conversation with message.From if not started,
            // otherwise, it will add a message to the conversation window with message.From.
        }

        private void OnUserOnline(User user)
        {
            // Mark user as online
            User.UpdateUserStatus(user);
        }

        private void OnUserOffline(User user)
        {
            // Mark user as offline
            User.UpdateUserStatus(user);
        }

    }
}

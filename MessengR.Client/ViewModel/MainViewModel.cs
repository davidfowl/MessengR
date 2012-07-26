using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Input;
using MessengR.Client.Common;
using MessengR.Client.Hubs;
using MessengR.Models;
using Microsoft.Practices.Prism.Events;
using SignalR.Client.Hubs;

namespace MessengR.Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region SignalR Objects
        private HubConnection _connection;
        private Chat _chat;
        private readonly SynchronizationContext _syncContext;
        #endregion

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private ContactViewModel _me;
        public ContactViewModel Me
        {
            get { return _me; }
            set
            {
                _me = value;
                OnPropertyChanged("Me");
            }
        }
        private ObservableCollection<ContactViewModel> _contacts;
        public ObservableCollection<ContactViewModel> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        private ObservableCollection<Message> _conversations;
        public ObservableCollection<Message> Conversations
        {
            get { return _conversations; }
            set
            {
                _conversations = value;
                OnPropertyChanged("Conversations");
            }
        }
        private readonly ChatSessionsViewModel _chatSessions = new ChatSessionsViewModel();

        public MainViewModel() { }

        public MainViewModel(string name, Cookie authCookie)
        {
            Name = name;
            _chatSessions.SendMessage += OnSendMessage;
            // Store the sync context from the ui thread so we can post to it
            _syncContext = SynchronizationContext.Current;
            InitializeConnection(ConfigurationManager.AppSettings["HostUrl"], authCookie);

        }

        private void InitializeConnection(string url, Cookie authCookie)
        {
            _connection = new HubConnection(url) { CookieContainer = new CookieContainer() };
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
                            Me = new ContactViewModel(getUserTask.Result.Single(u => u.Name == Name));
                            Me.OpenChatEvent += OnOpenChat;
                            //Me.SendMessageEvent +=

                            // Exclude current user from Contact list
                            Contacts = new ObservableCollection<ContactViewModel>(
                                getUserTask.Result.Where(u => !u.Name.Equals(Me.User.Name, StringComparison.OrdinalIgnoreCase)).Select(u => new ContactViewModel(u, Me))
                            );
                        }
                    });
                }
            });

            // Fire events on the ui thread
            _chat.UserOffline += user => _syncContext.Post(_ => OnUserStatusChange(user), null);
            _chat.UserOnline += user => _syncContext.Post(_ => OnUserStatusChange(user), null);
            _chat.Message += message => _syncContext.Post(_ => OnMessage(message), null);
        }

        private void OnMessage(Message message)
        {
            // Starts a new conversation with message.From if not started,
            // otherwise, it will add a message to the conversation window with message.From.
            _chatSessions.AddMessage(message, Me.User);
        }

        private void OnUserStatusChange(User user)
        {
            //Mark user as online/offline
            if (Contacts != null)
            {
                var contact = Contacts.SingleOrDefault(u => u.User.Name == user.Name);
                if (contact != null)
                {
                    Contacts.Remove(contact);
                    Contacts.Add(new ContactViewModel(user, Me));
                }
            }
        }

        public void OnOpenChat(object sender, EventArgs e)
        {
            if (sender is ContactViewModel)
            {
                _chatSessions.StartNewSession((sender as ContactViewModel).User, Me.User);
            }
        }

        void OnSendMessage(object sender, EventArgs e)
        {
            if (sender is ChatSessionViewModel)
            {
                var chat = sender as ChatSessionViewModel;
                _chat.SendMessage(chat.User.Name, chat.Message).ContinueWith(sendMessage =>
                {
                    if (sendMessage.IsFaulted)
                    {
                        //display error
                    }
                    else
                    {
                    }
                });
            }
        }
    }
}

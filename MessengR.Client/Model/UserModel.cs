using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using MessengR.Client.View;
using MessengR.Models;
using Microsoft.Practices.Prism.Commands;

namespace MessengR.Client.Model
{
    public class UserModel : INotifyPropertyChanged
    {
        private readonly User _user = new User();
        public string Name { get { return _user.Name; } set { _user.Name = value; } }
        public string Email { get { return _user.Email; } set { _user.Email = value; } }
        public bool Online
        {
            get { return _user.Online; }
            set
            {
                _user.Online = value;
                Status = GetUserStatus();
                OnPropertyChanged("Online");
            }
        }

        public string Username
        {
            get { return _user.Name; }
            set
            {
                if (_user.Name != value)
                {
                    _user.Name = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public UserModel() { }
        public UserModel(User user)
        {
            _user = user;
            _status = GetUserStatus();
        }

        private string GetUserStatus()
        {
            return _user.Online ? "Online" : "Offline";
        }

        private DelegateCommand<UserModel> _openChat;
        public ICommand OpenChat
        {
            get
            {
                if (_openChat == null)
                {
                    _openChat = new DelegateCommand<UserModel>(OnOpenChatExecuted, CanOpenChatExecute);
                }
                return _openChat;
            }
        }

        private void OnOpenChatExecuted(UserModel user)
        {
            var chatView = new ChatView();
            chatView.DataContext = user;
            chatView.Show();
        }

        private bool CanOpenChatExecute(UserModel user)
        {
            return user.Online;
        }
        private AuthenticationResult _authenticationResult;
        public AuthenticationResult Authentication
        {
            get { return _authenticationResult; }
            set
            {
                _authenticationResult = value;
                OnPropertyChanged("Authentication");
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

        private ObservableCollection<UserModel> _contacts;
        public ObservableCollection<UserModel> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        public void UpdateUserStatus(User user)
        {
            if (user != null)
            {
                if (Contacts != null && Contacts.Single(u => u.Name == user.Name) != null)
                {
                    var userModel = Contacts.Single(u => u.Name == user.Name);
                    Contacts.Remove(userModel);
                    Contacts.Add(new UserModel(user));
                }
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}

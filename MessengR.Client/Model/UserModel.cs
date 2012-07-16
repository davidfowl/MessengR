using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using MessengR.Models;

namespace MessengR.Client.Model
{
    public class UserModel : User, INotifyPropertyChanged
    {
        public string Username
        {
            get { return base.Name; }
            set
            {
                if (Name != value)
                {
                    Name = value;
                    OnPropertyChanged("Username");
                }
            }
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

        private ObservableCollection<User> _contacts;
        public ObservableCollection<User> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
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

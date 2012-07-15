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
        public ICollectionView Conversations { get; set; }
        public ICollectionView Contacts { get; set; }

        public UserModel()
        { }

        public UserModel(ObservableCollection<Message> conversations, ObservableCollection<User> contacts)
        {
            Conversations = new ListCollectionView(conversations);
            //add handlers for when conversations change

            Contacts = new ListCollectionView(contacts);
            //add handlers for when contacts change
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

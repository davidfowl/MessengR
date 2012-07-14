using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MessengR.Client.Model
{
    class UserModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private AuthenticationResult _result;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                if (PropertyChanged != null)
                {
                    OnPropertyChanged("Username");
                }
            }
        }

        public string Password { get; set; }

        public AuthenticationResult AuthenticationResult
        {
            get { return _result; }
            set
            {
                _result = value;
                if (PropertyChanged != null)
                {
                    OnPropertyChanged("AuthenticationResult");
                }
            }
        }

        #region INotifyPropertyChanged Members
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

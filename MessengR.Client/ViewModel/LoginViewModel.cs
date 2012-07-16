using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MessengR.Client.Model;
using MessengR.Models;
using Microsoft.Practices.Prism.Commands;

namespace MessengR.Client.ViewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public UserModel User { get; set; }
        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set
            {
                if (_dialogResult != value)
                {
                    _dialogResult = value;
                    OnPropertyChanged("DialogResult");
                }
            }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                if (_error != value)
                {
                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }
        public LoginViewModel()
        {
            User = new UserModel() { Email = string.Empty, Name = string.Empty, Online = false, Username = string.Empty };
        }

        public event EventHandler<LoginEventArgs> LoginSuccessful;

        private DelegateCommand<object> _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new DelegateCommand<object>(this.OnLoginExecuted, this.CanLoginExecute);
                }
                return _loginCommand;
            }
        }

        private void OnLoginExecuted(object arg)
        {
            //PasswordBox passed through as it is not bindable
            var password = arg as PasswordBox;
            if (User != null && password != null)
            {
                var login = LoginHelper.LoginAsync(ConfigurationManager.AppSettings["HostURL"], User.Name,
                                                   password.Password);
                login.Wait();

                var authResult = login.Result;
                User.Authentication = authResult;
                if (authResult.StatusCode == HttpStatusCode.OK)
                {
                    Error = string.Empty;
                    var main = new MainWindow { DataContext = new MainViewModel { User = User } };
                    main.Show();
                    DialogResult = true;
                }
                else
                {
                    Error = authResult.Error;
                }
            }
        }

        private bool CanLoginExecute(object arg)
        {
            //PasswordBox passed through as it is not bindable
            var password = arg as PasswordBox;
            return (password == null) ||
                   (!String.IsNullOrEmpty(User.Name) && !String.IsNullOrEmpty(password.Password));
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

    public class LoginEventArgs : EventArgs
    {
        public static readonly LoginEventArgs Empty;

        public AuthenticationResult Authentication { get; set; }

        public bool HasError { get { return !String.IsNullOrEmpty(Error); } }
        public string Error { get; set; }
    }
}

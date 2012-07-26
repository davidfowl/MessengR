using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;

using MessengR.Client.Common;

namespace MessengR.Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public event EventHandler LoginSuccess;

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

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
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

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new CommandBase(i => Login(), i => CanLoginExecute());
                }
                return _loginCommand;
            }
        }

        private void Login()
        {
            var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            LoginHelper.LoginAsync(HostUrl, Name, Password).ContinueWith(task =>
                {
                    AuthenticationResult authResult = task.Result;
                    if (authResult.StatusCode == HttpStatusCode.OK)
                    {
                        Error = String.Empty;

                        var viewModel = new MainViewModel(Name, authResult.AuthCookie);
                        var main = new MainWindow() { DataContext = viewModel };
                        main.Show();
                        LoginSuccess(this, null);
                    }
                    else
                    {
                        Error = authResult.Error;
                    }
                }, uiScheduler);
        }

        private bool CanLoginExecute()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Password);
        }
    }
}

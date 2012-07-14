using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MessengR.Client.Model;

namespace MessengR.Client.ViewModel
{
    internal class LoginViewModel
    {
        public MetroWindow View { get; set; }
        public UserModel User { get; set; }

        public ICommand LoginCommand()
        {
            
        }

        public void Login(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if(passwordBox == null) throw new Exception();

            var authResult = LoginHelper.Login(ConfigurationManager.AppSettings["HostURL"], User.Username,
                                               passwordBox.Password);
            if (authResult.StatusCode == HttpStatusCode.OK)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                //this.Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(authResult.Message))
                {
                    User.AuthenticationResult = authResult;
                }
            }
        }
    }
}

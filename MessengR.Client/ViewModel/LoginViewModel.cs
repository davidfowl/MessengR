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
using MessengR.Models;

namespace MessengR.Client.ViewModel
{
    internal class LoginViewModel
    {
        public UserModel User { get; set; }

        //public ICommand LoginCommand()
        //{
        //}

        public void Login(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if(passwordBox == null) throw new Exception();

            var authResult = LoginHelper.Login(ConfigurationManager.AppSettings["HostURL"], User.Name,
                                               passwordBox.Password);
            if (authResult.StatusCode == HttpStatusCode.OK)
            {
                User.Authentication = authResult;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                //this.Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(authResult.Message))
                {
                    User.Authentication = authResult;
                }
            }
        }
    }
}

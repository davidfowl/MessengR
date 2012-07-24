using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MessengR.Client.Interface;
using MessengR.Client.View;
using MessengR.Client.ViewModel;
using MessengR.Client.Service;

namespace MessengR.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper.Initialize();

            var loginViewModel = new LoginViewModel();
            var loginDialog = ServiceProvider.Instance.Get<ILoginDialog>();
            loginDialog.BindViewModel(loginViewModel);
            loginDialog.Show();

        }
    }
}

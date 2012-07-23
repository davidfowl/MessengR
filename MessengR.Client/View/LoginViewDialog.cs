using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengR.Client.Interface;
using MessengR.Client.ViewModel;

namespace MessengR.Client.View
{
    public class LoginViewDialog : ILoginDialog
    {
        private LoginView _view;

        private LoginView GetDialog()
        {
            if (_view == null)
            {
                _view = new LoginView();
                _view.Closed += ViewClosed;
            }
            return _view;
        }

        void ViewClosed(object sender, EventArgs e)
        {
            _view = null;
        }

        #region Implementation of IDialog

        public void BindViewModel<TViewModel>(TViewModel viewModel)
        {
            (viewModel as LoginViewModel).LoginSuccess += LoginViewDialog_LoginSuccess;
            GetDialog().DataContext = viewModel;
        }

        void LoginViewDialog_LoginSuccess(object sender, EventArgs e)
        {
            Close();
        }

        public void Show()
        {
            GetDialog().Show();
        }

        public void Close()
        {
            GetDialog().Close();
        }

        #endregion
    }
}

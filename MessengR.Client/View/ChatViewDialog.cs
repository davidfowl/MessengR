using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengR.Client.Interface;

namespace MessengR.Client.View
{
    public class ChatViewDialog : IChatDialog
    {
        private ChatView _view;

        private ChatView GetDialog()
        {
            if(_view == null)
            {
                _view = new ChatView();
                _view.Closed += ViewClosed;
            }
            return _view;
        }

        #region Implementation of IDialog

        public void BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetDialog().DataContext = viewModel;
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

        void ViewClosed(object sender, EventArgs e)
        {
            _view = null;
        }
    }
}

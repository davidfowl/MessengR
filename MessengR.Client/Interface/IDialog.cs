using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengR.Client.Interface
{
    public interface IDialog
    {
        void BindViewModel<TViewModel>(TViewModel viewModel);
        void Show();
        void Close();
    }
}

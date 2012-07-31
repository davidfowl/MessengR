using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengR.Client.Common;

namespace MessengR.Client.Interface
{
    public interface IChatDialog : IDialog
    {
        event EventHandler<ChatSessionEventArgs> ViewClosedEvent;
    }
}

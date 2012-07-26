using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengR.Models;

namespace MessengR.Client.Common
{
    public class OpenChatEventArgs : EventArgs
    {
        public User Contact { get; set; }

        public OpenChatEventArgs(User contact)
        {
            Contact = contact;
        }
    }
}

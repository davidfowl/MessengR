using System;

using MessengR.Models;

namespace MessengR.Client.Common
{
    public class ChatSessionEventArgs : EventArgs
    {
        public User Contact { get; set; }
        public string Message { get; set; }

        public ChatSessionEventArgs(User user, string message)
        {
            Contact = user;
            Message = message;
        }
    }
}

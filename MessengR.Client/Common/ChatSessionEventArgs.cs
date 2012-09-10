using System;

using MessengR.Models;

namespace MessengR.Client.Common
{
    public class ChatSessionEventArgs : EventArgs
    {
        public User Contact { get; set; }
        public Message Message { get; set; }

        public ChatSessionEventArgs(User user, Message message)
        {
            Contact = user;
            Message = message;
        }
    }
}

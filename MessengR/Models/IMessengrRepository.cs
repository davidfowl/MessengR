using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengR.Models
{
    public interface IMessengrRepository : IDisposable
    {
        IEnumerable<Message> GetConversations(User user);
        IEnumerable<Message> GetConversation(User user, User contact);
        void Add(Message message);
        void CommitChanges();
    }
}

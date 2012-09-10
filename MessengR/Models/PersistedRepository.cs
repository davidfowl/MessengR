using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessengR.Models
{
    public class PersistedRepository : IMessengrRepository
    {
        private readonly MessengrContext _db;

        private readonly Func<MessengrContext, User, IEnumerable<Message>> getConversations = (db, user) => db.Messages.Where(m => m.From == user.Name).OrderByDescending(msg => msg.DateReceived).GroupBy(c => c.To).Select(grp => grp.FirstOrDefault());
        private readonly Func<MessengrContext, User, User, IEnumerable<Message>> getConversation = (db, user, contact) => db.Messages.Where(m => (m.From == user.Name && m.To == contact.Name) || (m.From == contact.Name && m.To == user.Name)).OrderBy(msg => msg.DateReceived);
        public PersistedRepository(MessengrContext db)
        {
            _db = db;
        }

        public IEnumerable<Message> GetConversations(User user)
        {
            return getConversations(_db, user);
        }

        public IEnumerable<Message> GetConversation(User user, User contact)
        {
            var result = getConversation(_db, user, contact).ToList();
            result.ForEach(msg => msg.IsMine = (msg.From == user.Name));
            result.ForEach(msg => msg.Initiator = (msg.From == user.Name) ? user : contact);
            result.ForEach(msg => msg.Contact = (msg.To == user.Name) ? user : contact);

            return result;
        }

        public void Add(Message message)
        {
            _db.Messages.Add(message);
            _db.SaveChanges();
        }

        public void CommitChanges()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
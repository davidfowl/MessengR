using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using MessengR.Models;
using Microsoft.AspNet.SignalR;

namespace MessengR
{
    [Authorize]
    public class Chat : Hub
    {
        // This will only work on a single server
        private static readonly ConcurrentDictionary<string, List<string>> _userConnections = new ConcurrentDictionary<string, List<string>>();

        private static readonly IMessengrRepository _db = new PersistedRepository(new MessengrContext());

        public void Send(string who, Message message)
        {
            List<string> connections;
            if (_userConnections.TryGetValue(who, out connections))
            {
                // Get a list of connections for the user we want to send a message to
                foreach (var id in connections)
                {
                    // Send a message to each user, and tell them who it came from
                    Clients.Client(id).addMessage(new Message
                    {
                        From = Context.User.Identity.Name,
                        Initiator = GetUser(Context.User.Identity.Name),
                        Value = message.Value
                    });
                }
            }
        }

        public static bool IsOnline(string user)
        {
            return _userConnections.ContainsKey(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return from MembershipUser u in Membership.GetAllUsers()
                   select GetUser(u);
        }

        public IEnumerable<Message> GetConversations(User user)
        {
            var conversations = _db.GetConversations(user).ToList();
            conversations.ForEach(m => m.Contact = GetUser(m.To));
            return conversations;
        }

        public IEnumerable<Message> GetConversation(User user, User contact)
        {
            return _db.GetConversation(user, contact);
        }

        public void SaveMessage(Message message)
        {
            _db.Add(message);
            _db.CommitChanges();
        }

        public User GetUser(string userName)
        {
            return GetUser(Membership.GetUser(userName));
        }

        private User GetUser(MembershipUser u)
        {
            return new User
            {
                Online = IsOnline(u.UserName),
                Name = u.UserName,
                Email = u.Email
            };
        }

        public override Task OnConnected()
        {
            // Make a new slot of get a list of connections for this user name
            var connections = _userConnections.GetOrAdd(Context.User.Identity.Name, _ => new List<string>());

            lock (connections)
            {
                // Add the connection to the list of connections. This allows one user to log in from
                // different clients (broser tabs, browsers, mobile devices, other apps, etc),
                // and have them all be synchronized.
                connections.Add(Context.ConnectionId);
            }

            // Tell everyone this user came online
            return Clients.All.markOnline(GetUser(Context.User.Identity.Name));
        }

        public Task Disconnect(string userName, string connectionId)
        {
            List<string> connections;
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(connectionId))
            {
                if (_userConnections.TryGetValue(userName, out connections))
                {
                    lock (connections)
                    {
                        connections.Remove(connectionId);
                    }
                    if (connections.Count == 0)
                    {
                        _userConnections.TryRemove(userName, out connections);
                        return Clients.All.markOffline(GetUser(userName));
                    }
                }
            }
            return null;
        }

        public override Task OnDisconnected()
        {
            List<string> connections;
            string userName = Context.User.Identity.Name;
            
            if (_userConnections.TryGetValue(userName, out connections))
            {
                lock (connections)
                {
                    // Remove the connection from the list of connections
                    connections.Remove(Context.ConnectionId);
                }

                // In case this is a browser refresh, wait a few milli seconds before removing all connections
                // This way, hitting f5 don't suddenly cause an offline/online flip
                Thread.Sleep(100);

                if (connections.Count == 0)
                {
                    _userConnections.TryRemove(userName, out connections);

                    // If this is the last connection, mark the user offline
                    return Clients.All.markOffline(GetUser(userName));
                }
            }

            return base.OnDisconnected();
        }
    }
}
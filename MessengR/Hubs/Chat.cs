using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using MessengR.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace MessengR
{
    [Authorize]
    public class Chat : Hub
    {
        // This will only work on a single server
        private static readonly ConcurrentDictionary<string, List<string>> _userConnections = new ConcurrentDictionary<string, List<string>>();

        public void Send(string who, string message)
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
                        From = GetUser(Context.User.Identity.Name),
                        Value = message
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

        public override Task OnDisconnected()
        {
            List<string> connections;
            
            // Since the other information may not be available when disconnect is fired,
            // we keep track of which user name a connection id is associated with
            if (_userConnections.TryGetValue(Context.User.Identity.Name, out connections))
            {
                lock (connections)
                {
                    // Remove the connection from the list of connections
                    connections.Remove(Context.ConnectionId);
                }

                if (connections.Count == 0)
                {
                    _userConnections.TryRemove(Context.User.Identity.Name, out connections);

                    // If this is the last connection, mark the user offline
                    return Clients.All.markOffline(GetUser(Context.User.Identity.Name));
                }
            }

            return null;
        }
    }
}
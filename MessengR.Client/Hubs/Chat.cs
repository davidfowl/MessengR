using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessengR.Models;
using SignalR.Client.Hubs;

namespace MessengR.Client.Hubs
{
    /// <summary>
    /// Strongly typed client side proxy for the server side Hub (Similar to add service reference).
    /// </summary>
    public class Chat
    {
        private readonly IHubProxy _chat;

        public event Action<User> UserOnline;

        public event Action<User> UserOffline;

        public event Action<Message> Message;

        public Chat(HubConnection connection)
        {
            _chat = connection.CreateProxy("Chat");

            _chat.On<User>("markOnline", user =>
            {
                if (UserOnline != null)
                {
                    UserOnline(user);
                }
            });

            _chat.On<User>("markOffline", user =>
            {
                if (UserOffline != null)
                {
                    UserOffline(user);
                }
            });

            _chat.On<Message>("addMessage", message =>
            {
                if (Message != null)
                {
                    Message(message);
                }
            });
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            return _chat.Invoke<IEnumerable<User>>("GetUsers");
        }

        public Task<User> GetUser(string userName)
        {
            return _chat.Invoke<User>("GetUser", userName);
        }

        public Task<IEnumerable<Message>> GetConversations(User user)
        {
            return _chat.Invoke<IEnumerable<Message>>("GetConversations", user);
        }

        public Task<IEnumerable<Message>> GetConversation(User user, User contact)
        {
            return _chat.Invoke<IEnumerable<Message>>("GetConversation", user, contact);
        }

        public void SaveMessage(Message message)
        {
            _chat.Invoke<Message>("SaveMessage", message);
        }

        public Task<Message> SendMessage(string userName, Message message)
        {
            return _chat.Invoke<Message>("Send", userName, message);
        }

        public Task Disconnect(string userName, string connectionId)
        {
            return _chat.Invoke("Disconnect", userName, connectionId);
        }
    }
}

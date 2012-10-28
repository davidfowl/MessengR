using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MessengR.Client.Common;
using MessengR.Client.Interface;
using MessengR.Client.Service;
using MessengR.Client.View;
using MessengR.Models;

namespace MessengR.Client.ViewModel
{
    public class ChatSessionsViewModel
    {
        public event EventHandler<ChatSessionEventArgs> SendMessage;
        private readonly Dictionary<string, ChatSessionViewModel> _chatSessions = new Dictionary<string, ChatSessionViewModel>();

        public ChatSessionViewModel StartNewSession(User contact, User initiator)
        {
            var viewModel = new ChatSessionViewModel(contact);
            viewModel.Initiator = initiator;
            viewModel.SendMessage += OnSendMessage;
            viewModel.ChatSessionClosed += OnChatSessionClosed;
            _chatSessions.Add(contact.Name, viewModel);
            return viewModel;
        }

        public void LoadConversation(User contact, IEnumerable<Message> conversation)
        {
            ChatSessionViewModel chatSession;
            if(_chatSessions.TryGetValue(contact.Name, out chatSession))
            {
                var union = conversation.Union(chatSession.Conversation);
                chatSession.Conversation = new ObservableCollection<Message>(union);
            }
        }

        public void AddMessage(Message message, User initiator)
        {
            ChatSessionViewModel chatSession;
            if (!_chatSessions.TryGetValue(message.From, out chatSession))
            {
                chatSession = StartNewSession(message.Initiator, initiator);
                chatSession.OpenChat();
            }
            chatSession.MessageReceived(message);
        }

        public void CloseSessions(string user)
        {
            var chatSessions = _chatSessions.Values.Where(cs => cs.Initiator.Name == user);
            foreach(var chatSession in chatSessions.ToList())
            {
                chatSession.CloseChat();
            }
        }

        private void OnSendMessage(object sender, ChatSessionEventArgs e)
        {
            if (e != null)
            {
                SendMessage(sender, e);
            }
        }

        private void OnChatSessionClosed(object sender, ChatSessionEventArgs e)
        {
            if(e != null && e.Contact != null)
            {
                _chatSessions.Remove(e.Contact.Name);
            }
        }
    }
}

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
        private readonly ObservableCollection<ChatSessionViewModel> _chatSessions = new ObservableCollection<ChatSessionViewModel>();

        public void StartNewSession(User contact, User initiator)
        {
            var viewModel = new ChatSessionViewModel(contact);
            viewModel.Initiator = initiator;
            viewModel.SendMessage += OnSendMessage;
            var chatView = ServiceProvider.Instance.Get<IChatDialog>();
            chatView.BindViewModel(viewModel);
            chatView.Show();
            _chatSessions.Add(viewModel);
        }

        public void AddMessage(Message message, User initiator)
        {
            if (_chatSessions.SingleOrDefault(c => c.Contact.Name == message.From.Name) == null)
            {
                StartNewSession(message.From, initiator);
                _chatSessions.SingleOrDefault(c => c.Contact.Name == message.From.Name).MessageReceived(message);
            }
            else
            {
                _chatSessions.SingleOrDefault(c => c.Contact.Name == message.From.Name).MessageReceived(message);
            }
        }

        private void OnSendMessage(object sender, ChatSessionEventArgs e)
        {
            if (e != null)
            {
                SendMessage(sender, e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MessengR.Client.View;
using MessengR.Models;

namespace MessengR.Client.ViewModel
{
    public class ChatSessionsViewModel
    {
        public event EventHandler SendMessage;
        private readonly ObservableCollection<ChatSessionViewModel> _chatSessions = new ObservableCollection<ChatSessionViewModel>();

        public void StartNewSession(User user, User initiator)
        {
            var viewModel = new ChatSessionViewModel(user);
            viewModel.Initiator = initiator;
            viewModel.SendMessage += OnSendMessage;
            var chatView = new ChatViewDialog();
            chatView.BindViewModel(viewModel);
            chatView.Show();
            _chatSessions.Add(viewModel);
        }

        public void AddMessage(Message message, User initiator)
        {
            if (_chatSessions.SingleOrDefault(c => c.User.Name == message.From.Name) == null)
            {
                StartNewSession(message.From, initiator);
                _chatSessions.SingleOrDefault(c => c.User.Name == message.From.Name).MessageReceived(message);
            }
            else
            {
                _chatSessions.SingleOrDefault(c => c.User.Name == message.From.Name).MessageReceived(message);
            }
        }

        private void OnSendMessage(object sender, EventArgs e)
        {
            if(sender is ChatSessionViewModel)
            {
                SendMessage(sender, null);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MessengR.Client.Common;
using MessengR.Client.Hubs;
using MessengR.Client.Interface;
using MessengR.Client.Service;
using MessengR.Models;

namespace MessengR.Client.ViewModel
{
    public class ChatSessionViewModel : ViewModelBase
    {
        public event EventHandler<ChatSessionEventArgs> SendMessage;
        public event EventHandler<ChatSessionEventArgs> ChatSessionClosed;

        public User Initiator { get; set; }
        public User Contact { get; set; }
        public ObservableCollection<Message> Conversation { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public ChatSessionViewModel()
        {
            Conversation = new ObservableCollection<Message>();
        }

        public ChatSessionViewModel(User user)
        {
            Contact = user;
            Conversation = new ObservableCollection<Message>();
        }

        public void OpenChat()
        {
            var chatView = ServiceProvider.Instance.Get<IChatDialog>();
            chatView.BindViewModel(this);
            chatView.ViewClosedEvent += OnViewClosed;
            chatView.Show();
        }

        public void MessageReceived(Message message)
        {
            message.DateReceived = DateTime.Now;
            Conversation.Add(message);
        }

        private ICommand _sendMessageCommand;
        public ICommand SendMessageCommand
        {
            get
            {
                if (_sendMessageCommand == null)
                {
                    _sendMessageCommand = new CommandBase(m => Send(), m => CanSend());
                }
                return _sendMessageCommand;
            }
        }

        public void Send()
        {
            SendMessage(this, new ChatSessionEventArgs(this.Contact, this.Message));
            var message = new Message { From = Initiator, Value = Message, IsMine = true, DateReceived = DateTime.Now };
            Conversation.Add(message);
            Message = String.Empty;
        }

        public bool CanSend()
        {
            return !String.IsNullOrEmpty(Message);
        }

        public void OnViewClosed(object sender, EventArgs e)
        {
            if (ChatSessionClosed != null)
            {
                ChatSessionClosed(this, new ChatSessionEventArgs(Contact, String.Empty));
            }
        }
    }
}

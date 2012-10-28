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

        private IChatDialog _view;

        public User Initiator { get; set; }
        public User Contact { get; set; }
        private ObservableCollection<Message> _conversation;
        public ObservableCollection<Message> Conversation 
        {
            get { return _conversation; }
            set
            {
                _conversation = value;
                OnPropertyChanged("Conversation");
            }
        }

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
            _view = chatView;
        }

        public void CloseChat()
        {
            if (_view != null)
            {
                _view.Close();
                if (ChatSessionClosed != null)
                    ChatSessionClosed(this, new ChatSessionEventArgs(Contact, null));
                _view = null;
            }
        }

        public void AddMessage(Message message)
        {
            Conversation.Add(message);
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
            var message = new Message { From = Initiator.Name, Initiator = Initiator, To = Contact.Name, Contact = Contact, Value = Message, IsMine = true, DateReceived = DateTime.Now };
            SendMessage(this, new ChatSessionEventArgs(Contact, message));
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
                ChatSessionClosed(this, new ChatSessionEventArgs(Contact, null));
            }
        }
    }
}

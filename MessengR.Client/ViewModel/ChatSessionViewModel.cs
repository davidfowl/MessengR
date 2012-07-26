using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MessengR.Client.Common;
using MessengR.Client.Hubs;
using MessengR.Models;

namespace MessengR.Client.ViewModel
{
    public class ChatSessionViewModel : ViewModelBase
    {
        public event EventHandler SendMessage;

        public User Initiator { get; set; }
        public User User { get; set; }
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
            User = user;
            Conversation = new ObservableCollection<Message>();
        }


        public void MessageReceived(Message message)
        {
            Conversation.Add(message);
        }

        #region Current code that could be useful
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
            var message = new Message { From = Initiator, Value = Message };
            SendMessage(this, null);
            Conversation.Add(message);
            Message = String.Empty;
        }

        public bool CanSend()
        {
            return !String.IsNullOrEmpty(Message);
        }
        #endregion
    }
}

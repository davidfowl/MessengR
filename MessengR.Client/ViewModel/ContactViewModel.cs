using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows.Input;

using MessengR.Client.Common;
using MessengR.Client.Hubs;
using MessengR.Client.Interface;
using MessengR.Client.Service;
using MessengR.Models;

namespace MessengR.Client.ViewModel
{
    public class ContactViewModel : ViewModelBase
    {
        public event EventHandler OpenChatEvent;

        private readonly ContactViewModel _parent;

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        private bool _isOnline;
        public bool IsOnline
        {
            get { return _isOnline; }
            set
            {
                if (_isOnline != value)
                {
                    _isOnline = value;
                    Status = _isOnline ? "Online" : "Offline";
                    OnPropertyChanged("Online");
                }
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public ContactViewModel()
        {
        }

        public ContactViewModel(User user)
        {
            User = user;
            IsOnline = user.Online;
            Status = user.Online ? "Online" : "Offline";
        }

        public ContactViewModel(User user, ContactViewModel parent)
        {
            User = user;
            IsOnline = user.Online;
            Status = user.Online ? "Online" : "Offline";
            _parent = parent;
        }


        private ICommand _chatCommand;
        public ICommand ChatCommand
        {
            get
            {
                if (_chatCommand == null)
                {
                    _chatCommand = new CommandBase(c => OpenChat(), c => CanOpenChat());
                }
                return _chatCommand;
            }
        }

        private void OpenChat()
        {
            if(_parent.OpenChatEvent != null)
            {
                _parent.OpenChatEvent(this, null);
            }
        }

        private bool CanOpenChat()
        {
            return IsOnline;
        }
    }
}

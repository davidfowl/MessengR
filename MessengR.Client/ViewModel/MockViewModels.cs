using MessengR.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MessengR.Client.ViewModel
{
    public class MockChatViewModel
    {
        public User Contact { get; set; }
        public ObservableCollection<Message> Conversation { get; set; }
        public MockChatViewModel()
        {
            Contact = new User { Email = "user1@user.com", Name = "user1", Online = true };
            Conversation = new ObservableCollection<Message>();
            Conversation.Add(new Message { Id = -1, DateReceived = DateTime.Now.AddMinutes(-2), Value = "Test Message 1", From = "user", To = "user1", IsMine = true, Initiator = new User { Email = "user@user.com", Name = "user", Online = true }, Contact = new User { Email = "user1@user.com", Name = "user1", Online = true } });
            Conversation.Add(new Message { Id = -2, DateReceived = DateTime.Now.AddMinutes(-2), Value = "Test Message 2", From = "user1", To = "user", IsMine = false, Contact = new User { Email = "user@user.com", Name = "user", Online = true }, Initiator = new User { Email = "user1@user.com", Name = "user1", Online = true } });
            Conversation.Add(new Message { Id = -3, DateReceived = DateTime.Now.AddMinutes(-2), Value = "Test Message 3", From = "user", To = "user1", IsMine = true, Initiator = new User { Email = "user@user.com", Name = "user", Online = true }, Contact = new User { Email = "user1@user.com", Name = "user1", Online = true } });
        }
    }

    public class MockMainViewModel
    {
        public ContactViewModel Me { get; set; }
        public ObservableCollection<ContactViewModel> Conversations { get; set; }
        public ObservableCollection<ContactViewModel> Contacts { get; set; }
        public MockMainViewModel()
        {
            Me = new ContactViewModel { User = new User { Email = "user@user.com", Name = "user", Online = true }, IsOnline = true, Status = "Online" };
            Conversations = new ObservableCollection<ContactViewModel>();
            Conversations.Add(new ContactViewModel { User = new User { Email = "user1@user.com", Name = "user1", Online = true }, IsOnline = true, Status = "Online", Message = new Message { Id = -1, DateReceived = DateTime.Now.AddMinutes(-2), Value = "and the text box for your message needs to wrap not continue on", From = "user", To = "user1", IsMine = true, Initiator = new User { Email = "user@user.com", Name = "user", Online = true }, Contact = new User { Email = "user1@user.com", Name = "user1", Online = true } } });
            Conversations.Add(new ContactViewModel { User = new User { Email = "user2@user.com", Name = "user2", Online = false }, IsOnline = false, Status = "Offline", Message = new Message { Id = -1, DateReceived = DateTime.Now.AddMinutes(-2), Value = "Test Message 2", From = "user", To = "user2", IsMine = true, Initiator = new User { Email = "user@user.com", Name = "user", Online = true }, Contact = new User { Email = "user2@user.com", Name = "user2", Online = false } } });
            Conversations.Add(new ContactViewModel { User = new User { Email = "user3@user.com", Name = "user3", Online = true }, IsOnline = true, Status = "Online", Message = new Message { Id = -1, DateReceived = DateTime.Now.AddMinutes(-2), Value = "Test Message 3", From = "user", To = "user3", IsMine = true, Initiator = new User { Email = "user@user.com", Name = "user", Online = true }, Contact = new User { Email = "user3@user.com", Name = "user3", Online = true } } });
            Conversations.Add(new ContactViewModel { User = new User { Email = "user4@user.com", Name = "user4", Online = false }, IsOnline = false, Status = "Offline", Message = new Message { Id = -1, DateReceived = DateTime.Now.AddMinutes(-2), Value = "Test Message 4", From = "user", To = "user4", IsMine = true, Initiator = new User { Email = "user@user.com", Name = "user", Online = true }, Contact = new User { Email = "user4@user.com", Name = "user4", Online = false } } });
            Contacts = new ObservableCollection<ContactViewModel>();
            Contacts.Add(new ContactViewModel { User = new User { Email = "user1@user.com", Name = "user1", Online = true }, IsOnline = true, Status = "Online" });
            Contacts.Add(new ContactViewModel { User = new User { Email = "user2@user.com", Name = "user2", Online = false }, IsOnline = false, Status = "Offline" });
            Contacts.Add(new ContactViewModel { User = new User { Email = "user3@user.com", Name = "user3", Online = true }, IsOnline = true, Status = "Online" });
            Contacts.Add(new ContactViewModel { User = new User { Email = "user4@user.com", Name = "user4", Online = false }, IsOnline = false, Status = "Offline" });
        }
    }
}

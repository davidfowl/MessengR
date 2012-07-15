using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using MessengR.Models;

namespace MessengR.Client.Model
{
    public class UserModel : UserViewModel
    {
        public AuthenticationResult Authentication { get; set; }
        public ICollectionView Conversations { get; set; }
        public ICollectionView Contacts { get; set; }

        public UserModel(ObservableCollection<ConversationViewModel> conversations, ObservableCollection<UserViewModel> contacts)
        {
            Conversations = new ListCollectionView(conversations);
            //add handlers for when conversations change

            Contacts = new ListCollectionView(contacts);
            //add handlers for when contacts change
        }
    }

    public class ConversationViewModel
    {}
}

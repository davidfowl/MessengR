using MessengR.Client.ViewModel;
using MessengR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MessengR.Client.Helpers
{
    public class ChatViewDataTemplateSelectors : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate MineTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var message = item as Message;
            if (message.IsMine)
            {
                return MineTemplate;
            }
            return DefaultTemplate;
        }
    }

    public class ConversationViewDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OnlineTemplate { get; set; }
        public DataTemplate OfflineTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var contact = item as ContactViewModel;
            if (contact != null)
            {
                if (contact.IsOnline)
                {
                    return OnlineTemplate;
                }
            }
            return OfflineTemplate;
        }
    }

    public class ContactViewDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OnlineTemplate { get; set; }
        public DataTemplate OfflineTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var contact = item as ContactViewModel;
            if (contact.IsOnline)
            {
                return OnlineTemplate;
            }
            return OfflineTemplate;
        }
    }
}

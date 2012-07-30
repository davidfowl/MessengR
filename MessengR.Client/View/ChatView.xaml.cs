using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessengR.Client.Interface;
using MessengR.Client.ViewModel;
using Microsoft.Practices.Prism.Commands;

namespace MessengR.Client.View
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView
    {
        public ChatView()
        {
            InitializeComponent();
            //INotifyCollectionChanged interface which contains the event handler is explicitly implemented, 
            //which means you have to first cast the ItemCollection before the event handler can be used
            ((INotifyCollectionChanged)ConversationList.Items).CollectionChanged += ChatView_CollectionChanged;
        }

        void ChatView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ConversationList.ScrollIntoView(e.NewItems[0]);
            }
        }
    }
}

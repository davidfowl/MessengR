using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SignalR.Client.Hubs;

namespace MessengR.Client.Hubs
{
    class ChatFactory
    {
        public static HubConnection Instance { get; private set; }

        private static Chat _chat;
        public static Chat Chat
        {
            get
            {
                if (_chat == null)
                {
                    _chat = new Chat(Instance);

                }
                return _chat;
            }
            set { _chat = value; }
        }

        public static void InitialiseConnection(string url)
        {
            if (Instance == null)
            {
                Instance = new HubConnection(url);
            }
        }

    }
}

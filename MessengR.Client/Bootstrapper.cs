using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengR.Client.Interface;
using MessengR.Client.Service;
using MessengR.Client.View;

namespace MessengR.Client
{
    public class Bootstrapper
    {
        public static void Initialize()
        {
            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());

            ServiceProvider.Instance.Register<IChatDialog, ChatViewDialog>();
            ServiceProvider.Instance.Register<ILoginDialog, LoginViewDialog>();
        }
    }
}

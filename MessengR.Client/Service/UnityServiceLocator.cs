using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengR.Client.Interface;
using Microsoft.Practices.Unity;

namespace MessengR.Client.Service
{
    public class UnityServiceLocator : IServiceLocator
    {
        private UnityContainer _container;

        public UnityServiceLocator()
        {
            _container = new UnityContainer();
        }

        #region Implementation of IServiceLocator

        void IServiceLocator.Register<TInterface, TImplementation>()
        {
            _container.RegisterType<TInterface, TImplementation>();
        }

        TInterface IServiceLocator.Get<TInterface>()
        {
            return _container.Resolve<TInterface>();
        }

        #endregion
    }
}

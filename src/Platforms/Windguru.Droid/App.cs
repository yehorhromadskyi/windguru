using Android.App;
using Android.Runtime;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using Windguru.Core.Services;
using Windguru.Core.Services.Implementation;

namespace Windguru.Droid
{
    [Application]
    public class App : Application
    {
        protected App(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var container = new UnityContainer();

            container.RegisterType<IHttpProvider, HttpProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApiProvider, ApiProvider>(new ContainerControlledLifetimeManager());

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
        }
    }
}
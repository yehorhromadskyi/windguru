using Android.App;
using Android.Runtime;
using Newtonsoft.Json;
using System;

namespace Windguru.Droid
{
    [Application]
    public class App : Application
    {
        protected App(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"***Exception: {JsonConvert.SerializeObject(e.ExceptionObject)}");
        }

        public override void OnCreate()
        {
            //Locator.CurrentMutable.RegisterConstant<IHttpProvider>(new HttpProvider());

            base.OnCreate();
        }
    }
}
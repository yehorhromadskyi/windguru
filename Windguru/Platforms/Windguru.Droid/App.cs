using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Windguru.Core.ViewModels;
using Splat;
using ReactiveUI;
using Windguru.Core.Services;
using Windguru.Core.Services.Implementation;
using Newtonsoft.Json;

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
            Locator.CurrentMutable.RegisterConstant<IHttpProvider>(new HttpProvider());

            base.OnCreate();
        }
    }
}
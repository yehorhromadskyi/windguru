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
using ReactiveUI;

namespace Windguru.Droid.Activities
{
    [Activity(Label = "FirstActivity", MainLauncher = true)]
    public class FirstActivity : ReactiveActivity<FirstViewModel>
    {
        public EditText TextEdit { get; set; }
        public TextView TextView { get; set; }
        public Button Button { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FirstView);

            ViewModel = new FirstViewModel();

            this.WireUpControls();

            this.Bind(ViewModel,
                      vm => vm.Text,
                      v => v.TextEdit.Text);

            this.Bind(ViewModel,
                      vm => vm.Text,
                      v => v.TextView.Text);

            this.BindCommand(ViewModel,
                             vm => vm.ClearCommand,
                             v => v.Button);
        }
    }
}
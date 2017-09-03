using Android.App;
using Android.Widget;
using Android.OS;
using ReactiveUI;
using Windguru.Core.ViewModels;
using Splat;
using Windguru.Core.Services;
using Windguru.Core.Services.Implementation;
using Windguru.Core.Models.Api;
using Windguru.Droid.Adapters;

namespace Windguru.Droid.Activities
{
    [Activity(Label = "Windguru.Droid", MainLauncher = true)]
    public class MainActivity : ReactiveActivity<MainViewModel>
    {
        public EditText SearchEditText { get; private set; }
        public ListView SearchResultsListView { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Locator.CurrentMutable.RegisterConstant<IHttpProvider>(new HttpProvider());

            SetContentView(Resource.Layout.Main);

            var httpProvider = Locator.CurrentMutable.GetService<IHttpProvider>();
            
            ViewModel = new MainViewModel(httpProvider);

            this.WireUpControls();

            this.Bind(ViewModel, vm => vm.SearchableText, v => v.SearchEditText.Text);
            //this.Bind(ViewModel, vm => vm.Password, v => v.PasswordEditText.Text);
            //this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.LoginButton);
        }
    }
}


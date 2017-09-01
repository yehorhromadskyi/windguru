using Android.App;
using Android.Widget;
using Android.OS;
using ReactiveUI;
using Windguru.Core.ViewModels;

namespace Windguru.Droid.Activities
{
    [Activity(Label = "Windguru.Droid", MainLauncher = true)]
    public class MainActivity : ReactiveActivity, IViewFor<MainViewModel>
    {
        public TextView TheTextView { get; private set; }
        //public EditText PasswordEditText { get; private set; }
        //public Button LoginButton { get; private set; }

        public MainViewModel ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel)value;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            
            ViewModel = new MainViewModel();

            this.WireUpControls();

            this.Bind(ViewModel, vm => vm.Login, v => v.TheTextView.Text);
            //this.Bind(ViewModel, vm => vm.Password, v => v.PasswordEditText.Text);
            //this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.LoginButton);

        }
    }
}


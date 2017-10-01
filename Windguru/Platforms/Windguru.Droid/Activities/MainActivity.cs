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
    [Activity(Label = "Windguru", MainLauncher = false)]
    public class MainActivity : ReactiveActivity<MainViewModel>
    {
        public EditText SearchEditText { get; private set; }
        public ListView SearchResultsListView { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var httpProvider = Locator.CurrentMutable.GetService<IHttpProvider>();
            
            ViewModel = new MainViewModel(httpProvider);

            this.WireUpControls();

            this.Bind(ViewModel, vm => vm.SearchableText, v => v.SearchEditText.Text);

            var adapter = new ReactiveListAdapter<SpotInfo>(ViewModel.Spots, (spot, viewGroup) =>
            {
                var view = LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
                var textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);

                textView.Text = spot.Name;

                return view;
            });

            SearchResultsListView.Adapter = adapter;

            //this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.LoginButton);
        }
    }
}


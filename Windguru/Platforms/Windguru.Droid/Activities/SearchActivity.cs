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
using Windguru.Core.Models.Api;

namespace Windguru.Droid.Activities
{
    [Activity(Label = "Windguru", MainLauncher = true)]
    public class SearchActivity : ReactiveActivity<SearchViewModel>
    {
        public EditText SearchEditText { get; private set; }
        public ListView SearchResultsListView { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SearchView);
            this.WireUpControls();

            ViewModel = new SearchViewModel();

            var adapter = new ReactiveListAdapter<SpotInfo>(ViewModel.Spots, (spot, viewGroup) =>
            {
                var view = LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
                var textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);

                textView.Text = spot.Name;

                return view;
            });

            SearchResultsListView.Adapter = adapter;

            SearchResultsListView.SetOnScrollListener()

            this.Bind(ViewModel, vm => vm.SearchableText, v => v.SearchEditText.Text);
        }
    }
}
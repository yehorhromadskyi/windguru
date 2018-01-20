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
using Microsoft.Practices.ServiceLocation;
using Windguru.Core.Services;
using Android.Text;
using System.Threading;
using System.Threading.Tasks;
using Windguru.Droid.Common;
using Android.Support.V7.App;
using Windguru.Core.Models.Api;

namespace Windguru.Droid.Activities
{
    [Activity(Label = "SearchActivity", MainLauncher = true, Theme = "@style/MainTheme")]
    public class SearchActivity : AppCompatActivity
    {
        IApiProvider _apiProvider;

        ArrayAdapter<SpotInfo> _spotsAdapter;
        ScrollListener _scrollListener;

        int _page = 1;

        public EditText SearchEditText { get; set; }
        public Button SearchButton { get; set; }
        public ListView ResultsListView { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SearchView);

            _apiProvider = ServiceLocator.Current.GetInstance<IApiProvider>();

            SearchEditText = FindViewById<EditText>(Resource.Id.searchEditText);
            SearchButton = FindViewById<Button>(Resource.Id.searchButton_SearchView);
            ResultsListView = FindViewById<ListView>(Resource.Id.searchResultsListView);

            _spotsAdapter = new ArrayAdapter<SpotInfo>(this, Android.Resource.Layout.SimpleListItem1);
            _scrollListener = new ScrollListener();

            SearchButton.Click += OnSearchClicked;
            _scrollListener.ScrolledToBottom += OnScrolledToBottom;
            ResultsListView.ItemClick += OnSpotClicked;

            ResultsListView.Adapter = _spotsAdapter;
            ResultsListView.SetOnScrollListener(_scrollListener);

            SearchEditText.Text = "Maui";
        }

        private void OnSpotClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            var spot = _spotsAdapter.GetItem(e.Position);
            var intent = new Intent(this, typeof(SpotForecastActivity));
            intent.PutExtra("spotId", spot.Id);

            StartActivity(intent);
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            _page = 1;
            var spots = await _apiProvider.GetSpotsAsync(SearchEditText.Text);

            if (!_spotsAdapter.IsEmpty)
            {
                _spotsAdapter.Clear();
            }

            _spotsAdapter.AddAll(spots.ToList());

            _spotsAdapter.NotifyDataSetChanged();
        }

        private async void OnScrolledToBottom(object sender, EventArgs e)
        {
            var spots = await _apiProvider.GetSpotsAsync(SearchEditText.Text, ++_page);

            _spotsAdapter.AddAll(spots.ToList());

            _spotsAdapter.NotifyDataSetChanged();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            SearchButton.Click -= OnSearchClicked;
            ResultsListView.ItemClick -= OnSpotClicked;
            _scrollListener.ScrolledToBottom -= OnScrolledToBottom;
        }
    }
}
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
using System.Reactive.Linq;
using Android.Text;
using System.Reactive.Disposables;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;
using Windguru.Droid.Common;
using Windguru.Core.Models.Api;

namespace Windguru.Droid.Activities
{
    [Activity(Label = "SearchActivity", MainLauncher = true)]
    public class SearchActivity : Activity
    {
        readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        IApiProvider _apiProvider;

        ArrayAdapter<SpotInfo> _spotsAdapter;

        int _page = 1;

        public EditText SearchEditText { get; set; }
        public ListView ResultsListView { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SearchView);

            _apiProvider = ServiceLocator.Current.GetInstance<IApiProvider>();

            SearchEditText = FindViewById<EditText>(Resource.Id.SearchEditText);
            ResultsListView = FindViewById<ListView>(Resource.Id.SearchResultsListView);

            _spotsAdapter = new ArrayAdapter<SpotInfo>(this, Android.Resource.Layout.SimpleListItem1);
            var scrollListener = new ScrollListener();
            ResultsListView.Adapter = _spotsAdapter;
            ResultsListView.SetOnScrollListener(scrollListener);

            var loadMore = Observable.FromEventPattern(
                                         h => scrollListener.ScrolledToBottom += h,
                                         h => scrollListener.ScrolledToBottom -= h)
                                     .SelectMany(_ =>
                                     {
                                         return _apiProvider.GetSpotsAsync(SearchEditText.Text, ++_page);
                                     })
                                     .ObserveOn(SynchronizationContext.Current)
                                     .Subscribe(results =>
                                     {
                                         _spotsAdapter.AddAll(results.ToList());

                                         _spotsAdapter.NotifyDataSetChanged();
                                     });

            var textChanged = SearchEditText.Events()
                                            .TextChanged
                                            .Where(args => args.Text.ToString().Length > 1)
                                            .Throttle(TimeSpan.FromSeconds(.5))
                                            .DistinctUntilChanged()
                                            .SelectMany(args =>
                                            {
                                                _page = 1;
                                                return _apiProvider.GetSpotsAsync(args.Text.ToString());
                                            })
                                            .ObserveOn(SynchronizationContext.Current)
                                            .Subscribe(results =>
                                            {
                                                if (!_spotsAdapter.IsEmpty)
                                                {
                                                    _spotsAdapter.Clear();
                                                }

                                                _spotsAdapter.AddAll(results.ToList());
                                                
                                                _spotsAdapter.NotifyDataSetChanged();
                                            });

            var itemClick = ResultsListView.Events()
                                           .ItemClick
                                           .Subscribe(args =>
                                           {
                                               var spot = _spotsAdapter.GetItem(args.Position);
                                               var intent = new Intent(this, typeof(SpotForecastActivity));
                                               intent.PutExtra("spotId", spot.Id);

                                               StartActivity(intent);
                                           });

            _compositeDisposable.Add(loadMore);
            _compositeDisposable.Add(textChanged);
            _compositeDisposable.Add(itemClick);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _compositeDisposable.Clear();
        }
    }
}
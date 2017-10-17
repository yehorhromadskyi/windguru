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

namespace Windguru.Droid.Activities
{
    [Activity(Label = "SearchActivity", MainLauncher = true)]
    public class SearchActivity : Activity
    {
        readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        IApiProvider _apiProvider;

        ArrayAdapter<string> _spotsAdapter;

        public EditText SearchEditText { get; set; }
        public ListView ResultsListView { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SearchView);

            _apiProvider = ServiceLocator.Current.GetInstance<IApiProvider>();

            SearchEditText = FindViewById<EditText>(Resource.Id.SearchEditText);
            ResultsListView = FindViewById<ListView>(Resource.Id.SearchResultsListView);

            _spotsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);
            ResultsListView.Adapter = _spotsAdapter;

            var textChanged = SearchEditText.Events()
                                            .TextChanged
                                            .Where(args => args.Text.ToString().Length > 1)
                                            .Throttle(TimeSpan.FromSeconds(1))
                                            .ObserveOn(SynchronizationContext.Current)
                                            .Subscribe(async args =>
                                            {
                                                var text = args.Text.ToString();
                                                System.Diagnostics.Debug.WriteLine($"(*) Loading  Started for {text}");

                                                var results = await _apiProvider.GetSpotsAsync(text);

                                                if (_spotsAdapter.Count > 0)
                                                {
                                                    _spotsAdapter.Clear();
                                                }

                                                _spotsAdapter.AddAll(results.Select(s => s.Name).ToList());
                                                _spotsAdapter.NotifyDataSetChanged();

                                                System.Diagnostics.Debug.WriteLine($"(*) Loading Finished for {text}");
                                            });

            _compositeDisposable.Add(textChanged);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _compositeDisposable.Clear();
        }
    }
}
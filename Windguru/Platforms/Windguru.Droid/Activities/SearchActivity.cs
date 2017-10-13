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

namespace Windguru.Droid.Activities
{
    [Activity(Label = "SearchActivity", MainLauncher = true)]
    public class SearchActivity : Activity
    {
        readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        IApiProvider _apiProvider;

        public EditText SearchEditText { get; set; }
        public ListView ResultsListView { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SearchView);

            _apiProvider = ServiceLocator.Current.GetInstance<IApiProvider>();

            SearchEditText = FindViewById<EditText>(Resource.Id.SearchEditText);
            ResultsListView = FindViewById<ListView>(Resource.Id.SearchResultsListView);

            var textChanged = SearchEditText.Events()
                                            .TextChanged
                                            .Where(args => args.Text.Any())
                                            //.Throttle(TimeSpan.FromSeconds(0.75))
                                            .Subscribe(async args =>
                                            {
                                                System.Diagnostics.Debug.WriteLine("Loading Started");
                                                var results = await _apiProvider.GetSpotsAsync(args.Text.ToString());
                                                //var results = new List<string>();
                                                //for (int i = 0; i < 10; i++)
                                                //{
                                                //    results.Add(args.Text.ToString());
                                                //}

                                                ResultsListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, results.Select(s => s.Name).ToList());
                                                //ResultsListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, results);
                                                System.Diagnostics.Debug.WriteLine("Loading Finished");
                                            });

            compositeDisposable.Add(textChanged);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            compositeDisposable.Clear();
        }
    }
}
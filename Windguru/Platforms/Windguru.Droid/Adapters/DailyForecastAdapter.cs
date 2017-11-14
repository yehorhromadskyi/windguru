using Android.Support.V7.Widget;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Windguru.Core.Models.Common;
using Windguru.Droid.ViewHolders;

namespace Windguru.Droid.Adapters
{
    public class DailyForecastAdapter : RecyclerView.Adapter
    {
        readonly Subject<DailyForecast> _itemClicked = new Subject<DailyForecast>();

        readonly List<DailyForecast> _dailyForecast;

        public IObservable<DailyForecast> ItemClicked => _itemClicked.AsObservable();

        public override int ItemCount => _dailyForecast.Count;

        public DailyForecastAdapter(List<DailyForecast> dailyForecast)
        {
            _dailyForecast = dailyForecast;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;
            var id = Resource.Layout.DailyForecast;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new DailyForecastViewHolder(itemView, position => _itemClicked.OnNext(_dailyForecast[position]));
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as DailyForecastViewHolder;
            var dailyData = _dailyForecast[position];

            viewHolder.Day.Text = dailyData.Day;
        }
    }
}
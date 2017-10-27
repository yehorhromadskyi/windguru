using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Windguru.Core.Models.Common;
using System.Collections.Generic;
using Windguru.Droid.ViewHolders;

namespace Windguru.Droid.Adapters
{
    public class HourlyForecastsAdapter : RecyclerView.Adapter
    {
        readonly List<HourlyForecast> _hourlyForecasts;

        public HourlyForecastsAdapter(List<HourlyForecast> hourlyForecasts)
        {
            _hourlyForecasts = hourlyForecasts;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;
            var id = Resource.Layout.HourlyForecast;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new HourlyForecastViewHolder(itemView);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as HourlyForecastViewHolder;
            viewHolder.HourTextView.Text = _hourlyForecasts[position].Hour;
        }

        public override int ItemCount => _hourlyForecasts.Count;

    }
}
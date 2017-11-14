using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Windguru.Core.Models.Common;
using System.Collections.Generic;
using Windguru.Droid.ViewHolders;

namespace Windguru.Droid.Adapters
{
    public class HourlyForecastAdapter : RecyclerView.Adapter
    {
        List<HourlyForecast> _hourlyForecast;

        public override int ItemCount => _hourlyForecast.Count;

        public HourlyForecastAdapter(List<HourlyForecast> hourlyForecasts)
        {
            _hourlyForecast = hourlyForecasts;
        }

        public void ChangeData(List<HourlyForecast> hourlyForecasts)
        {
            _hourlyForecast = hourlyForecasts;
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
            var hourlyData = _hourlyForecast[position];

            viewHolder.DayTextView.Text = hourlyData.Day;
            viewHolder.HourTextView.Text = hourlyData.Hour;
            viewHolder.TemperatureTextView.Text = hourlyData.Temperature;
            viewHolder.WindSpeedTextView.Text = hourlyData.WindSpeed;
            viewHolder.PrecipitationTextView.Text = hourlyData.Precipitation;

            var windDirection = hourlyData.WindDirection ?? 0;
            viewHolder.WindDirectionImageView.Rotation = (float)windDirection;
        }
    }
}
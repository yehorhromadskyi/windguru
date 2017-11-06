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
using Android.Support.V7.Widget;
using Windguru.Core.Models.Common;

namespace Windguru.Droid.Adapters
{
    public class DailyForecastAdapter : RecyclerView.Adapter
    {
        private readonly List<DailyForecast> dailyForecast;

        public DailyForecastAdapter(List<DailyForecast> dailyForecast)
        {
            this.dailyForecast = dailyForecast;
        }

        public override int ItemCount => dailyForecast.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }
    }
}
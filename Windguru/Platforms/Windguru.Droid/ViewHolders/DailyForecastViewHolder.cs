using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace Windguru.Droid.ViewHolders
{
    public class DailyForecastViewHolder : RecyclerView.ViewHolder
    {
        public TextView Day { get; private set; }

        public DailyForecastViewHolder(View itemView, Action<int> clicked) : base(itemView)
        {
            Day = itemView.FindViewById<TextView>(Resource.Id.dayDailyTextView);

            itemView.Click += delegate { clicked(AdapterPosition); };
        }
    }
}
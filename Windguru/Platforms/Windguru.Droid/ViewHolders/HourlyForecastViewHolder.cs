﻿using System;
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

namespace Windguru.Droid.ViewHolders
{
    public class HourlyForecastViewHolder : RecyclerView.ViewHolder
    {
        public TextView DayTextView { get; private set; }
        public TextView HourTextView { get; private set; }

        public HourlyForecastViewHolder(View itemView) : base(itemView)
        {
            HourTextView = itemView.FindViewById<TextView>(Resource.Id.hourTextView);
            DayTextView = itemView.FindViewById<TextView>(Resource.Id.dayTextView);
        }
    }
}
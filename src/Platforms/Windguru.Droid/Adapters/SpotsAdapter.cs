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
using Windguru.Core.Models.Api;

namespace Windguru.Droid.Adapters
{
    public class SpotsAdapter : BaseAdapter<SpotInfo>
    {
        List<SpotInfo> _items;
        LayoutInflater _inflater;

        public SpotsAdapter(LayoutInflater inflater, List<SpotInfo> items)
        {
            _items = items;
            _inflater = inflater;
        }

        public override long GetItemId(int position)
        {
            return position;
            //return _items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = _inflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = _items[position].Name;
            return view;
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override SpotInfo this[int position]
        {
            get { return _items[position]; }
        }
    }
}
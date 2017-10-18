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
using System.Windows.Input;

namespace Windguru.Droid.Common
{
    // https://github.com/codepath/android_guides/wiki/Endless-Scrolling-with-AdapterViews-and-RecyclerView
    public class ScrollListener : Java.Lang.Object, AbsListView.IOnScrollListener
    {
        int _totalItemCount;
        bool _updateRequired;

        public event EventHandler ScrolledToBottom;

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            

            if (totalItemCount < _totalItemCount)
            {
                _updateRequired = true;
            }

            if (totalItemCount > _totalItemCount)
            {
                _updateRequired = true;
            }

            System.Diagnostics.Debug.WriteLine($"!!!previous {_totalItemCount}");
            System.Diagnostics.Debug.WriteLine($"!!!total {totalItemCount}");
            if (_updateRequired)
            {
                System.Diagnostics.Debug.WriteLine("!!!YES");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("!!!NO");
            }

            _totalItemCount = totalItemCount;

            var scrollIsAboutToEnd = firstVisibleItem + visibleItemCount >= (totalItemCount - 4);
            if (scrollIsAboutToEnd && _updateRequired)
            {
                ScrolledToBottom?.Invoke(this, EventArgs.Empty);
                _updateRequired = false;

                //System.Diagnostics.Debug.WriteLine("!!!Load More");
            }
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
        }
    }
}
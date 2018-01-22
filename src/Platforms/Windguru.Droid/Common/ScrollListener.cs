using Android.Runtime;
using Android.Widget;
using System;

namespace Windguru.Droid.Common
{
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

            _totalItemCount = totalItemCount;

            var scrollIsAboutToEnd = firstVisibleItem + visibleItemCount >= (totalItemCount - 4);
            if (scrollIsAboutToEnd && _updateRequired)
            {
                ScrolledToBottom?.Invoke(this, EventArgs.Empty);
                _updateRequired = false;
            }
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
        }
    }
}
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
    public class EndlessScrollListener : Java.Lang.Object, AbsListView.IOnScrollListener
    {
        public ICommand LoadMoreCommand { get; private set; }

        public EndlessScrollListener(ICommand loadMoreCommand)
        {
            LoadMoreCommand = loadMoreCommand;
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            var loadMore = firstVisibleItem + visibleItemCount >= (totalItemCount - 4);
            if (loadMore && LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
            {
                LoadMoreCommand.Execute(null);
            }
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
        }
    }
}
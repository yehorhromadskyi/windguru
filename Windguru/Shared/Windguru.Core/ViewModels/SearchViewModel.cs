using Windguru.Core.Services;
using Windguru.Core.Services.Implementation;

namespace Windguru.Core.ViewModels
{
    public class SearchViewModel
    {
        readonly IApiProvider _apiProvider;

        string _searchableText;
        //public string SearchableText
        //{
        //    get => _searchableText;
        //    set => this.RaiseAndSetIfChanged(ref _searchableText, value);
        //}

        //readonly ReactiveList<SpotInfo> _spots = new ReactiveList<SpotInfo>();
        //public IReadOnlyReactiveList<SpotInfo> Spots => _spots;

        //public ReactiveCommand<int, IEnumerable<SpotInfo>> LoadMoreSpots { get; }

        public SearchViewModel()
        {
            _apiProvider = new ApiProvider(new HttpProvider());

            //var searchObservable =
            // this.WhenAnyValue(vm => vm.SearchableText)
            //     .Where(t => !string.IsNullOrEmpty(t))
            //     .Throttle(TimeSpan.FromSeconds(.75))
            //     .ObserveOn(RxApp.MainThreadScheduler)
            //     .Subscribe(async text =>
            //     {
            //         var spots = await _apiProvider.GetSpotsAsync(text);

            //         using (Spots.SuppressChangeNotifications())
            //         {
            //             if (_spots.Any())
            //             {
            //                 _spots.Clear();
            //             }

            //             _spots.AddRange(spots);
            //         }
            //     });
        }
    }
}

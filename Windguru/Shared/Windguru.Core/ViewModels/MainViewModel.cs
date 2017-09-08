using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windguru.Core.Models.Api;
using Windguru.Core.Services;

namespace Windguru.Core.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        readonly string REQUEST = "https://www.windguru.cz/int/jsonapi.php?client=android&search={0}&limit=20&page=1&q=search_spots&username=&password=";

        readonly IHttpProvider _httpProvider;

        string _searchableText;
        public string SearchableText
        {
            get => _searchableText;
            set => this.RaiseAndSetIfChanged(ref _searchableText, value);
        }

        readonly ReactiveList<SpotInfo> _spots = new ReactiveList<SpotInfo>();

        public IReadOnlyReactiveList<SpotInfo> Spots => _spots;

        //public ReactiveCommand LoginCommand { get; private set; }

        public MainViewModel(IHttpProvider httpProvider)
        {
            _httpProvider = httpProvider;
            this.WhenAnyValue(vm => vm.SearchableText)
                .Where(t => !string.IsNullOrEmpty(t))
                .Throttle(TimeSpan.FromSeconds(.75), RxApp.MainThreadScheduler)
                .Subscribe(async text =>
                {
                    var spotsResponse = await _httpProvider.GetAsync(string.Format(REQUEST, text));
                    var searchResult = JsonConvert.DeserializeObject<SearchSpotsResult>(spotsResponse.Result);

                    if (searchResult.Spots == null)
                    {
                        searchResult.Spots = new List<SpotInfo>();
                    }

                    using (Spots.SuppressChangeNotifications())
                    {
                        if (_spots.Any())
                        {
                            _spots.Clear();
                        }

                        _spots.AddRange(searchResult.Spots);
                    }
                });

            //var canLogin = this.WhenAnyValue(
            //    vm => vm.Login,
            //    vm => vm.Password,
            //    (l, p) => !string.IsNullOrEmpty(l) && !string.IsNullOrEmpty(p));

            //LoginCommand = ReactiveCommand.Create(() =>
            //{
            //    System.Diagnostics.Debug.WriteLine("LoginCommand");
            //}, canLogin);
        }
    }
}

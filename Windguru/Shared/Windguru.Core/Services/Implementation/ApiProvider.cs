using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windguru.Core.Models.Api;

namespace Windguru.Core.Services.Implementation
{
    public class ApiProvider : IApiProvider
    {
        readonly string SEARCH_REQUEST = "https://www.windguru.cz/int/jsonapi.php?client=android&search={0}&limit=20&page={1}&q=search_spots&username=&password=";
        readonly string SELECT_SPOT_REQUEST = "https://www.windguru.cz/int/jsonapi.php?client=android&id_spot={0}&id_model=3&no_wave=1&q=wforecast&username=&password=";

        private readonly IHttpProvider _httpProvider;

        public ApiProvider(IHttpProvider httpProvider)
        {
            _httpProvider = httpProvider;
        }

        public async Task<IEnumerable<SpotInfo>> GetSpotsAsync(string searchQuery, int page = 1)
        {
            var spotsResponse = await _httpProvider.GetAsync(string.Format(SEARCH_REQUEST, searchQuery, page));
            var searchResult = JsonConvert.DeserializeObject<SearchSpotsResult>(spotsResponse.Result);
            
            return searchResult.Spots ?? new List<SpotInfo>();
        }

        public async Task<SpotForecast> GetSpotForecastAsync(int spotId)
        {
            var spotsResponse = await _httpProvider.GetAsync(string.Format(SELECT_SPOT_REQUEST, spotId));

            var searchResult = JsonConvert.DeserializeObject<SpotForecast>(spotsResponse.Result);

            return searchResult ?? new SpotForecast();
        }
    }
}

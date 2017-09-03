using Newtonsoft.Json;
using System.Collections.Generic;

namespace Windguru.Core.Models.Api
{
    public class SearchSpotsResult
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("spots")]
        public IList<SpotInfo> Spots { get; set; }
    }
}

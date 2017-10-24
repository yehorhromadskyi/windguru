using Newtonsoft.Json;

namespace Windguru.Core.Models.Api
{
    public class SpotInfo
    {
        [JsonProperty("id_spot")]
        public int Id { get; set; }

        [JsonProperty("spotname")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        public override string ToString() => Name;
    }
}

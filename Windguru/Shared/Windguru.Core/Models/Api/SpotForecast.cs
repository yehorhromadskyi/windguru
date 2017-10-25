using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windguru.Core.Models.Api
{
    public class SpotForecast
    {
        [JsonProperty("spot")]
        public string Name { get; set; }

        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("utc_offset")]
        public long UtcOffset { get; set; }

        [JsonProperty("fcst")]
        public Forecast Forecast { get; set; }
    }

    public class Forecast
    {
        [JsonProperty("3")]
        public ForecastModel Data { get; set; }
    }

    public class ForecastModel
    {
        [JsonProperty("RH")]
        public long?[] RH { get; set; }

        [JsonProperty("FLHGT")]
        public long?[] FLHGT { get; set; }

        // Precip. (mm/3h)
        [JsonProperty("APCP")]
        public double?[] APCP { get; set; }

        // Wind gusts
        [JsonProperty("GUST")]
        public double?[] GUST { get; set; }

        // Cloud cover high
        [JsonProperty("HCDC")]
        public long?[] HCDC { get; set; }

        // Cloud cover mid
        [JsonProperty("MCDC")]
        public long?[] MCDC { get; set; }

        // Cloud cover low
        [JsonProperty("LCDC")]
        public long?[] LCDC { get; set; }

        [JsonProperty("PCPT")]
        public long?[] PCPT { get; set; }

        [JsonProperty("TMP")]
        public double?[] TMP { get; set; }

        [JsonProperty("SMERN")]
        public string[] SMERN { get; set; }

        [JsonProperty("SLP")]
        public long?[] SLP { get; set; }

        [JsonProperty("TCDC")]
        public long?[] TCDC { get; set; }

        [JsonProperty("WINDDIR")]
        public long?[] WINDDIR { get; set; }

        [JsonProperty("WINDSPD")]
        public double?[] WINDSPD { get; set; }

        // Temperature
        [JsonProperty("TMPE")]
        public double?[] TMPE { get; set; }

        [JsonProperty("hr_h")]
        public string[] HrH { get; set; }

        [JsonProperty("hr_d")]
        public string[] HrD { get; set; }

        [JsonProperty("initdate")]
        public string InitDate { get; set; }

        [JsonProperty("init_h")]
        public string InitH { get; set; }

        [JsonProperty("initstamp")]
        public long? Initstamp { get; set; }

        [JsonProperty("model_name")]
        public string ModelName { get; set; }

        [JsonProperty("model_longname")]
        public string ModelLongname { get; set; }

        [JsonProperty("update_last")]
        public string UpdateLast { get; set; }

        [JsonProperty("update_next")]
        public string UpdateNext { get; set; }
    }
}

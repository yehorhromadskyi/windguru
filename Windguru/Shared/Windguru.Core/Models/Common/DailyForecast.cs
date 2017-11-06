using System.Collections.Generic;

namespace Windguru.Core.Models.Common
{
    public class DailyForecast
    {
        public string Day { get; set; }
        public string Temperature { get; set; }

        public List<HourlyForecast> HourlyForecast { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windguru.Core.Models.Common
{
    public class HourlyForecast
    {
        public string Hour { get; set; }
        public string Day { get; set; }
        public string Precipitation { get; set; }
        public string Temperature { get; set; }
        public string WindSpeed { get; set; }
        public double? WindGusts { get; set; }
        public double? WindDirection { get; set; }
    }
}

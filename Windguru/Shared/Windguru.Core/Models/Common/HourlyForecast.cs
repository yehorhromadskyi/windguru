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
        public double? Precipitation { get; set; }
        public double? Temperature { get; set; }
        public double? WindDirection { get; set; }
        public double? WindSpeed { get; set; }
    }
}

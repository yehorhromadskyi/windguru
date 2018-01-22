using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windguru.Core.Models.Api;

namespace Windguru.Core.Services
{
    public interface IApiProvider
    {
        Task<IEnumerable<SpotInfo>> GetSpotsAsync(string searchQuery, int page = 1);

        Task<SpotForecast> GetSpotForecastAsync(int spotId);
    }
}

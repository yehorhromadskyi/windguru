using System.Collections.Generic;
using System.Threading.Tasks;
using Windguru.Core.Models.Http;

namespace Windguru.Core.Services
{
    public interface IHttpProvider
    {
        Task<Response> GetAsync(string url);
        Task<Response> GetAsync(string url, Dictionary<string, string> parameters);
        Task<Response> PostAsync(string url);
        Task<Response> PostAsync(string url, string content);
        Task<Response> PostAsync(string url, Dictionary<string, string> parameters);
    }
}

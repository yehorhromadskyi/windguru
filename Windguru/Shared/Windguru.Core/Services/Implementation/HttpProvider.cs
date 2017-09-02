using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windguru.Core.Models.Http;

namespace Windguru.Core.Services.Implementation
{
    public sealed class HttpProvider : IHttpProvider
    {
        private readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);
        private const string ContentType = "application/json";

        private readonly HttpClient _httpClient;

        public HttpProvider()
        {
            _httpClient = new HttpClient() { Timeout = Timeout };
        }

        public Task<Response> GetAsync(string url)
        {
            return SendRequestAsync(HttpMethod.Get, url);
        }

        public Task<Response> GetAsync(string url, Dictionary<string, string> parameters)
        {
            if (!url.EndsWith("?"))
                url += "?";

            foreach (var parameter in parameters)
            {
                url += $"{parameter.Key}={parameter.Value}&";
            }

            url = url.Remove(url.Length - 1);

            return SendRequestAsync(HttpMethod.Get, url);
        }

        public Task<Response> PostAsync(string url)
        {
            return SendRequestAsync(HttpMethod.Post, url);
        }

        public Task<Response> PostAsync(string url, string content)
        {
            return SendRequestAsync(HttpMethod.Post, url, new StringContent(content, Encoding.UTF8, ContentType));
        }

        public Task<Response> PostAsync(string url, Dictionary<string, string> parameters)
        {
            return SendRequestAsync(HttpMethod.Post, url, new FormUrlEncodedContent(parameters));
        }

        private async Task<Response> SendRequestAsync(HttpMethod method, string url, HttpContent content = null)
        {
            var response = new Response();
            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                if (method == HttpMethod.Post && content != null)
                {
                    requestMessage.Content = content;
                }

                try
                {
                    using (var responseMessage = await _httpClient.SendAsync(requestMessage))
                    {
                        response.Status = responseMessage.StatusCode;
                        response.Result = await responseMessage.Content.ReadAsStringAsync();
                    }
                }
                catch (TimeoutException)
                {
                    response.Status = System.Net.HttpStatusCode.RequestTimeout;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return response;
        }
    }
}

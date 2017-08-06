using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ZedSharp.RateLimit;
using ZedSharp.RequestOptions;
using ZedSharp.Utils;

// TODO: RetryOnError
// TODO: Timeout
namespace ZedSharp.Requester
{
    internal class RiotRequester
    {
        private readonly HttpClient _httpClient;
        private readonly RateLimiter _rateLimiter;

        public RiotRequester(string baseAddress, string apiKey, int maxTries, int timeout)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Connection.Clear();
            _httpClient.DefaultRequestHeaders.ConnectionClose = false;
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
            _httpClient.BaseAddress = new Uri(baseAddress);
            _rateLimiter = new RateLimiter();
        }

        public async Task<T> GetAsync<T>(string method, Dictionary<string, string> options = null, IRequestOptions requestOptions = null, bool wait = true) 
        {
            var url = method;
            if (options != null)
            {
                url = _buildUrlWithOptions(url, options);
            }
            if (requestOptions != null)
            {
                url = url + "?" + string.Join("&", requestOptions.GetRiotOptions().Select(kvp =>
                {
                    if (kvp.Value is IEnumerable)
                    {
                        return string.Join("&", ((IEnumerable) kvp.Value).Cast<object>().Select(v => kvp.Key + "=" + v));
                    }
                    return kvp.Key + "=" + kvp.Value;
                }));
            }
            await _rateLimiter.WaitAllAsync(method, wait);
            var requestTime = DateTime.UtcNow;
            var result = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            var headerDic = result.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.FirstOrDefault());
            await _rateLimiter.AdjustToHeaderAsync(method, requestTime, DateTime.UtcNow, headerDic, wait);
            if ((int)result.StatusCode >= 400)
            {
                throw new ZedException((int)result.StatusCode);
            }
            return JToken.Parse(await result.Content.ReadAsStringAsync()).ToObject<T>();
        }

        private string _buildUrlWithOptions(string url, Dictionary<string, string> options)
        {
            return options.Aggregate(url, (current, kvp) => current.Replace("{" + kvp.Key + "}", kvp.Value));
        }
    }
}

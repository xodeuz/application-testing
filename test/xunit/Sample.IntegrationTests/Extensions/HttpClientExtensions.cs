using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sample.XUnit.IntegrationTests.Extensions
{
    /// <summary>
    ///     Helper extensions to get both status code and convert response to contract
    /// </summary>
    public static class HttpClientExtension
    {
        public static async Task<(T? Contract, HttpStatusCode StatusCode)> GetContractAsync<T>(this HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            var contract = await TryGetContract<T>(response);
            return (contract, response.StatusCode); ;
        }

        public static async Task<(T? Contract, HttpStatusCode StatusCode)> PutContractAsync<T>(this HttpClient client, string url, StringContent content)
        {
            var response = await client.PutAsync(url, content);
            var contract = await TryGetContract<T>(response);
            return (contract, response.StatusCode); ;
        }

        public static async Task<(T? Contract, HttpStatusCode StatusCode)> PostContractAsync<T>(this HttpClient client, string url, StringContent content)
        {
            var response = await client.PostAsync(url, content);
            var contract = await TryGetContract<T>(response);
            return (contract, response.StatusCode);
        }

        private static async Task<T?> TryGetContract<T>(HttpResponseMessage response)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    CheckAdditionalContent = true,
                });
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}

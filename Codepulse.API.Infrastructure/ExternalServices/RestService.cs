using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codepulse.API.Infrastructure.ExternalServices
{
    public class RestService
    {
        private RestClient BuildClient(string url)
        {
            var client = new RestClient(url);

            return client;
        }

        private RestRequest BuildRequest(Method method, Dictionary<string, string> headers)
        {
            var request = new RestRequest();

            request.Method = method;

            foreach (var keyValue in headers)
            {
                request.AddHeader(keyValue.Key, keyValue.Value);
            }

            return request;
        }
        private T Deserialize<T>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<T> Get<T>(string url, Dictionary<string, string> headers) where T : class
        {
            RestClient client = BuildClient(url);

            RestRequest request = BuildRequest(Method.Get, headers);

            RestResponse response = await client.ExecuteAsync(request);

            if (typeof(T) == typeof(RestResponse)) return response as T;

            return Deserialize<T>(response);
        }

        private async Task<T> DataCall<T>(Method method, string url, string data, Dictionary<string, string> headers, Dictionary<string, string> formUrlData = null) where T : class
        {
            RestClient client = BuildClient(url);
            RestRequest request = BuildRequest(method, headers);

            if (formUrlData != null)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                foreach (var keyVal in formUrlData)
                {
                    request.AddParameter(keyVal.Key, keyVal.Value);
                }
            }
            else
            {
                request.AddParameter("application/json", data, ParameterType.RequestBody);
            }

            RestResponse response = await client.ExecuteAsync(request);

            if (typeof(T) == typeof(RestResponse)) return response as T;

            return Deserialize<T>(response);
        }

        public async Task<T> Post<T>(string url, string data, Dictionary<string, string> headers, Dictionary<string, string> formUrlData = null) where T : class
        {
            return await DataCall<T>(Method.Post, url, data, headers, formUrlData);
        }

        public async Task<T> Put<T>(string url, string data, Dictionary<string, string> headers, Dictionary<string, string> formUrlData = null) where T : class
        {
            return await DataCall<T>(Method.Put, url, data, headers, formUrlData);
        }
    }
}

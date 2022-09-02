using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQaApiTest.Support
{
    public class ApiMapper
    {
        public static RestClient? client;
        public static RestRequest? request;
        public static RestResponse? response;


        public static RestClient getClient(string url) => client = new RestClient(url);
        public static RestRequest getRequest(string resource, Method method, string? userId = null) => request = new RestRequest(resource, method);
        public static RestResponse getResponse() => response = client.ExecuteGetAsync(request).Result;
        public static RestResponse postResponse() => response = client.Execute(request);

        //public static RestResponse? getResponse() => response = client.ExecuteGetAsync<>();

        public class ExecuteRequest
        {
            public RestResponse postResponse(RestRequest restRequest)
            {
                var client = new RestClient();
                return client.ExecuteAsync(restRequest).Result;
            }
        }

        public static RestResponse SendRequest(string url, Method method, object? body = null, Dictionary<string, string>? headers = null,
        Dictionary<string, string>? parameters = null, Dictionary<string, string>? userId = null)
        {
            var client = new RestClient();
            var request = new RestRequest(url, method);
            if (body != null)
            {
                request.AddBody(body);
            }
            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    request.AddHeader(key, headers[key]);
                }
            }
            if (parameters != null)
            {
                foreach (var key in parameters.Keys)
                {
                    request.AddHeader(key, parameters[key]);
                }
            }
            if (userId != null)
                foreach (var key in userId.Keys)
                {
                    request.AddHeader(key, userId[key]);
                }
            return client.ExecuteAsync(request).Result;
        }
    }
}
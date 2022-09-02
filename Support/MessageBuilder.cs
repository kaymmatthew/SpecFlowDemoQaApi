using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQaApiTest.Support
{
    class MessageBuilder
    {
        private readonly RestRequest restRequest;
        private readonly RestClient restClient;
        private readonly RestResponse restResponse;

        public MessageBuilder()
        {
            restRequest = new RestRequest();
            restClient = new RestClient();  
            restResponse = new RestResponse();
        }

        public MessageBuilder WithUrl(string url)
        {
            restRequest.Resource = url;
            return this;
        }

        public MessageBuilder WithMethod(Method method)
        {
            restRequest.Method = method;
            return this;
        }

        public MessageBuilder WithJsonBody(object body)
        {
            restRequest.AddJsonBody(body);
            return this;
        }

        public MessageBuilder WithBody(string body)
        {
            restRequest.AddBody(body);
            return this;
        }

        public MessageBuilder WithHeader(Dictionary<string, string> headers)
        {
            foreach (var key in headers.Keys)
            {
                restRequest.AddHeader(key, headers[key]);
            }
            return this;
        }

        public MessageBuilder WithUserId(Dictionary<string, string> userId)
        {
            foreach (var key in userId.Keys)
            {
                restRequest.AddHeader(key, userId[key]);
            }
            return this;
        }

        public MessageBuilder WithBasicToken(string username, string password)
        {
            var token = $"Basic {GetToken.GenerateBasicToken(username, password)}";
            restRequest.AddHeader("authorization", $"Basic {GetToken.GenerateBasicToken(username, password)}");
            return this;
        }
        public RestResponse Build<T>() where T : class
        {
            return restClient.Execute<T>(restRequest);
        }
    }
}
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQaApiTest.Support
{
    class ExecuteRequest
    {
        public RestResponse SendRequest(RestRequest restRequest)
        {
            var client = new RestClient();
            return client.ExecuteAsync(restRequest).Result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQaApiTest.Models
{
    public class CreateNewUserModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public string? userName { get; set; }
            public string? password { get; set; }
    }
    public class CreateNewUserResponseModel
    {
        public string? userID { get; set; }
        public string? username { get; set; }
        public List<object>? books { get; set; }
    }
}
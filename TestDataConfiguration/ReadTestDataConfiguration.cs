using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQaApiTest.TestDataConfiguration
{
    public class ReadTestDataConfiguration
    {
        //public static int rName { get; set; }
        public string? userName;
        public string? password; 
        private static IConfigurationRoot? config { get; set; }

        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "TestDataConfiguration"))
                .AddJsonFile("Settings.json");

            config = builder.Build();

            return config;
        }

        private static Root getData()
        {
            var url = new UriBuilder(Directory.GetCurrentDirectory());
            //var urlPath = Path.GetDirectoryName(url.Path).Replace("/", "\\");
            var urlPath = Path.GetDirectoryName(Uri.UnescapeDataString(url.Path));
            if (urlPath is null) throw new InvalidOperationException("Failed to obtain path");
            var fullPath = Path.Combine(urlPath, "net6.0", "TestDataConfiguration", "Settings.json");
            return JsonConvert.DeserializeObject<Root>(File.ReadAllText(fullPath));
        }
        public static Root data => getData();
        public static string getbaseurl => data.baseUrl.DemoQa;

        public class Root
        {
            public BaseUrl? baseUrl { get; set; }
            public EndPoint? endPoints { get; set; }
        } 

        public class BaseUrl
        {
            public string? DemoQa { get; set; }
        }

        public class EndPoint
        {
            public string? GetAllBooks { get; set; }
            public string? CreateNewUser { get; set; }
            public string? DeleteUser { get; set; }
            public string? AddBooks { get; set; }
        }

        public string demoQaUrl() => GetConfiguration().GetSection("BaseUrl").GetValue<string>("DemoQa");
        public string getAllBooksEndpoint() => GetConfiguration().GetValue<string>("EndPoints:GetAllBooks");
        public string getDeleteUserEndPoint() => GetConfiguration().GetValue<string>("EndPoints:DeleteUser");
        public string getSpecificUserEndPoint() => GetConfiguration().GetValue<string>("EndPoints:GetUser");
        public string getDeleteSpecificBookEndPoint() => GetConfiguration().GetValue<string>("EndPoints:DeleteSingleBook");
        public string getAddBooksEndpoint() => GetConfiguration().GetValue<string>("EndPoints:AddBooks");
        public string getSingleBookEndpoint() => GetConfiguration().GetValue<string>("EndPoints:GetSingleBook");
        public string getCreateNewUserEndPoint() => GetConfiguration().GetValue<string>("EndPoints:CreateNewUser");
        public string getDeleteBooksEndPoint() => GetConfiguration().GetValue<string>("EndPoints:DeleteBooks");
        public string getUserName() => string.Format(GetConfiguration().GetValue<string>
            ("UserCreateTestData:UserName"), DateTime.Now.ToString("HHmmssfff"));
        public string getPassword() => GetConfiguration().GetValue<string>("UserCreateTestData:Password");
        public string getUserId() => GetConfiguration().GetValue<string>("UserIdSetting:UserId");
        public string getIsbn1() => GetConfiguration().GetValue<string>("AddBooksTestData:Isbn1");
        public string getIsbn2() => GetConfiguration().GetValue<string>("AddBooksTestData:Isbn2");
        public string getIsbn3() => GetConfiguration().GetValue<string>("AddBooksTestData:Isbn3");
        public string getIsbn4() => GetConfiguration().GetValue<string>("AddBooksTestData:Isbn4");
    }
}
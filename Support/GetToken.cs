using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQaApiTest.Support
{
    class GetToken
    {
        public static string GenerateBasicToken(string username, string password)
        {
            var s = $"{username}:{password}";
            byte[] a = Encoding.ASCII.GetBytes(s);
            return Convert.ToBase64String(a);
        }
    }
}
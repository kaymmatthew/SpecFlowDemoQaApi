using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQaApiTest.Models
{
    public class DeleteUserModel
    {
            public string? userId { get; set; }
            public string? message { get; set; }
    }
    public class DeleteUserResponseModel
    {
        public int code { get; set; }
        public string? message { get; set; }
    }

    public class DeleteBooksModel
    {
        public string? userId { get; set; }
        public string? message { get; set; }
    }
    public class DeleteSpecificBookModel
    {
        public string? isbn { get; set; }
        public string? userId { get; set; }
    }
    public class DeleteSpecificBookRespModel
    {
        public string? userId { get; set; }
        public string? isbn { get; set; }
        public string? message { get; set; }
    }


}
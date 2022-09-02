using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemoQaApiTest.Models
{
    class AddListOfBooksModel
    {
        public string? userId { get; set; }
        public List<CollectionOfIsbns>? collectionOfIsbns { get; set; }
    }

    class CollectionOfIsbns
    {
        public string? isbn { get; set; }

        public AddListOfBooksModel AddBooks(string userId, List<CollectionOfIsbns> isbn)
        {
            return new AddListOfBooksModel
            {
                userId = userId,
                collectionOfIsbns = new List<CollectionOfIsbns>
                {
                    AddIsbn(isbn.ToString()),
                    AddIsbn(isbn.ToString()),
                    AddIsbn(isbn.ToString()),
                    AddIsbn(isbn.ToString()),
                }
            };
        }

        public CollectionOfIsbns AddIsbn(string isbn)
        {
            return new CollectionOfIsbns
            {
                isbn = isbn
            };
        }

        public class AddBooksResponseModel
        {
            public string isbn { get; set; }
        }

        public class Root
        {
            public List<AddBooksResponseModel> books { get; set; }
        }
    }
}
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecFlowDemoQaApiTest.Models;
using SpecFlowDemoQaApiTest.Support;
using SpecFlowDemoQaApiTest.TestDataConfiguration;
using System.Net;
using static SpecFlowDemoQaApiTest.Models.CollectionOfIsbns;

namespace SpecFlowDemoQaApi.StepDefinitions
{
    [Binding]
    public sealed class GetAllBooksStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ReadTestDataConfiguration readTestDataConfiguration;
        AllBooksResponseModel allBooksResponseModel;
        AddListOfBooksModel addListOfBooksModel;
        CollectionOfIsbns collectionOfIsbns;
        AddBooksResponseModel addBooksResponseModel;
        MessageBuilder messageBuilder;
        public GetAllBooksStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            readTestDataConfiguration = new ReadTestDataConfiguration();
            allBooksResponseModel = new AllBooksResponseModel();
            messageBuilder = new MessageBuilder();
            addListOfBooksModel = new AddListOfBooksModel();
            collectionOfIsbns = new CollectionOfIsbns();
            messageBuilder = new MessageBuilder();
            addBooksResponseModel = new AddBooksResponseModel();
        }

        [Given(@"I have a book service")]
        public void GivenIHaveABookService()
        {
            ApiMapper.getClient(readTestDataConfiguration.demoQaUrl());
        }

        [When(@"I send a Get request to retrieve all books")]
        public void WhenISendAGetRequestToRetrieveAllBooks()
        {
            ApiMapper.getRequest(readTestDataConfiguration.getAllBooksEndpoint(), Method.Get);
        }

        [Then(@"Response code is (.*)")]
        public void ThenResponseIsOK(string respCode)
        {
            var response = ApiMapper.getResponse();
            allBooksResponseModel = JsonConvert.DeserializeObject<AllBooksResponseModel>(response.Content);
        }

        [Then(@"Response body contains:")]
        public void ThenResponseBodyContains(Table table)
        {
            Assert.AreEqual(table.Rows[0]["author"], allBooksResponseModel?.books?[0].author);
            Assert.AreEqual(table.Rows[0]["publisher"], allBooksResponseModel?.books?[0].publisher);
            Assert.AreEqual(table.Rows[0]["subTitle"], allBooksResponseModel?.books?[0].subTitle);
        }

        [When(@"I send a Get request to retrieve a specific user with userId")]
        public void RetrieveASpecificUserWithUserId()
        {
            var ReqResponse = messageBuilder.WithUrl(readTestDataConfiguration.demoQaUrl() + readTestDataConfiguration.getSpecificUserEndPoint() +
               _scenarioContext.Get<string>("userID")).WithMethod(Method.Get).WithBasicToken(_scenarioContext.Get<string>("UserName"),
               readTestDataConfiguration.getPassword()).Build<GetUserResponseModel>();

            var response = JsonConvert.DeserializeObject<GetUserResponseModel>(ReqResponse.Content);

            var statusCode = (int)ReqResponse.StatusCode;
            _scenarioContext.Add("resp", response);
            _scenarioContext.Add("resCode", ReqResponse.StatusCode);
            _scenarioContext.Add("userInfo", ReqResponse.Content);
        }

        [Then(@"Response statusCode is (.*)")]
        public void ThenResponseStatusCodeIsOK(string espectedStatus)
        {
            var actualStatus = _scenarioContext.Get<HttpStatusCode>("resCode").ToString();
            Assert.IsTrue(espectedStatus.Equals(actualStatus));
        }

        [Then(@"Returned response has correct info")]
        public void ResponseHasCorrectInfo()
        {
            var response = JsonConvert.DeserializeObject<GetUserResponseModel>(_scenarioContext.Get<string>("userInfo").ToString());
            Assert.Multiple(() =>
            {
                Assert.NotNull(response.userId, "userId is null");
                Assert.NotNull(response.username, "username is null");
            });
        }

        [When(@"I send a post request to add book with userId and isbn to users account")]
        public void AddSingeBookWithUserIdAndIsbnNumber()
        {
            string json = "";
            var addListOfBooksModel = new AddListOfBooksModel()
            {
                userId = _scenarioContext.Get<string>("userID"),
                collectionOfIsbns = new List<CollectionOfIsbns>()
                {
                    collectionOfIsbns.AddIsbn(readTestDataConfiguration.getIsbn1())
                }
            };
            json = JsonConvert.SerializeObject(addListOfBooksModel);
            var ReqResponse = messageBuilder.WithUrl(readTestDataConfiguration.demoQaUrl()
                + readTestDataConfiguration.getAddBooksEndpoint()).WithMethod(Method.Post).WithJsonBody(addListOfBooksModel)
                .WithBasicToken(_scenarioContext.Get<string>("UserName"), readTestDataConfiguration.getPassword()).Build<rRoot>();

            var response = JsonConvert.DeserializeObject<rRoot>(ReqResponse.Content).books;

            var statusCode = (int)ReqResponse.StatusCode;
            //_scenarioContext.Add("reResp", ReqResponse);
            _scenarioContext.Add("isbns", ReqResponse.Content);
        }

        [Then(@"Returned response has isbnNumber")]
        public void ResponseHasIsbnNumber()
        {
            var response = JsonConvert.DeserializeObject<rRoot>(_scenarioContext.Get<string>("isbns").ToString());
            Assert.NotNull(response?.books?[0].isbn, "books is null");
            Assert.AreEqual(response?.books?[0].isbn, readTestDataConfiguration.getIsbn1());
        }

        [When(@"I send a Get request to retrieve a specific book with isbnNumber")]
        public void GetSpecificBookWithIsbnNumber()
        {
            var ReqResponse = messageBuilder.WithUrl(readTestDataConfiguration.demoQaUrl() + readTestDataConfiguration.getSingleBookEndpoint() +
              readTestDataConfiguration.getIsbn1()).WithMethod(Method.Get).WithBasicToken(_scenarioContext.Get<string>("UserName"),
              readTestDataConfiguration.getPassword()).Build<GetSpecificBookModel>();

            var response = JsonConvert.DeserializeObject<GetSpecificBookModel>(ReqResponse.Content);

            var statusCode = (int)ReqResponse.StatusCode;
            _scenarioContext.Add("resp", response);
            _scenarioContext.Add("resCode", ReqResponse.StatusCode);
            _scenarioContext.Add("bookInfo", ReqResponse.Content);
        }

        [Then(@"The Status response code is (.*)")]
        public void ThenTheStatusResponseCodeIsOK(string expectedStatus)
        {
            var actualStatus = _scenarioContext.Get<HttpStatusCode>("resCode").ToString();
            //Assert.IsNotNull(resCodeCreated);
            Assert.IsTrue(expectedStatus.Equals(actualStatus));
        }

        [Then(@"Returned response has book info")]
        public void ThenReturnedResponseHasBookInfo()
        {
            var response = JsonConvert.DeserializeObject<GetSpecificBookModel>(_scenarioContext.Get<string>("bookInfo").ToString());
            Assert.Multiple(() =>
            {
                Assert.NotNull(response.isbn, "isbn is null");
                Assert.NotNull(response.title, "title is null");
                Assert.NotNull(response.subTitle, "subTitle is null");
                Assert.NotNull(response.author, "author is null");
                Assert.NotNull(response.publish_date, "publish_date is null");
                Assert.NotNull(response.publisher, "publisher is null");
                Assert.NotNull(response.pages, "pages is null");
                Assert.NotNull(response.description, "description is null");
                Assert.NotNull(response.website, "website is null");
            });
        }
    }
}
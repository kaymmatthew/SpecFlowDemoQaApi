using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecFlowDemoQaApiTest.Models;
using SpecFlowDemoQaApiTest.Support;
using SpecFlowDemoQaApiTest.TestDataConfiguration;
using System;
using System.Net;
using TechTalk.SpecFlow;
using static SpecFlowDemoQaApiTest.Models.CollectionOfIsbns;

namespace SpecFlowDemoQaApiTest.StepDefinitions
{
    [Binding]
    public class AddListOfBooksStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ReadTestDataConfiguration readTestDataConfiguration;
        AddListOfBooksModel addListOfBooksModel;
        CollectionOfIsbns collectionOfIsbns;
        MessageBuilder messageBuilder;
        AddBooksResponseModel addBooksResponseModel;
        public AddListOfBooksStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            readTestDataConfiguration = new ReadTestDataConfiguration();
            addListOfBooksModel = new AddListOfBooksModel();
            collectionOfIsbns = new CollectionOfIsbns(); 
            messageBuilder = new MessageBuilder();
            addBooksResponseModel = new AddBooksResponseModel();
        }

        [When(@"I send a post request to add list of books with userId and isbn to users account")]
        public void userAddListOfBooks()
        {
            string json = "";
            var addListOfBooksModel = new AddListOfBooksModel()
            {
                userId = _scenarioContext.Get<string>("userID"),
                collectionOfIsbns = new List<CollectionOfIsbns>()
                {
                    collectionOfIsbns.AddIsbn(readTestDataConfiguration.getIsbn1()),
                    collectionOfIsbns.AddIsbn(readTestDataConfiguration.getIsbn2()),
                    collectionOfIsbns.AddIsbn(readTestDataConfiguration.getIsbn3()),
                    collectionOfIsbns.AddIsbn(readTestDataConfiguration.getIsbn4()),
                }
            };
            json = JsonConvert.SerializeObject(addListOfBooksModel);
            var ReqResponse = messageBuilder.WithUrl(readTestDataConfiguration.demoQaUrl()
                + readTestDataConfiguration.getAddBooksEndpoint()).WithMethod(Method.Post).WithJsonBody(addListOfBooksModel)
                .WithBasicToken(_scenarioContext.Get<string>("UserName"), readTestDataConfiguration.getPassword()).Build<Root>();

            var response = JsonConvert.DeserializeObject<Root>(ReqResponse.Content);

            var statusCode = (int)ReqResponse.StatusCode;
            _scenarioContext.Add("reResp", ReqResponse);
            _scenarioContext.Add("isbns", response);
         }

        [Then(@"Returned response has the (.*)")]
        public void ReturnedResponseHasTheListOfBooks(string expectedValue)
        {
            var actualValue = _scenarioContext.Get<Root>("isbns");
            foreach (var book in actualValue.books)
            {
                book.Should().NotBeNull();
                Assert.AreEqual(13, actualValue?.books.FirstOrDefault()?.isbn.Length);
                book.isbn.Count().Should().Be(actualValue?.books.FirstOrDefault()?.isbn.Length);
            }
        }
    }
}
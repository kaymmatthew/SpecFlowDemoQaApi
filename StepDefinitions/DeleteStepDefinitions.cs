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
    public class DeleteStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ReadTestDataConfiguration readTestDataConfiguration;
        DeleteUserModel deleteUserModel;
        MessageBuilder messageBuilder;
        DeleteSpecificBookModel deleteSpecificBookModel;
        public DeleteStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            readTestDataConfiguration = new ReadTestDataConfiguration();
            deleteUserModel = new DeleteUserModel();
            messageBuilder = new MessageBuilder();
            deleteSpecificBookModel = new DeleteSpecificBookModel();    
        }
        [When(@"I send a delete request to delete user with userId")]
        public void RequestToDeleteUserWithUserId()
        {
            //string json = "";
            var ReqResponse = messageBuilder.WithUrl(readTestDataConfiguration.demoQaUrl() + readTestDataConfiguration.getDeleteUserEndPoint() +
                _scenarioContext.Get<string>("userID")).WithMethod(Method.Delete).WithBasicToken(_scenarioContext.Get<string>("UserName"),
                readTestDataConfiguration.getPassword()).Build<DeleteUserResponseModel>();
            var statusCode = (int)ReqResponse.StatusCode;
            _scenarioContext.Add("sResponse", ReqResponse.StatusCode);
        }

        [When(@"I send a delete request to delete books with userId")]
        public void DeleteRequestToDeleteBooksWithUserId()
        {
            var ReqResponse = messageBuilder.WithUrl(readTestDataConfiguration.demoQaUrl() + readTestDataConfiguration.getDeleteBooksEndPoint() + 
                _scenarioContext.Get<string>("userID")).WithMethod(Method.Delete).WithBasicToken(_scenarioContext.Get<string>("UserName"),
                readTestDataConfiguration.getPassword()).Build<DeleteBooksModel>();
            var statusCode = (int)ReqResponse.StatusCode;
            _scenarioContext.Add("retResponse", ReqResponse.StatusCode);
        }

        [Then(@"Returned response code is (.*)")]
        public void ResponseCodeIsNoFound(string expectedStatus)
        {
            var actualStatus = _scenarioContext.Get<HttpStatusCode>("retResponse").ToString();
            Assert.IsTrue(expectedStatus.Equals(actualStatus));
        }

        [Then(@"Status response code is (.*)")]
        public void StatusCodeIsNoContent(string expectedStatus)
        {
            var actualStatus = _scenarioContext.Get<HttpStatusCode>("sResponse").ToString();
            Assert.IsTrue(expectedStatus.Equals(actualStatus));
        }

        [When(@"I send a delete request to delete a specific book with isbnNumber and userId")]
        public void RequestToDeleteASpecificBookWithIsbnAndUserId()
        {
            string json = "";
            var deleteBookModel = new DeleteSpecificBookModel()
            {
                isbn = readTestDataConfiguration.getIsbn1(),
                userId = _scenarioContext.Get<string>("userID")
            };
            json = JsonConvert.SerializeObject(deleteBookModel);
            var ReqResponse = messageBuilder.WithUrl(readTestDataConfiguration.demoQaUrl() + readTestDataConfiguration.getDeleteSpecificBookEndPoint())
                .WithMethod(Method.Delete).WithJsonBody(deleteBookModel).WithBasicToken(_scenarioContext.Get<string>("UserName"),
             readTestDataConfiguration.getPassword()).Build<DeleteSpecificBookRespModel>();

            var response = JsonConvert.DeserializeObject<DeleteSpecificBookRespModel>(ReqResponse.Content);

            var statusCode = (int)ReqResponse.StatusCode;
            _scenarioContext.Add("retResponse", ReqResponse.StatusCode);
            _scenarioContext.Add("brResponse", ReqResponse.StatusCode);
            _scenarioContext.Add("deleteResp", ReqResponse.Content);
        }

        [When(@"I send a Get request to retrieve a specific deleted book with isbnNumber")]
        public void RetrieveASpecificDeletedBookWithIsbnNumber()
        {
            var ReqResponse = messageBuilder.WithUrl(readTestDataConfiguration.demoQaUrl() + readTestDataConfiguration.getSingleBookEndpoint() +
              readTestDataConfiguration.getIsbn1()).WithMethod(Method.Get).WithBasicToken(_scenarioContext.Get<string>("UserName"),
              readTestDataConfiguration.getPassword()).Build<GetSpecificBookModel>();
        }

        [Then(@"The status code is (.*)")]
        public void ThenTheStatusCodeIsNotFound(string expectedStatus)
        {
            var actualStatus = _scenarioContext.Get<HttpStatusCode>("brResponse").ToString();
            Assert.IsTrue(expectedStatus.Equals(actualStatus));
        }
    }
}
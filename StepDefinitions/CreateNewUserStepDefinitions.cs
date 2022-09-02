using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecFlowDemoQaApiTest.Models;
using SpecFlowDemoQaApiTest.Support;
using SpecFlowDemoQaApiTest.TestDataConfiguration;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace SpecFlowDemoQaApiTest.StepDefinitions
{
    [Binding]
    public class CreateNewUserStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ReadTestDataConfiguration readTestDataConfiguration;
        CreateNewUserModel createNewUserModel;
        CreateNewUserResponseModel createNewUserResponseModel;
        public CreateNewUserStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            readTestDataConfiguration = new ReadTestDataConfiguration();
            createNewUserModel = new CreateNewUserModel();
            createNewUserResponseModel = new CreateNewUserResponseModel();
        }

        [When(@"I send a request to create new user with username and password")]
        public void CreateNewUserWithUsernameAndPassword()
        {
            string json = "";
            var createNewUserModel = new CreateNewUserModel()
            {
                userName = readTestDataConfiguration.getUserName(),
                password = readTestDataConfiguration.getPassword(),
            };
            json = JsonConvert.SerializeObject(createNewUserModel);
            var requestResponse = ApiMapper.SendRequest(readTestDataConfiguration.demoQaUrl() + readTestDataConfiguration.getCreateNewUserEndPoint(),
               Method.Post, createNewUserModel);
            var statusCode = (int)requestResponse.StatusCode;
            var response = JsonConvert.DeserializeObject<CreateNewUserResponseModel>(requestResponse.Content);
            _scenarioContext.Add("response", response);
            _scenarioContext.Add("Created", requestResponse.StatusCode);
            _scenarioContext.Add("userID", response.userID);
            _scenarioContext.Add("UserName", response.username);
        }

        [Then(@"The response code is (.*)")]
        public void ThenResponseIsCreated(string resCodeCreated)
        {
            resCodeCreated = _scenarioContext.Get<HttpStatusCode>(resCodeCreated).ToString();
            Assert.IsNotNull(resCodeCreated);
            Assert.IsTrue(resCodeCreated.Equals(resCodeCreated));
        }
        [Then(@"The returned response has the right user info")]
        public void ReturnedResponseHasTheRightUserInfo()
        {
            var response = _scenarioContext.Get<CreateNewUserResponseModel>("response");
            //var Gresponse = JsonConvert.DeserializeObject<CreateNewUserResponseModel>(response);
            Assert.Multiple(() =>
            {
                Assert.NotNull(response.userID, "userId is null");
                Assert.NotNull(response.username, "username is null");
                Assert.IsTrue(response?.userID?.Length.Equals(36), $"userId is {response?.userID?.Length} in length");
            });
        }
    }
}
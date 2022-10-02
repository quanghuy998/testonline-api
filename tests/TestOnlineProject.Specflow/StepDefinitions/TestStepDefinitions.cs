//using Newtonsoft.Json;
//using System.Net.Http.Headers;
//using TestOnlineProject.API;
//using TestOnlineProject.Domain.Aggregates.TestAggregate;
//using TestOnlineProject.Specflow.Core;
//using static TestOnlineProject.API.Dtos.TestRequest;

//namespace TestOnlineProject.Specflow.StepDefinitions
//{
//    [Binding]
//    internal class TestStepDefinitions : IClassFixture<CustomWebApplicationFactory<Startup>>
//    {
//        private readonly HttpClient _client;
//        private HttpResponseMessage _response;
//        private readonly ScenarioContext _scenarioContext;

//        public TestStepDefinitions(CustomWebApplicationFactory<Startup> factory, ScenarioContext scenarioContext)
//        {
//            _client = factory.CreateClient();
//            _scenarioContext = scenarioContext;
//        }

//        [Given(@"The test repository already exists the following tests")]
//        public async void GivenTheTestRepositoryAlreadyExistsTheFollowingTests(Table table)
//        {
//            foreach (var row in table.Rows)
//            {
//                var request = new CreateTestRequest(row[0]);

//                string json = JsonConvert.SerializeObject(request);
//                var httpContent = new StringContent(json);
//                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//                var response = await _client.PostAsync("/api/tests", httpContent);
//            };

//            _scenarioContext.Pending();
//        }

//        [When(@"Admin wants to get all tests")]
//        public async void WhenAdminWantsToGetAllTests()
//        {
//            _response = await _client.GetAsync("/api/tests");
//            _scenarioContext.Pending();
//        }

//        [Then(@"The test repository should return the following tests")]
//        public void ThenTheTestRepositoryShouldReturnTheFollowingTests(Table table)
//        {
//            var expectedTestList = new List<Test>();
//            foreach (var row in table.Rows)
//            {
//                var test = new Test(row[1]);
//                expectedTestList.Add(test);
//            }

//            var result = _response.Content.ReadAsStringAsync().Result;
//            var actualTestList = JsonConvert.DeserializeObject<List<Test>>(result);

//            actualTestList.Count.Should().Be(expectedTestList.Count);
//            actualTestList[0].Title.Should().Be(expectedTestList[0].Title);
//            actualTestList[1].Title.Should().Be(expectedTestList[1].Title);
//            _scenarioContext.Pending();
//        }
//    }
//}

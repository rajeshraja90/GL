using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectManagement.Entities;
using Xunit;

namespace ProjectManagement.Api.Controllers.Test
{
    public class ProjectControllerTest : IClassFixture<TestingFactory<Startup>>
    {

        private readonly HttpClient _client;
        public ProjectControllerTest(TestingFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task ReturnUserForm()
        {
            var response = await _client.GetAsync("/api/User");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("rajesh", responseString);
            Assert.Contains("sam", responseString);
        }
        [Fact]
        public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/User/");

           
            var userModel = new Dictionary<string, string>
            {
                { "ID" , "10" },
                { "FirstName" , "david" },
                { "LastName" , "kahn" },
                { "Email" , "david@global.com"},
                { "Password" , "kahn" }
            };

            User postquote = new User()
            {               
                FirstName = "david" ,
               LastName = "kahn" ,
                Email = "david@global.com",
                Password = "kahn"               
            };
            string json = JsonConvert.SerializeObject(postquote);           

           // postRequest.Content = new FormUrlEncodedContent(userModel);
            postRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");          

           //ostRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            var response = await _client.SendAsync(postRequest);

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseString = await response.Content.ReadAsStringAsync();

           //var statusCheck = JsonConvert.DeserializeObject<User>(responseString);
            response.EnsureSuccessStatusCode();

            Assert.Contains("david", responseString);
        }
    }

}

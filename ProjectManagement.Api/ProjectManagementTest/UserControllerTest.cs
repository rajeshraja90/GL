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
    public class UserControllerTest : IClassFixture<TestingFactory<Startup>>
    {

        private readonly HttpClient _client;
        public UserControllerTest(TestingFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task TestUserGet()
        {
            var response = await _client.GetAsync("/api/User");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("rajesh", responseString);
            Assert.Contains("sam", responseString);
        }
        [Fact]
        public async Task TestUserPost()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/User/");

            User postquote = new User()
            {               
                FirstName = "david" ,
               LastName = "kahn" ,
                Email = "david@global.com",
                Password = "kahn"               
            };
            string jsonUser = JsonConvert.SerializeObject(postquote);    

            postRequest.Content = new StringContent(jsonUser, Encoding.UTF8, "application/json");  
            
            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();            

            Assert.Contains("david", responseString);
        }
    }

}

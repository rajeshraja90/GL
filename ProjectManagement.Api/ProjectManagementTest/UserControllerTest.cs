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
        public async Task TestGetUser()
        {
            var response = await _client.GetAsync("/api/User");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("rajesh", responseString);
            Assert.Contains("sam", responseString);
        }
        [Fact]
        public async Task TestGetUserById()
        {
            var response = await _client.GetAsync("/api/User/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();           
            Assert.Contains("rajesh", responseString);
        }
        [Fact]
        public async Task TestPostUser()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/User/");

            User postUser = new User()
            {               
                FirstName = "david" ,
                LastName = "kahn" ,
                Email = "david@global.com",
                Password = "kahn"               
            };
            string jsonUser = JsonConvert.SerializeObject(postUser);    

            postRequest.Content = new StringContent(jsonUser, Encoding.UTF8, "application/json");  
            
            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();            

            Assert.Contains("david", responseString);
        }

        [Fact]
        public async Task TestPutUser()
        {
            var putRequest = new HttpRequestMessage(HttpMethod.Put, "/api/User/");

            User putUser = new User()
            {
                FirstName = "Sam",
                LastName = "Saran",
                Email = "sam@global.com",
            };
            string jsonUser = JsonConvert.SerializeObject(putUser);

            putRequest.Content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(putRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Sam", responseString);
        }
        [Fact]
        public async Task TestDeleteUser()
        {
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, "/api/User/Delete/2");

            var response = await _client.SendAsync(deleteRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Deleted Successfully", responseString);
        }
    }

}

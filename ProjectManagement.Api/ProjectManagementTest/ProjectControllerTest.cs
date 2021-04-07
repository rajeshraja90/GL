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
        public async Task TestGetProject()
        {
            var response = await _client.GetAsync("/api/Project");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Project1", responseString);
            Assert.Contains("Project2", responseString);
        }
        [Fact]
        public async Task TestGetProjectbyId()
        {
            var response = await _client.GetAsync("/api/Project/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Project1", responseString);
        }
        [Fact]
        public async Task TestPostProject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Project/");

            Project postProject = new Project()
            {
                Name = "Project3",
                CreatedOn = DateTime.Now,
                Detail = "This is project 3"
            };
            string jsonProject = JsonConvert.SerializeObject(postProject);    

            postRequest.Content = new StringContent(jsonProject, Encoding.UTF8, "application/json");  
            
            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();            

            Assert.Contains("Project3", responseString);
        }

        [Fact]
        public async Task TestPutProject()
        {
            var putRequest = new HttpRequestMessage(HttpMethod.Put, "/api/Project/");

            Project putProject = new Project()
            {
                Name = "Project2",
                CreatedOn = DateTime.Now,
                Detail = "This is project 2 Updated"
            };
            string jsonProject = JsonConvert.SerializeObject(putProject);

            putRequest.Content = new StringContent(jsonProject, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(putRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Project2", responseString);
        }
        [Fact]
        public async Task TestDeleteProject()
        {
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, "/api/Project/Delete/2");

            var response = await _client.SendAsync(deleteRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Deleted Successfully", responseString);
        }
    }

}

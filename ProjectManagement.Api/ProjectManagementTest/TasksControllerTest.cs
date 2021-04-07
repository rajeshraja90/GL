using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectManagement.Entities;
using Xunit;

namespace ProjectManagement.Api.Controllers.Test
{
    public class TasksControllerTest : IClassFixture<TestingFactory<Startup>>
    {

        private readonly HttpClient _client;
        public TasksControllerTest(TestingFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task TestGetTasks()
        {
            var response = await _client.GetAsync("/api/Tasks");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Task 1", responseString);
            Assert.Contains("Test Task 2", responseString);
        }
        [Fact]
        public async Task TestGetTasksbyId()
        {
            var response = await _client.GetAsync("/api/Tasks/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Task 1", responseString);
        }
        [Fact]
        public async Task TestPostTasks()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Tasks/");

            Tasks postTasks = new Tasks()
            {
                Detail = "Test Task 3",
                CreatedOn = DateTime.Now,
                Status = ProjectManagement.Entities.Enums.TaskStatus.New
            };
            string jsonTasks = JsonConvert.SerializeObject(postTasks);    

            postRequest.Content = new StringContent(jsonTasks, Encoding.UTF8, "application/json");  
            
            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();            

            Assert.Contains("Test Task 3", responseString);
        }

        [Fact]
        public async Task TestPutTasks()
        {
            var putRequest = new HttpRequestMessage(HttpMethod.Put, "/api/Tasks/");

            Tasks putTasks = new Tasks()
            {
                Detail = "Test Task 2",
                Status = ProjectManagement.Entities.Enums.TaskStatus.InProgress
            };
            string jsonTasks = JsonConvert.SerializeObject(putTasks);

            putRequest.Content = new StringContent(jsonTasks, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(putRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Test Task 2", responseString);
        }
        [Fact]
        public async Task TestDeleteTasks()
        {
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, "/api/Tasks/Delete/2");

            var response = await _client.SendAsync(deleteRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Deleted Successfully", responseString);
        }
    }

}

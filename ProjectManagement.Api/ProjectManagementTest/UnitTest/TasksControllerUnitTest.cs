using Moq;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManagement.Api.Controllers.Test
{
    public class TasksControllerUnitTest
    {
        private FakeDbSet<Tasks> tasks;
        Mock<IBaseRepository<Tasks>> mockObject;

        public TasksControllerUnitTest()
        {
            mockObject = new Mock<IBaseRepository<Tasks>>();
            var testTasks1 = new Tasks { ID=10, Detail = "Test Task 1", CreatedOn = DateTime.Now, Status = Entities.Enums.TaskStatus.New };
            mockObject.Object.Add(testTasks1);
        }

        [Fact]
        public void TestGet()
        {
            tasks = new FakeDbSet<Tasks>();
            var testTasks1 = new Tasks { Detail = "Test Task 1", CreatedOn = DateTime.Now, Status = Entities.Enums.TaskStatus.New };
            var testTasks2 = new Tasks { Detail = "Test Task 2", CreatedOn = DateTime.Now, Status = Entities.Enums.TaskStatus.New };
            tasks.Add(testTasks1);
            tasks.Add(testTasks2);

            mockObject.Setup(m => m.Get()).Returns(tasks);
            TasksController tasksController = new TasksController(mockObject.Object);
            var result = tasksController.Get();
            Assert.NotNull(result);
        }
        [Fact]
        public void TestGetById()
        {
            var testTasks1 = new Tasks { ID=1, Detail = "Test Task 1", CreatedOn = DateTime.Now, Status = Entities.Enums.TaskStatus.New };

            mockObject.Setup(m => m.Get(1)).Returns(testTasks1);
            TasksController tasksController = new TasksController(mockObject.Object);
            var result = tasksController.Get(1);
            Assert.NotNull(result);
            var resultFail = tasksController.Get(2);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(resultFail);
        }
        [Fact]
        public void TestPost()
        {
            var tasksAdd = new Tasks { Detail = "Test Task 1", CreatedOn = DateTime.Now, Status = Entities.Enums.TaskStatus.New };

            mockObject.Setup(m => m.Add(tasksAdd)).Returns(() => Task<Tasks>.FromResult(tasksAdd));
            TasksController tasksController = new TasksController(mockObject.Object);
            var result = tasksController.Add(tasksAdd);
            Assert.NotNull(result);
        }

        [Fact]
        public void TestPut()
        {
            var tasksUpdate = new Tasks { Detail = "Test Task 1", CreatedOn = DateTime.Now, Status = Entities.Enums.TaskStatus.New };

            mockObject.Setup(m => m.Update(tasksUpdate)).Returns(() => Task<Tasks>.FromResult(tasksUpdate));
            TasksController tasksController = new TasksController(mockObject.Object);
            var result = tasksController.Put(tasksUpdate);
            Assert.NotNull(result);
        }

        [Fact]
        public void TestDelete()
        {  
            mockObject.Setup(m => m.Delete(1)).Returns(() => Task<int>.FromResult(1));
            TasksController tasksController = new TasksController(mockObject.Object);
            var result = tasksController.Delete(1);
            Assert.NotNull(result);
        }

    }
}
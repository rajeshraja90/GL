using Moq;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManagement.Api.Controllers.Test
{
    public class UserControllerUnitTest
    {
        private FakeDbSet<User> Users;
        Mock<IBaseRepository<User>> mockObject;

        public UserControllerUnitTest()
        {
            mockObject = new Mock<IBaseRepository<User>>();
        }

        [Fact]
        public void TestGet()
        {
            Users = new FakeDbSet<User>();
            var testUser1 = new User
            {
                FirstName = "Rajesh",
                LastName = "Kumar",
                Email = "rajesh@global.com",
                Password = "rajesh",
                ID = 1
            };
            Users.Add(testUser1);
            var testUser2 = new User
            {
                FirstName = "Sam",

                LastName = "Samir",
                Email = "sam@global.com",
                ID = 2
            };
            Users.Add(testUser2);

            mockObject.Setup(m => m.Get()).Returns(Users);
            UserController userController = new UserController(mockObject.Object);
            var result = userController.Get();
            Assert.NotNull(result);
        }
        [Fact]
        public void TestGetById()
        {
            var testUser1 = new User
            {
                FirstName = "Rajesh",
                LastName = "Kumar",
                Email = "rajesh@global.com",
                Password = "rajesh",
                ID = 1
            };

            mockObject.Setup(m => m.Get(1)).Returns(testUser1);
            UserController userController = new UserController(mockObject.Object);
            var result = userController.Get(1);
            Assert.NotNull(result);
            var resultFail = userController.Get(2);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(resultFail);
        }
        [Fact]
        public void TestPost()
        {
            var userAdd = new User
            {
                FirstName = "RajeshAdd",
                LastName = "Kumar",
                Email = "rajesh@global.com",
                Password = "rajesh"
            };
            mockObject.Setup(m => m.Add(userAdd)).Returns(() => Task<User>.FromResult(userAdd));
            UserController userController = new UserController(mockObject.Object);
            var result = userController.Add(userAdd);
            Assert.NotNull(result);
        }
        [Fact]
        public void TestPut()
        {
            var user = new User
            {
                FirstName = "karim",
                LastName = "David",
                Email = "karim@global.com",
                Password = "Karim"
            };

            mockObject.Setup(m => m.Update(user)).Returns(() => Task<Project>.FromResult(user));
            UserController userController = new UserController(mockObject.Object);
            var result = userController.Put(user);
            Assert.NotNull(result);
        }

        [Fact]
        public void TestDelete()
        {
            mockObject.Setup(m => m.Delete(1)).Returns(() => Task<int>.FromResult(1));
            UserController userController = new UserController(mockObject.Object);
            var result = userController.Delete(1);
            Assert.NotNull(result);
        }

    }
}
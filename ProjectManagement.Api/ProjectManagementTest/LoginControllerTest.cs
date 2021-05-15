using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagement.Data;
using ProjectManagement.Data.Implementation;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using System;
using Xunit;
using System.Linq;

namespace ProjectManagement.Api.Controllers.Test
{
    public class LoginControllerTest 
    {
        ILoginRepository loginRepository;
        public LoginControllerTest()
        {
             loginRepository = MockData();
        }
        private ILoginRepository MockData()
        {
            User user = new User() {FirstName = "rajesh", LastName = "Kumar",Email="test@test.com" };
            Mock<ILoginRepository> mockObject = new Mock<ILoginRepository>();
            object p = mockObject.Setup(m => m.Login("rajesh","password")).Returns(user);
            return mockObject.Object;
        }
        [Fact]
        public void TestLoginSuccess()
        { 
            LoginController loginController = new LoginController(loginRepository);
            var result = loginController.Login("rajesh", "password");
            Assert.NotNull(result);
        }
        [Fact]
        public void TestLoginFailure()
        { 
            LoginController loginController = new LoginController(loginRepository);
            var result = loginController.Login("kumar", "password");
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);

        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
       
        public ActionResult Login(string userName, string password)
        {
            var result = _loginRepository.Login(userName,password);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}

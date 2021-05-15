using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProjectManagement.Data.Implementation
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ProjectManagementContext _projectManagementContext;

        public LoginRepository(ProjectManagementContext projectManagementContext)
        {
            this._projectManagementContext = projectManagementContext;
        }
        public User Login(string userName, string password)
        {
            return _projectManagementContext.Users.Where(i => i.Email==userName && i.Password==password).FirstOrDefault();
        }
    }
}

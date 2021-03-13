using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Data.Interfaces
{
    public interface ILoginRepository
    {
        User Login(string UserName, string password);
    }
}

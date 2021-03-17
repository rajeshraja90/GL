using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;
using System;

namespace ProjectManagement.Data
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext(DbContextOptions options) : base(options)
        {

        }

        public void AddInitialData()
        {
            var testUser1 = new User
            {
                FirstName = "Rajesh",
                LastName = "Kumar",
                Email = "rajesh@global.com",
                Password="rajesh"
            };
            Users.Add(testUser1);
            var testUser2 = new User
            {
                FirstName = "Sam",
                LastName = "Samir",
                Email = "sam@global.com"
            };
            Users.Add(testUser2);

            Project testProject1 = new Project { Name = "TestProject1", CreatedOn = DateTime.Now, Detail = "This is Test project 1" };
            Project testProject2 = new Project { Name = "TestProject2", CreatedOn = DateTime.Now, Detail = "This is Test project 2" };

            Project.Add(testProject1);
            Project.Add(testProject2);
            this.SaveChanges();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Tasks> Task { get; set; }
    }
}

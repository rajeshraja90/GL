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
                Email = "sam@global.com",
            };
            Users.Add(testUser2);

            var testProject1 = new Project { Name = "Project1", CreatedOn = DateTime.Now, Detail = "This is project 1" };
            var testProject2 = new Project { Name = "Project2", CreatedOn = DateTime.Now, Detail = "This is project 2" };

            Project.Add(testProject1);
            Project.Add(testProject2);

            var testTask = new Tasks { ProjectID = 1, AssignedToUserID = 1, CreatedOn = DateTime.Now };
            Task.Add(testTask);

            this.SaveChanges();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Tasks> Task { get; set; }
    }
}

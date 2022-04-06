using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Models;
using System;

namespace ProjectManagement.DataAccessLayer
{
    public class InMemoryDataProvider
    {
        public static void InitDataProvider(IServiceProvider serviceProvider)
        {
            using (var context = new ProjectManagementDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<ProjectManagementDBContext>>()))
            { 
                context.Database.EnsureCreated();

                // Adding Tasks
                context.Tasks.AddRange(new Task { Id = 1, ProjectId = 1, Detail = "Inmemory Project1 User1 Task1 Detail", AssignedToUserId = 1, Status = 1, CreatedOn = DateTime.Now },
                                       new Task { Id = 2, ProjectId = 1, Detail = "Inmemory Project1 User1 Task2 Detail", AssignedToUserId = 1, Status = 2, CreatedOn = DateTime.Now },
                                       new Task { Id = 3, ProjectId = 2, Detail = "Inmemory Project1 User2 Task1 Detail", AssignedToUserId = 2, Status = 1, CreatedOn = DateTime.Now });

                // Adding Projects
                context.Projects.AddRange( new Project { Id = 1, Name = "Inmemory Project1", Detail = "Project1 Details", CreatedOn = DateTime.Now },
                                            new Project { Id = 2, Name = "Inmemory Project2", Detail = "Project2 Details", CreatedOn = DateTime.Now },
                                            new Project { Id = 3, Name = "Inmemory Project3", Detail = "Project3 Details", CreatedOn = DateTime.Now });

                // Adding Users
                context.Users.AddRange(
                    new User { Id = 1, FirstName = "UserF1", LastName = "Inmemory  UserL1", Email = "User1@gmail.com", Password = "password1234" },
                    new User { Id = 2, FirstName = "UserF2", LastName = "Inmemory  UserL2", Email = "User2@gmail.com", Password = "password1234" },
                    new User { Id = 3, FirstName = "UserF3", LastName = "Inmemory  UserL3", Email = "User3@gmail.com", Password = "password1234" }
                    );

                context.SaveChanges();
            }
            
        }
    }
}

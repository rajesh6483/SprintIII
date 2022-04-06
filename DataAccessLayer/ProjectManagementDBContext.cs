
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.DataAccessLayer
{
    public class ProjectManagementDBContext : DbContext
    {
        public ProjectManagementDBContext(DbContextOptions<ProjectManagementDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}

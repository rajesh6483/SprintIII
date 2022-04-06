using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Models;

namespace ProjectManagement.Interfaces
{
     public interface IProject
    {
        IList<Project> GetAllProjects();

        IList<Project> GetAllProjectsWithTasks();
        Project GetProjectById(int id);
        Project Create(Project project);
        Project Update(Project project);
    }
}

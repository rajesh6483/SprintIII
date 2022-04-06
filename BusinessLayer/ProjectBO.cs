using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.DataAccessLayer;
using System.Threading.Tasks;
using ProjectManagement.Interfaces;
using ProjectManagement.Models;

namespace ProjectManagement.BusinessLayer
{
    public class ProjectBO :IProject
    {
        private ProjectDAO  _projectDAO;

        public ProjectBO(ProjectDAO projectDAO)
        {
            _projectDAO = projectDAO;
        }
        
        public Project Create(Project project)
        {
          return  _projectDAO.Create(project);
        }

        public IList<Project> GetAllProjects()
        {
            return _projectDAO.GetAllProjects();
        }
        public IList<Project> GetAllProjectsWithTasks()
        {
            return _projectDAO.GetAllProjectsWithTasks();
        }

        public Project GetProjectById(int id)
        {
            return _projectDAO.GetProjectById(id);
        }

        public Project Update(Project project)
        {
            return _projectDAO.Update(project);
        }
    }
}

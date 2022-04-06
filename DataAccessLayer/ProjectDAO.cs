using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Models;
using ProjectManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.DataAccessLayer
{
    public class ProjectDAO : IProject
    {
        private readonly ProjectManagementDBContext _context;
        private int _count;

        public ProjectDAO(ProjectManagementDBContext context)
        {
            _context = context;
            _count = _context.Projects.Count();
        }
        public Project Create(Project project)
        {
            project.Id = ++_count;
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }

        public IList<Project> GetAllProjects()
        {
           return _context.Projects.ToList();
        }

        public IList<Project> GetAllProjectsWithTasks()
        {
            return  _context.Projects.Include(project => project.Tasks).ToList();
        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.Include(project => project.Tasks).FirstOrDefault(x => x.Id == id);
        }

        public Project Update(Project project)
        {
            if (Delete(project.Id))
            {
                Create(project);
                _context.SaveChanges();
                return project;
            }
            throw new Exception($"No Tasjk Exists with Id {project.Id}");
        }

        public bool Delete(int projectId)
        {
            var userToDelete = _context.Projects.Where(user => user.Id == projectId).FirstOrDefault();
            if (userToDelete != null)
            {
                _context.Projects.Remove(userToDelete);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}

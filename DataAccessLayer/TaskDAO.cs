using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Models;
using ProjectManagement.Interfaces;

namespace ProjectManagement.DataAccessLayer
{
    public class TaskDAO : ITask
    {
        private readonly ProjectManagementDBContext _context;
        private int _count;

        public TaskDAO(ProjectManagementDBContext context)
        {
            _context = context;
             _count = _context.Tasks.Count();
        }
        public Task Create(Task task)
        {
            task.Id = ++_count;
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public IList<Task> GetAllTasks()
        {
            return _context.Tasks.ToList();

        }

        public Task GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(x => x.Id == id);
        }

        public IList<Task> GetTasksByUserID(int userId)
        {
            return _context.Tasks.Where(task=> task.AssignedToUserId== userId).ToList();
        }

        public Task Update(Task task)
        {
            if (Delete(task.Id))
            {
                Create(task);
                _context.SaveChanges();
                return task;
            }
            throw new Exception($"No Tasjk Exists with Id {task.Id}");
        }

        public bool Delete(int taskID)
        {
            var userToDelete = _context.Tasks.Where(user => user.Id == taskID).FirstOrDefault();
            if (userToDelete != null)
            {
                _context.Tasks.Remove(userToDelete);
                _context.SaveChanges();
                return true;
            }
            else return false;

        }
    }
}

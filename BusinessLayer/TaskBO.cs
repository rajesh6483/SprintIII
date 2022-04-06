using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Interfaces;
using ProjectManagement.Models;
using ProjectManagement.DataAccessLayer;


namespace ProjectManagement.BusinessLayer
{
    public class TaskBO :ITask
    {
        private readonly TaskDAO _taskDAO;
       
        public TaskBO(TaskDAO dao)
        {
            _taskDAO = dao;
        }
        public Task Create(Task task)
        {
            _taskDAO.Create(task);
            return task;
        }

        public IList<Task> GetAllTasks()
        {
            return _taskDAO.GetAllTasks();
        }

        public Task GetTaskById(int id)
        {
            return _taskDAO.GetTaskById(id);
        }

        public IList<Task> GetTasksByUserID(int userId)
        {
            return _taskDAO.GetTasksByUserID(userId);
        }
        
        public Task Update(Task task)
        {
            return _taskDAO.Update(task);
        }
       
    }
}

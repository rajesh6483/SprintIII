using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Models;

namespace ProjectManagement.Interfaces
{
    public interface ITask
    {
        IList<Task> GetAllTasks();
        Task GetTaskById(int id);
        Task Create(Task Task);
        Task Update(Task Task);
        IList<Task> GetTasksByUserID(int id); 
    }
}

using System;
using ProjectManagement.Controllers;
using ProjectManagement.Models;
using ProjectManagement.Interfaces;

using ProjectManagement.BusinessLayer;
using ProjectManagement.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;

namespace ProjectManagementTests
{
    public class UnitTestHelper
    {
        private static IList<Task> _taskList;
        static UnitTestHelper()
        {
            _taskList = new List<Task> { new Task() { Id = 1, ProjectId = 1, AssignedToUserId = 1, Detail = "detail1", CreatedOn = DateTime.Now, Status = 0} ,
            new Task() { Id = 2, ProjectId = 1, AssignedToUserId = 2, Detail = "detail2", CreatedOn = DateTime.Now, Status = 1 } };
        }
        public static Task GetSingleTask()
        {
            return _taskList[0];
        }

        public static IList<Task> GetTasks()
        {
            return _taskList;
        }
    }
}

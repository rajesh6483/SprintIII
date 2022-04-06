using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ProjectManagement.Interfaces;

using ProjectManagement.Models;
using ProjectManagement.Controllers;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ProjectManagementTests.ControllerTests
{
    public class TaskControllerTest
    {
        [Fact]
        public void GetTest()
        {
            //Arrange
            var taskRepoMock = new Mock<ITask>();
            var tasksList = UnitTestHelper.GetTasks();
            taskRepoMock.Setup(x => x.GetAllTasks()).Returns(tasksList);
            var userRepoMock = new Mock<IUser>();
            var taskController = new TaskController(taskRepoMock.Object, userRepoMock.Object);
            //Act
            OkObjectResult result = (OkObjectResult)taskController.Get();
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(2, ((IList)result.Value).Count);
        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var taskRepoMock = new Mock<ITask>();
            var task = UnitTestHelper.GetSingleTask();
            taskRepoMock.Setup(x => x.GetTaskById(1)).Returns(task);
            var userRepoMock = new Mock<IUser>();
            var taskController = new TaskController(taskRepoMock.Object, userRepoMock.Object);
            //Act
            OkObjectResult result = (OkObjectResult)taskController.Get(1);
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(task.Id, ((Task)result.Value).Id);
        }

        [Fact]
        public void GetByIdNotFoundTest()
        {
            //Arrange
            var taskRepoMock = new Mock<ITask>();
            var userRepoMock = new Mock<IUser>();
            var taskController = new TaskController(taskRepoMock.Object, userRepoMock.Object);
            //Act
            ActionResult result = taskController.Get(0);
            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }



        [Fact]
        public void PostTest()
        {
            var taskRepoMock = new Mock<ITask>();
            var task = UnitTestHelper.GetSingleTask();
            var userRepoMock = new Mock<IUser>();
            var taskController = new TaskController(taskRepoMock.Object, userRepoMock.Object); 
            taskController.ControllerContext = new ControllerContext();
            taskController.ControllerContext.HttpContext = new DefaultHttpContext();
            taskController.ControllerContext.HttpContext.Request.Headers["device-id"] = "20317";
            taskController.ControllerContext.HttpContext.Request.Scheme = "http";
            taskController.ControllerContext.HttpContext.Request.Host = new HostString("localhost:9090");
            taskController.ControllerContext.HttpContext.Request.Path = new PathString();
            taskRepoMock.Setup(x => x.Create(task)).Returns(task);

            //Act
            CreatedResult result = (CreatedResult)taskController.Post(task);

            //Assert
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(task.Id, ((Task)result.Value).Id);
        }

        [Fact]
        public void PostBadRequestTest()
        {
            var taskRepoMock = new Mock<ITask>();
            Task task = null;
            var userRepoMock = new Mock<IUser>();
            var taskController = new TaskController(taskRepoMock.Object, userRepoMock.Object);

            //Act
            ActionResult result = taskController.Post(task);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PutBadRequestTest()
        {
            var taskRepoMock = new Mock<ITask>();
            var task = UnitTestHelper.GetSingleTask();
            var userRepoMock = new Mock<IUser>();
            var taskController = new TaskController(taskRepoMock.Object, userRepoMock.Object);

            //Act
            ActionResult result = taskController.Put(task);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutTest()
        {
            var taskRepoMock = new Mock<ITask>();
            var task = UnitTestHelper.GetSingleTask();
            var userRepoMock = new Mock<IUser>();
            var taskController = new TaskController(taskRepoMock.Object, userRepoMock.Object);
            taskRepoMock.Setup(x => x.Update(task)).Returns(task);

            //Act
            OkObjectResult result = (OkObjectResult)taskController.Put(task);

            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(task.Id, ((Task)result.Value).Id);
        }
    }
}

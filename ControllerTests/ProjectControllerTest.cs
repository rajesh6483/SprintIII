using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagement.Interfaces;

using ProjectManagement.Models;
using ProjectManagement.Controllers;
using Xunit;

namespace ProjectManagementTests.ControllerTests
{
    public class ProjectControllerTest
    {
        [Fact]
        public void GetTest()
        {  
            //Arrange
            var projRepoMock = new Mock<IProject>();
            var projectsList = new List<Project>() { new Project() { Id=1,Name="Project1",Detail="Detail1", CreatedOn= DateTime.Now} ,
            new Project() { Id=2,Name="Project2",Detail="Detail2", CreatedOn= DateTime.Now}};
            projRepoMock.Setup(x => x.GetAllProjects()).Returns(projectsList);
            var projectController = new ProjectController(projRepoMock.Object);
            //Act
            OkObjectResult result= (OkObjectResult)projectController.Get();
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(2, ((IList)result.Value).Count);
        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var projRepoMock = new Mock<IProject>();
            var project = new Project() { Id = 1, Name = "Project1", Detail = "Detail1", CreatedOn = DateTime.Now }; 
            
            projRepoMock.Setup(x => x.GetProjectById(1)).Returns(project);
            var projectController = new ProjectController(projRepoMock.Object);
            //Act
            OkObjectResult result = (OkObjectResult)projectController.Get(1);
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(project.Id, ((Project)result.Value).Id);
        }

        [Fact]
        public void GetByIdNotFoundTest()
        {
            //Arrange
            var projRepoMock = new Mock<IProject>();
            var projectController = new ProjectController(projRepoMock.Object);
            //Act
            ActionResult result = projectController.Get(0);
            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void PostTest()
        {
            var projRepoMock = new Mock<IProject>();
            var project = new Project() { Id = 1, Name = "Project1", Detail = "Detail1", CreatedOn = DateTime.Now };
            var projectController = new ProjectController(projRepoMock.Object);
            projectController.ControllerContext = new ControllerContext();
            projectController.ControllerContext.HttpContext = new DefaultHttpContext();
            projectController.ControllerContext.HttpContext.Request.Headers["device-id"] = "20317";
            projectController.ControllerContext.HttpContext.Request.Scheme = "http";
            projectController.ControllerContext.HttpContext.Request.Host = new HostString("localhost:9090");
            projectController.ControllerContext.HttpContext.Request.Path = new PathString();
            projRepoMock.Setup(x => x.Create(project)).Returns(project);
            
            //Act
            CreatedResult result = (CreatedResult)projectController.Post(project);

            //Assert
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(project.Id, ((Project)result.Value).Id);
        }

        [Fact]
        public void PostBadRequestTest()
        {
            var projRepoMock = new Mock<IProject>();
            var project = new Project() { Id = 1, Name = "Project1", Detail = "Detail1", CreatedOn = DateTime.Now };
            var projectController = new ProjectController(projRepoMock.Object);          

            //Act
            ActionResult result = projectController.Post(project);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PutBadRequestTest()
        {
            var projRepoMock = new Mock<IProject>();
            var project = new Project() { Id = 1, Name = "Project1", Detail = "Detail1", CreatedOn = DateTime.Now };
            var projectController = new ProjectController(projRepoMock.Object);

            //Act
            ActionResult result = projectController.Put(project);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutTest()
        {
            var projRepoMock = new Mock<IProject>();
            var project = new Project() { Id = 1, Name = "Project1", Detail = "Detail1", CreatedOn = DateTime.Now };
            var projectController = new ProjectController(projRepoMock.Object);
            projRepoMock.Setup(x => x.Update(project)).Returns(project);
            //Act
            OkObjectResult result = (OkObjectResult)projectController.Put(project);

            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(project.Id, ((Project)result.Value).Id);
        }
    }
}

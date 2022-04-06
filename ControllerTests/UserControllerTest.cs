using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class UserControllerTest
    {
        [Fact]
        public void GetTest()
        {
            //Arrange
            var userRepoMock = new Mock<IUser>();
            var usersList = new List<User>() { new User() { Id =1, FirstName = "firstname1", LastName= "lastname1", Email ="f1@gmail.com", Password="1234"} ,
            new User() {Id =2, FirstName = "firstname2", LastName= "lastname2", Email ="f2@gmail.com", Password="1234"}};
            userRepoMock.Setup(x => x.GetAllUsers()).Returns(usersList);
            var userController = new UserController(userRepoMock.Object);
            //Act
            OkObjectResult result = (OkObjectResult)userController.Get();
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(2, ((IList)result.Value).Count);
        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var userRepoMock = new Mock<IUser>();
            var user = new User() { Id = 1, FirstName = "firstname1", LastName = "lastname1", Email = "f1@gmail.com", Password = "1234" };
            userRepoMock.Setup(x => x.GetUserById(1)).Returns(user);
            var userController = new UserController(userRepoMock.Object);
            //Act
            OkObjectResult result = (OkObjectResult)userController.Get(1);
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(user.Id, ((User)result.Value).Id);
        }

        [Fact]
        public void GetByIdNotFoundTest()
        {
            //Arrange
            var userRepoMock = new Mock<IUser>();
            var userController = new UserController(userRepoMock.Object);
            //Act
            ActionResult result = userController.Get(1);
            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void PostTest()
        {
            //Arrange
            var userRepoMock = new Mock<IUser>();
            var user = new User() { Id = 1, FirstName = "firstname1", LastName = "lastname1", Email = "f1@gmail.com", Password = "1234" };
            var userController = new UserController(userRepoMock.Object);
            userController.ControllerContext = new ControllerContext();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            userController.ControllerContext.HttpContext.Request.Headers["device-id"] = "20317";
            userController.ControllerContext.HttpContext.Request.Scheme = "http";
            userController.ControllerContext.HttpContext.Request.Host = new HostString("localhost:9090");
            userController.ControllerContext.HttpContext.Request.Path = new PathString();
            userRepoMock.Setup(x => x.Create(user)).Returns(user);

            //Act
            CreatedResult result = (CreatedResult)userController.Post(user);
            //Assert
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(user.Id, ((User)result.Value).Id);
        }

        [Fact]
        public void PostBadRequestTest()
        {
            //Arrange
            var userRepoMock = new Mock<IUser>();
            var user = new User() { Id = 1, FirstName = "firstname1", LastName = "lastname1", Email = "f1@gmail.com", Password = "1234" };
            var userController = new UserController(userRepoMock.Object);
            
            //Act
            ActionResult result = userController.Post(user);
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PutBadRequestTest()
        {
            //Arrange
            var userRepoMock = new Mock<IUser>();
            var user = new User() { Id = 1, FirstName = "firstname1", LastName = "lastname1", Email = "f1@gmail.com", Password = "1234" };
            var userController = new UserController(userRepoMock.Object);

            //Act
            ActionResult result = userController.Put(user);
            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutTest()
        {
            //Arrange
            var userRepoMock = new Mock<IUser>();
            var user = new User() { Id = 1, FirstName = "firstname1", LastName = "lastname1", Email = "f1@gmail.com", Password = "1234" };
            var userController = new UserController(userRepoMock.Object);
            userRepoMock.Setup(x => x.Update(user)).Returns(user);

            //Act
            OkObjectResult result = (OkObjectResult)userController.Put(user);

            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(user.Id, ((User)result.Value).Id);
        }
    }
}

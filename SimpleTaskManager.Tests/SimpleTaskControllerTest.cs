using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleTaskManager.API.Controllers;
using SimpleTaskManager.API.DAL;
using SimpleTaskManager.API.Model.BLL;
using SimpleTaskManager.API.Model.BLL.Repository;
using SimpleTaskManager.API.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleTaskManager.Tests
{
    public class SimpleTaskControllerTest
    {
        private SimpleTasksController _controller;
        private List<SimpleTask> _simpleTasks;

        public SimpleTaskControllerTest()
        {
            _simpleTasks = InitializeList();

            Mock<ISimpleTaskRepository> mockRepo = new Mock<ISimpleTaskRepository>();
            mockRepo.Setup(m => m.GetAll())
                .Returns(() => _simpleTasks);
            _controller = new SimpleTasksController(mockRepo.Object);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.Get();

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Post_WhenCalled_ReturnsOkResult()
        {
            var newTask = new SimpleTaskBaseDto
            {
                Name = "New Task 1",
                CreatedBy = "Mikk"
            };

            var result = _controller.Post(newTask);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void PostDuplicateName_WhenCalled_ReturnsBadRequestResult()
        {
            var newTask = new SimpleTaskBaseDto
            {
                Name = "Task 1",
                CreatedBy = "Mikk"
            };

            var result = _controller.Post(newTask);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        private List<SimpleTask> InitializeList()
        {
            _simpleTasks = new List<SimpleTask>();
            var now = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                _simpleTasks.Add(new SimpleTask
                {
                    CreatedBy = "Mikk",
                    CreationDateTime = DateTime.Now,
                    LastUpdateBy = "Matt",
                    LastUpdateDateTime = DateTime.Now,
                    Name = $"Task {i}",
                    NumberOfUpdates = 1,
                    Status = Status.Open
                });
            }

            return _simpleTasks;
        }
    }
}

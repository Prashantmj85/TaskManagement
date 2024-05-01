using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagement.Controllers;
using TaskManagement.Model;


namespace TaskManagement.RestApi.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class TaskManagerControllerTests
    {
        [Test]
        public void Create_ValidTask_Returns201Created()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TaskManagerController>>();
            var controller = new TaskManagerController(loggerMock.Object);
            var newTask = new TaskModel { TaskId = 3, Title = "New Task", Description = "New Description", DueDate = DateTime.Today.AddDays(21), IsCompleted = false };

            // Act
            var result = controller.Create(newTask) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(newTask, result.Value);
        }

        [Test]
        public void Create_NullTask_ReturnsBadRequest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TaskManagerController>>();
            var controller = new TaskManagerController(loggerMock.Object);

            // Act
            var result = controller.Create(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Test]
        public void Create_InvalidModelState_ReturnsBadRequestWithModelState()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TaskManagerController>>();
            var controller = new TaskManagerController(loggerMock.Object);
            controller.ModelState.AddModelError("Title", "Title is required");
            var invalidTask = new TaskModel();

            // Act
            var result = controller.Create(invalidTask) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(controller.ModelState, result.Value);
        }

        [Test]
        public void Get_ReturnsListOfTasks()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TaskManagerController>>();
            var controller = new TaskManagerController(loggerMock.Object);

            // Act
            var result = controller.Get() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            var taskVM = Assert.IsType<TaskViewModel>(result.Value);
            Assert.Equal(2, taskVM.TotalTask);
            Assert.Equal(10, taskVM.PageSize);
            Assert.Equal(2, taskVM.Tasks.Count);
        }

        [Test]
        public void GetTask_ExistingId_ReturnsTask()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TaskManagerController>>();
            var controller = new TaskManagerController(loggerMock.Object);
            var taskId = 1;

            // Act
            var result = controller.GetTask(taskId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            var task = Assert.IsType<TaskModel>(result.Value);
            Assert.Equal(taskId, task.TaskId);
        }

        [Test]
        public void GetTask_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<TaskManagerController>>();
            var controller = new TaskManagerController(loggerMock.Object);
            var nonExistingId = 100;

            // Act
            var result = controller.GetTask(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

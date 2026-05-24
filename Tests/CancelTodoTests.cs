using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TodoManagementApp.Business.Services;
using TodoManagementApp.DataAccess.Interfaces;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Domain.Enums;
using Xunit;

namespace TodoManagementApp.Tests
{
    public class CancelTodoTests
    {
        [Fact]
        public void T3_1_CancelTodo_Success()
        {
            var mockTodoRepo = new Mock<ITodoRepository>();
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var todo = new TodoTask { Id = ObjectId.GenerateNewId(), Content = "test", Status = TodoStatus.Active };
            mockTodoRepo.Setup(r => r.GetById(It.IsAny<string>())).Returns(todo);
            var service = new TodoService(mockTodoRepo.Object, mockEmployeeRepo.Object);
            service.CancelTodo(todo.Id.ToString());
            mockTodoRepo.Verify(r => r.Update(It.Is<TodoTask>(t => t.Status == TodoStatus.Cancelled)), Times.Once);
        }

        [Fact]
        public void T3_2_NoTodoSelected()
        {
            var mockTodoRepo = new Mock<ITodoRepository>();
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            mockTodoRepo.Setup(r => r.GetById(It.IsAny<string>())).Returns((TodoTask)null);
            var service = new TodoService(mockTodoRepo.Object, mockEmployeeRepo.Object);
            service.CancelTodo(ObjectId.GenerateNewId().ToString());
            mockTodoRepo.Verify(r => r.Update(It.IsAny<TodoTask>()), Times.Never);
        }
    }
}

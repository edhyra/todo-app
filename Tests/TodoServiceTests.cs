using System.Collections.Generic;
using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TodoManagementApp.Business.Services;
using TodoManagementApp.DataAccess.Interfaces;
using TodoManagementApp.Domain.Entities;
using Xunit;

namespace TodoManagementApp.Tests
{
    public class TodoServiceTests
    {
        [Fact]
        public void T2_1_CreateTodo_Success()
        {
            var mockTodoRepo = new Mock<ITodoRepository>();
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var service = new TodoService(mockTodoRepo.Object, mockEmployeeRepo.Object);
            var managerId = ObjectId.GenerateNewId().ToString();
            var emp1 = ObjectId.GenerateNewId().ToString();
            var emp2 = ObjectId.GenerateNewId().ToString();
            service.CreateTodoForEmployees(managerId, new List<string> { emp1, emp2 }, "Hello");
            mockTodoRepo.Verify(r => r.Insert(It.IsAny<TodoTask>()), Times.Exactly(2));
        }

        [Fact]
        public void T2_2_ContentEmpty()
        {
            var mockTodoRepo = new Mock<ITodoRepository>();
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var service = new TodoService(mockTodoRepo.Object, mockEmployeeRepo.Object);
            service.CreateTodoForEmployees(ObjectId.GenerateNewId().ToString(), new List<string> { ObjectId.GenerateNewId().ToString() }, "");
            mockTodoRepo.Verify(r => r.Insert(It.IsAny<TodoTask>()), Times.Never);
        }

        [Fact]
        public void T2_3_NoEmployeeSelected()
        {
            var mockTodoRepo = new Mock<ITodoRepository>();
            var mockEmployeeRepo = new Mock<IEmployeeRepository>();
            var service = new TodoService(mockTodoRepo.Object, mockEmployeeRepo.Object);
            service.CreateTodoForEmployees(ObjectId.GenerateNewId().ToString(), new List<string>(), "Some content");
            mockTodoRepo.Verify(r => r.Insert(It.IsAny<TodoTask>()), Times.Never);
        }
    }
}

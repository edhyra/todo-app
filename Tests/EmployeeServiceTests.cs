using System;
using FluentAssertions;
using MongoDB.Bson;
using Moq;
using TodoManagementApp.Business.Services;
using TodoManagementApp.DataAccess.Interfaces;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Presentation.Forms;
using Xunit;

namespace TodoManagementApp.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void T1_1_AddEmployee_Success()
        {
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(r => r.Insert(It.IsAny<Employee>()));
            var service = new EmployeeService(mockRepo.Object);
            var managerId = ObjectId.GenerateNewId().ToString();
            var (employee, accessCode) = service.AddEmployee(managerId, "John Doe", "E001");
            mockRepo.Verify(r => r.Insert(It.Is<Employee>(e => e.Name == "John Doe" && e.EmployeeId == "E001")), Times.Once);
            accessCode.Length.Should().Be(10);
        }

        [Fact]
        public void T1_2_AddEmployee_EmptyFields()
        {
            Assert.True(AddEmployeeForm.HasEmptyFields(null, "id"));
            Assert.True(AddEmployeeForm.HasEmptyFields("name", ""));
            Assert.False(AddEmployeeForm.HasEmptyFields("name", "id"));
        }
    }
}

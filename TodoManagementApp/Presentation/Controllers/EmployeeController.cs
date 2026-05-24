using System.Collections.Generic;
using TodoManagementApp.Business.Services;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Domain.Enums;

namespace TodoManagementApp.Presentation.Controllers
{
    public class EmployeeController
    {
        private readonly TodoService _todoService = new TodoService();

        public List<TodoTask> GetTodos(string employeeObjectId)
        {
            return _todoService.GetTodosByEmployee(employeeObjectId);
        }

        public void UpdateTodoStatus(string todoId, TodoStatus newStatus)
        {
            _todoService.UpdateTodoStatus(todoId, newStatus);
        }

        public TodoTask? GetTodoById(string todoId)
        {
            return _todoService.GetTodoById(todoId);
        }
    }
}

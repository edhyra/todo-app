using System.Collections.Generic;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Domain.Enums;

namespace TodoManagementApp.Business.Interfaces
{
    public interface ITodoService
    {
        void CreateTodoForEmployees(string managerObjectId, IEnumerable<string> employeeObjectIds, string content);
        List<TodoTask> GetTodosByEmployee(string employeeObjectId);
        List<TodoTask> GetTodosByManager(string managerObjectId);
        TodoTask? GetTodoById(string todoId);
        void CancelTodo(string todoId);
        void UpdateTodoStatus(string todoId, TodoStatus newStatus);
    }
}

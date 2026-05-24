using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using TodoManagementApp.Business.Interfaces;
using TodoManagementApp.DataAccess.Repositories;
using TodoManagementApp.DataAccess.Interfaces;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Domain.Enums;

namespace TodoManagementApp.Business.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepo;
        private readonly IEmployeeRepository _employeeRepo;

        public TodoService(ITodoRepository? todoRepo = null, IEmployeeRepository? employeeRepo = null)
        {
            _todoRepo = todoRepo ?? new TodoRepository();
            _employeeRepo = employeeRepo ?? new EmployeeRepository();
        }

        public void CreateTodoForEmployees(string managerObjectId, IEnumerable<string> employeeObjectIds, string content)
        {
            if (string.IsNullOrEmpty(content)) return;
            if (content.Length > 256) content = content.Substring(0, 256);
            var mid = ObjectId.Parse(managerObjectId);
            foreach (var eo in employeeObjectIds)
            {
                var todo = new TodoTask
                {
                    Content = content,
                    ManagerId = mid,
                    EmployeeId = ObjectId.Parse(eo),
                    Status = TodoStatus.Active,
                    CreatedAt = DateTime.UtcNow
                };
                _todoRepo.Insert(todo);
            }
        }

        public List<TodoTask> GetTodosByEmployee(string employeeObjectId)
        {
            return _todoRepo.GetByEmployeeId(employeeObjectId);
        }

        public List<TodoTask> GetTodosByManager(string managerObjectId)
        {
            return _todoRepo.GetByManagerId(managerObjectId);
        }

        public TodoTask? GetTodoById(string todoId)
        {
            return _todoRepo.GetById(todoId);
        }

        public void CancelTodo(string todoId)
        {
            var todo = _todoRepo.GetById(todoId);
            if (todo == null) return;
            todo.Status = TodoStatus.Cancelled;
            todo.UpdatedAt = DateTime.UtcNow;
            _todoRepo.Update(todo);
        }

        public void UpdateTodoStatus(string todoId, TodoStatus newStatus)
        {
            var todo = _todoRepo.GetById(todoId);
            if (todo == null) return;
            if (todo.Status == TodoStatus.Cancelled) return;
            todo.Status = newStatus;
            todo.UpdatedAt = DateTime.UtcNow;
            _todoRepo.Update(todo);
        }
    }
}

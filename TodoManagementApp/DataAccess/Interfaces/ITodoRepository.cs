using System.Collections.Generic;
using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.DataAccess.Interfaces
{
    public interface ITodoRepository
    {
        void Insert(TodoTask todo);
        List<TodoTask> GetByEmployeeId(string employeeId);
        List<TodoTask> GetByManagerId(string managerId);
        TodoTask? GetById(string id);
        void Update(TodoTask todo);
    }
}

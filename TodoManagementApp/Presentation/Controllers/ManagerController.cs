using System.Collections.Generic;
using TodoManagementApp.Business.Services;
using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.Presentation.Controllers
{
    public class ManagerController
    {
        private readonly EmployeeService _employeeService = new EmployeeService();
        private readonly TodoService _todoService = new TodoService();

        public (Employee employee, string accessCode) AddEmployee(string managerObjectId, string name, string employeeId)
        {
            return _employeeService.AddEmployee(managerObjectId, name, employeeId);
        }

        public List<Employee> GetEmployees(string managerObjectId)
        {
            return _employeeService.GetEmployeesForManager(managerObjectId);
        }

        public Employee? GetEmployeeById(string id)
        {
            return _employeeService.GetById(id);
        }

        public List<TodoTask> GetTodosByManager(string managerObjectId)
        {
            return _todoService.GetTodosByManager(managerObjectId);
        }

        public List<TodoTask> GetTodosByEmployee(string employeeObjectId)
        {
            return _todoService.GetTodosByEmployee(employeeObjectId);
        }

        public void CreateTodoForEmployees(string managerObjectId, IEnumerable<string> employeeObjectIds, string content)
        {
            _todoService.CreateTodoForEmployees(managerObjectId, employeeObjectIds, content);
        }

        public void CancelTodo(string todoId)
        {
            _todoService.CancelTodo(todoId);
        }

        public void TerminateEmployee(string employeeObjectId)
        {
            _employeeService.TerminateEmployee(employeeObjectId);
        }
    }
}

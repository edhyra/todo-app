using System.Collections.Generic;
using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.Business.Interfaces
{
    public interface IEmployeeService
    {
        (Employee employee, string accessCode) AddEmployee(string managerObjectId, string name, string employeeId);
        List<Employee> GetEmployeesForManager(string managerObjectId);
        Employee? GetById(string id);
        void TerminateEmployee(string employeeObjectId);
    }
}

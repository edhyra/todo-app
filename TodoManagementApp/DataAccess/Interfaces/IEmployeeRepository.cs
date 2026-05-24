using TodoManagementApp.Domain.Entities;
using System.Collections.Generic;

namespace TodoManagementApp.DataAccess.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee? GetByAccessCode(string accessCode);
        Employee? GetByEmployeeId(string employeeId);
        Employee? GetById(string id);
        List<Employee> GetByManagerId(string managerId);
        void Insert(Employee employee);
        void Update(Employee employee);
    }
}

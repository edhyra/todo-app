using System;
using System.Collections.Generic;
using MongoDB.Bson;
using TodoManagementApp.Business.Interfaces;
using TodoManagementApp.DataAccess.Repositories;
using TodoManagementApp.DataAccess.Interfaces;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Utils.Helpers;

namespace TodoManagementApp.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeService(IEmployeeRepository? employeeRepo = null)
        {
            _employeeRepo = employeeRepo ?? new EmployeeRepository();
        }

        public (Employee employee, string accessCode) AddEmployee(string managerObjectId, string name, string employeeId)
        {
            var code = CodeGenerator.Generate(10);
            var emp = new Employee
            {
                Name = name,
                EmployeeId = employeeId,
                AccessCodeHash = HashingHelper.Hash(code),
                Active = true,
                ManagerId = ObjectId.Parse(managerObjectId),
                CreatedAt = DateTime.UtcNow
            };
            _employeeRepo.Insert(emp);
            return (emp, code);
        }

        public List<Employee> GetEmployeesForManager(string managerObjectId)
        {
            return _employeeRepo.GetByManagerId(managerObjectId);
        }

        public void TerminateEmployee(string employeeObjectId)
        {
            var emp = _employeeRepo.GetById(employeeObjectId);
            if (emp == null) return;
            emp.Active = false;
            _employeeRepo.Update(emp);
        }

        public Employee? GetById(string id)
        {
            return _employeeRepo.GetById(id);
        }
    }
}

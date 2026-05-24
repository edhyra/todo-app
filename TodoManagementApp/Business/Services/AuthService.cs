using System;
using TodoManagementApp.Business.Interfaces;
using TodoManagementApp.DataAccess.Repositories;
using TodoManagementApp.Domain.Entities;
using TodoManagementApp.Utils.Helpers;

namespace TodoManagementApp.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly ManagerRepository _managerRepo = new ManagerRepository();
        private readonly EmployeeRepository _employeeRepo = new EmployeeRepository();

        public (string? role, Employee? employee, Manager? manager) Login(string accessCode)
        {
            if (!string.IsNullOrEmpty(DataAccess.Database.MongoDbContext.AdminSeed) && accessCode == DataAccess.Database.MongoDbContext.AdminSeed)
            {
                return ("SEED", null, null);
            }

            var manager = _managerRepo.GetByAccessCode(accessCode);
            if (manager != null) return ("Manager", null, manager);

            var employee = _employeeRepo.GetByAccessCode(accessCode);
            if (employee != null && employee.Active) return ("Employee", employee, null);

            return (null, null, null);
        }

        public Manager CreateManager(string name, string employeeId, string accessCode)
        {
            var manager = new Manager
            {
                Name = name,
                EmployeeId = employeeId,
                AccessCodeHash = HashingHelper.Hash(accessCode),
                CreatedAt = DateTime.UtcNow
            };
            _managerRepo.Insert(manager);
            return manager;
        }
    }
}

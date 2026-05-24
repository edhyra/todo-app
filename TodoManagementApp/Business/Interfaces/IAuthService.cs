using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.Business.Interfaces
{
    public interface IAuthService
    {
        (string? role, Employee? employee, Manager? manager) Login(string accessCode);
        Manager CreateManager(string name, string employeeId, string accessCode);
    }
}

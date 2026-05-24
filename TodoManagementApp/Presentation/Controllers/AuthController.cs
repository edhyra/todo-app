using TodoManagementApp.Business.Services;
using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.Presentation.Controllers
{
    public class AuthController
    {
        private readonly AuthService _authService = new AuthService();

        public (string? role, Employee? employee, Manager? manager) Login(string accessCode)
        {
            return _authService.Login(accessCode);
        }

        public Manager CreateManager(string name, string employeeId, string accessCode)
        {
            return _authService.CreateManager(name, employeeId, accessCode);
        }
    }
}

using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.DataAccess.Interfaces
{
    public interface IManagerRepository
    {
        Manager? GetByAccessCode(string accessCode);
        Manager? GetByEmployeeId(string employeeId);
        void Insert(Manager manager);
    }
}

using MongoDB.Driver;
using System.Collections.Generic;
using TodoManagementApp.DataAccess.Database;
using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.DataAccess.Repositories
{
    public class ManagerRepository
    {
        private readonly IMongoCollection<Manager> _col;
        public ManagerRepository()
        {
            _col = MongoDbContext.GetCollection<Manager>("managers");
        }

        public Manager? GetByAccessCode(string accessCode)
        {
            var all = _col.Find(Builders<Manager>.Filter.Empty).ToList();
            foreach (var m in all)
            {
                if (!string.IsNullOrEmpty(m.AccessCodeHash) && BCrypt.Net.BCrypt.Verify(accessCode, m.AccessCodeHash))
                    return m;
            }
            return null;
        }

        public Manager? GetByEmployeeId(string employeeId)
        {
            return _col.Find(m => m.EmployeeId == employeeId).FirstOrDefault();
        }

        public void Insert(Manager manager)
        {
            _col.InsertOne(manager);
        }
    }
}

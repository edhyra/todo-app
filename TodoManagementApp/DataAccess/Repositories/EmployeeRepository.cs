using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using TodoManagementApp.DataAccess.Database;
using TodoManagementApp.DataAccess.Interfaces;
using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _col;
        public EmployeeRepository()
        {
            _col = MongoDbContext.GetCollection<Employee>("employees");
        }

        public Employee? GetByAccessCode(string accessCode)
        {
            var all = _col.Find(Builders<Employee>.Filter.Empty).ToList();
            foreach (var e in all)
            {
                if (!string.IsNullOrEmpty(e.AccessCodeHash) && BCrypt.Net.BCrypt.Verify(accessCode, e.AccessCodeHash))
                    return e;
            }
            return null;
        }

        public Employee GetByEmployeeId(string employeeId)
        {
            return _col.Find(e => e.EmployeeId == employeeId).FirstOrDefault();
        }

        public Employee GetById(string id)
        {
            var oid = ObjectId.Parse(id);
            return _col.Find(e => e.Id == oid).FirstOrDefault();
        }

        public List<Employee> GetByManagerId(string managerId)
        {
            if (string.IsNullOrEmpty(managerId)) return new List<Employee>();
            var oid = ObjectId.Parse(managerId);
            return _col.Find(e => e.ManagerId == oid).ToList();
        }

        public void Insert(Employee employee)
        {
            _col.InsertOne(employee);
        }

        public void Update(Employee employee)
        {
            _col.ReplaceOne(e => e.Id == employee.Id, employee);
        }
    }
}

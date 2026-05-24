using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using TodoManagementApp.DataAccess.Database;
using TodoManagementApp.DataAccess.Interfaces;
using TodoManagementApp.Domain.Entities;

namespace TodoManagementApp.DataAccess.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IMongoCollection<TodoTask> _col;
        public TodoRepository()
        {
            _col = MongoDbContext.GetCollection<TodoTask>("todos");
        }

        public void Insert(TodoTask todo) => _col.InsertOne(todo);

        public List<TodoTask> GetByEmployeeId(string employeeId)
        {
            var oid = ObjectId.Parse(employeeId);
            return _col.Find(t => t.EmployeeId == oid).ToList();
        }

        public List<TodoTask> GetByManagerId(string managerId)
        {
            var oid = ObjectId.Parse(managerId);
            return _col.Find(t => t.ManagerId == oid).ToList();
        }

        public TodoTask? GetById(string id)
        {
            var oid = ObjectId.Parse(id);
            return _col.Find(t => t.Id == oid).FirstOrDefault();
        }

        public void Update(TodoTask todo)
        {
            _col.ReplaceOne(t => t.Id == todo.Id, todo);
        }
    }
}

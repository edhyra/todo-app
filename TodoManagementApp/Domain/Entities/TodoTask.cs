using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TodoManagementApp.Domain.Enums;

namespace TodoManagementApp.Domain.Entities
{
    public class TodoTask
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public required string Content { get; set; }
        public ObjectId ManagerId { get; set; }
        public ObjectId EmployeeId { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}

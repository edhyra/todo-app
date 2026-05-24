using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TodoManagementApp.Domain.Entities
{
    public class Manager
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string EmployeeId { get; set; }
        public required string AccessCodeHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using System;
using MongoDB.Driver;

namespace TodoManagementApp.DataAccess.Database
{
    public static class MongoDbContext
    {
        private static IMongoDatabase? _database;
        public static string? AdminSeed { get; private set; }

        public static void Initialize(string connectionString, string databaseName, string adminSeed)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            AdminSeed = adminSeed;
        }

        public static IMongoCollection<T> GetCollection<T>(string name)
        {
            if (_database == null) throw new InvalidOperationException("MongoDbContext is not initialized. Call MongoDbContext.Initialize(...) before using the context.");
            return _database.GetCollection<T>(name);
        }
    }
}

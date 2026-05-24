using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using TodoManagementApp.DataAccess.Database;
using TodoManagementApp.Presentation.Forms;

namespace TodoManagementApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("Config/appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var conn = config["MongoConnectionString"] ?? "mongodb://localhost:27017";
            var dbName = config["DatabaseName"] ?? "TodoManagementDb";
            var seed = config["AdminSeedAccessCode"] ?? "admin";

            MongoDbContext.Initialize(conn, dbName, seed);

            Application.Run(new LoginForm());
        }
    }
}

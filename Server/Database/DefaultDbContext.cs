using Data.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using TheGodfatherGM.Data;

namespace TheGodfatherGM.Server.DBManager
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(string connectionString) : base(connectionString)
        {
        }


        protected override void OnModelCreating(DbModelBuilder builder)
        {
           builder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Account.Account> Account { get; set; }
        public DbSet<Ban> Ban { get; set; }
        public DbSet<Character> Character { get; set; }

        public DbSet<Group> Group { get; set; }
        public DbSet<GroupMember> GroupMember { get; set; }

        public DbSet<Job> Job { get; set; }
        public DbSet<Data.Property> Property { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
    }

    public class ContextFactory : IDbContextFactory<DefaultDbContext>
    {
        private static string ConnectionString;

        public static void SetConnectionParameters(string serverAddress, string username, string password, string database, uint port = 3306)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder()
            {
                Server = serverAddress,
                UserID = username,
                Password = password,
                Database = database,
                Port = port
            };

            ConnectionString = connectionStringBuilder.ToString();
        }

        private static DefaultDbContext _instance;

        public static DefaultDbContext Instance
        {
            get
            {
                if (_instance != null) return _instance;
                return _instance = new ContextFactory().Create();
            }
            private set { }
        }

        public DefaultDbContext Create()
        {
            if (string.IsNullOrEmpty(ConnectionString)) throw new InvalidOperationException("Please set the connection parameters before trying to instantiate a database connection.");

            return new DefaultDbContext(ConnectionString);
        }

    }
}
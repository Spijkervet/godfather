//using TheGodfatherGM.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheGodfatherGM.Data;

namespace TheGodfatherGM.Web.Models
{
    public class DefaultDbContext : IdentityDbContext<Account, IdentityRole, string>
    {
        private static DefaultDbContext _instance;

        public static string ConnectionString;

        public DefaultDbContext() { } // I guess?

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /*builder.Entity<Ban>()
                .HasOne(ban => (Account)ban.Issuer);
               */
            builder.Entity<Ban>()
                .HasOne(ban => (Account)ban.Target)
                .WithMany(account => account.Ban);
                
            builder.Entity<Character>()
                .HasOne(character => (Account)character.Account)
                .WithMany(account => account.Character);

            builder.Entity<IdentityUserLogin<string>>()
                .HasKey(iul => iul.UserId);

        }

        public static DefaultDbContext Instance => _instance ?? (_instance = new DefaultDbContext());

        public DbSet<Ban> Ban { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<GroupMember> GroupMember { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }

    }
}

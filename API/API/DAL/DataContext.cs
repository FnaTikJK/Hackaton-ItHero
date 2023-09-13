using API.Modules.AccountsModule.Entity;
using API.Modules.ProfilesModule.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public void RecreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<AccountEntity> Accounts => Set<AccountEntity>();
        public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
    }
}

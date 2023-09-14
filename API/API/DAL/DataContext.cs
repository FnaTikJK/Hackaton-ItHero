using API.Modules.AccountsModule.Entity;
using API.Modules.ApplicationsModule.Entity;
using API.Modules.CompaniesModule.Entity;
using API.Modules.ProfilesModule.Entity;
using API.Modules.SpecializationsModule.Entity;
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
            DbDefaultInitializer.FillDbByDefaultValues(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<AccountEntity> Accounts => Set<AccountEntity>();
        public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
        public DbSet<CompanyEntity> Companies => Set<CompanyEntity>();
        public DbSet<ApplicationEntity> Applications => Set<ApplicationEntity>();
        public DbSet<SpecializationEntity> Specializations => Set<SpecializationEntity>();
    }
}

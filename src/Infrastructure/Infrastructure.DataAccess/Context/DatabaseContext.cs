using Domain.Model;
using Infrastructure.DataAccess.DataBaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.DataAccess.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Goal> DtGoals { get; set; }
        public DbSet<GTask> DtTasks { get; set; }
        public DbSet<GState> DtStatus { get; set; }

        public DatabaseContext() : base()
        {
            //Database.SetInitializer(new DatabaseInitializer());
        }
        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public void Save()
        {
            this.SaveChanges();
        }

        public IEnumerable<EntityEntry> GetChanges()
        {
            return this.ChangeTracker.Entries();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=74.208.60.29\SQLEXPRESS;Database=neitek;User Id=dbneitek;Password=neiiUsr2024#.$sW;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApiUserConfiguration());
            modelBuilder.ApplyConfiguration(new GoalConfiguration());
            modelBuilder.ApplyConfiguration(new GTaskConfiguration());
            modelBuilder.ApplyConfiguration(new GStateConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
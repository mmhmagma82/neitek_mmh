using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.DataAccess.Context
{
    public interface IDatabaseContext
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<Goal> DtGoals { get; set; }
        DbSet<GTask> DtTasks { get; set; }
        DbSet<GState> DtStatus { get; set; }
        
        void Save();
        IEnumerable<EntityEntry> GetChanges();
    }
}
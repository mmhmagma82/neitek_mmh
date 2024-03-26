using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.DataBaseConfig
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            #region TableFields Configuration

            builder.ToTable("adm_goal");
            builder.HasKey(f => f.GoalId);
            builder.Property(f => f.GoalId).HasColumnName("goalid").IsRequired();
            builder.HasIndex(f => f.GoalId).HasDatabaseName("pk_goa_idx").IsUnique();
            builder.Property(f => f.Name).HasColumnName("name").HasMaxLength(80).IsRequired();
            builder.Property(f => f.RegisterDate).HasColumnName("register_date").IsRequired();
            builder.Property(f => f.TotalTasks).HasColumnName("total_tasks").IsRequired();
            builder.Property(f => f.Percentege).HasColumnName("percentege").IsRequired();

            #endregion

            #region Foreign Keys Configuration

            #endregion

            #region Initial Seed Table

            #endregion

        }
    }
}
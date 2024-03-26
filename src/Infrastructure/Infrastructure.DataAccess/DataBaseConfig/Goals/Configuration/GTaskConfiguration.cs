using Common.Model;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.DataAccess.DataBaseConfig
{
    public class GTaskConfiguration : IEntityTypeConfiguration<GTask>
    {
        public void Configure(EntityTypeBuilder<GTask> builder)
        {
            #region TableFields Configuration

            builder.ToTable("adm_task");
            builder.HasKey(f => f.TaskId);
            builder.Property(f => f.TaskId).HasColumnName("taskid").IsRequired();
            builder.HasIndex(f => f.TaskId).HasDatabaseName("pk_tas_idx").IsUnique();
            builder.Property(f => f.Name).HasColumnName("name").HasMaxLength(80).IsRequired();
            builder.Property(f => f.RegisterDate).HasColumnName("register_date").IsRequired();
            builder.Property(f => f.IsImportant).HasColumnName("is_important").IsRequired();

            #endregion

            #region Foreign Keys Configuration

            builder.HasOne(f => f.Goal).WithMany().HasForeignKey(new string[] { "goalid" }).HasConstraintName("fk_goa_tas").OnDelete(DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(f => f.GState).WithMany().HasForeignKey(new string[] { "stateid" }).HasConstraintName("fk_goa_sta").OnDelete(DeleteBehavior.NoAction).IsRequired();

            builder.HasIndex("goalid").HasDatabaseName("fk_goa_tas_idx");
            builder.HasIndex("stateid").HasDatabaseName("fk_goa_sta_idx");

            #endregion

            #region Initial Seed Table

            #endregion

        }
    }
}
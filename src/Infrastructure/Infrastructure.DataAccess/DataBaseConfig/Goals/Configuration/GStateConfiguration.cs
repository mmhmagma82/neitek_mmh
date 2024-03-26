using Common.Model;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.DataAccess.DataBaseConfig
{
    public class GStateConfiguration : IEntityTypeConfiguration<GState>
    {
        public void Configure(EntityTypeBuilder<GState> builder)
        {
            #region TableFields Configuration

            builder.ToTable("adm_state");
            builder.HasKey(f => f.StateId);
            builder.Property(f => f.StateId).HasColumnName("stateid").IsRequired();
            builder.HasIndex(f => f.StateId).HasDatabaseName("pk_sta_idx").IsUnique();
            builder.Property(f => f.Name).HasColumnName("name").HasMaxLength(10).IsRequired();
            
            #endregion

            #region Foreign Keys Configuration

            #endregion

            #region Initial Seed Table

            builder.HasData(
                new GState()
                {
                    StateId = (int)TaskState.Open,
                    Name = "Abierta"
                },
                new GState()
                {
                    StateId = (int)TaskState.Closed,
                    Name = "Completada"
                }
            );

            #endregion

        }
    }
}
using System;
using Common.Model;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.DataBaseConfig
{
    public class ApiUserConfiguration : IEntityTypeConfiguration<ApiUser>
    {
        public void Configure(EntityTypeBuilder<ApiUser> builder)
        {
            #region TableFields Configuration

            builder.ToTable("adm_api_user");
            builder.HasKey(f => f.ApiUserId);
            builder.Property(f => f.ApiUserId).HasColumnName("apiuserid").IsRequired();
            builder.HasIndex(f => f.ApiUserId).HasDatabaseName("pk_apu_idx").IsUnique();
            builder.Property(f => f.Role).HasColumnName("role").HasMaxLength(500).IsRequired();
            builder.Property(f => f.UserName).HasColumnName("username").HasMaxLength(500).IsRequired();
            builder.Property(f => f.Password).HasColumnName("password").HasMaxLength(500).IsRequired();
            builder.Property(f => f.Project).HasColumnName("project").HasMaxLength(1000).IsRequired();
            builder.Property(f => f.Applicant).HasColumnName("applicant").HasMaxLength(2000).IsRequired();
            builder.Property(f => f.Expires).HasColumnName("expires").IsRequired();

            #endregion

            #region Foreign Keys Configuration
            

            #endregion

            #region Initial Seed Table

            builder.HasData(
                new ApiUser() { 
                    ApiUserId = 1, 
                    Role = "Admin", 
                    UserName = Crypto.Encrypt("neitek2024"), 
                    Password = Crypto.Encrypt("ramketG@24"),
                    Project = "Neitek", 
                    Applicant = "Neitek",
                    Expires = Convert.ToDateTime("2100-12-31 00:00:00"), 
                }
            );
            #endregion
        }
    }
}
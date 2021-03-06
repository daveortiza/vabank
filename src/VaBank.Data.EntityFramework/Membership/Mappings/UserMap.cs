﻿using System.Data.Entity.ModelConfiguration;
using VaBank.Core.Membership;
using VaBank.Core.Membership.Entities;
using VaBank.Data.EntityFramework.Common;

namespace VaBank.Data.EntityFramework.Membership.Mappings
{
    internal class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User", "Membership").HasKey(x => x.Id)
                .HasMany(x => x.Claims).WithRequired()
                .HasForeignKey(x => x.UserId);
            Property(x => x.Id).HasColumnName("UserID");
            Property(x => x.LockoutEndDateUtc).IsOptional();
            Property(x => x.PasswordHash).HasMaxLength(Restrict.Length.SecurityString)
                .IsRequired();
            Property(x => x.PasswordSalt).HasMaxLength(Restrict.Length.SecurityString)
                .IsRequired();
            Property(x => x.LockoutEnabled).IsRequired();
            Property(x => x.UserName).HasMaxLength(Restrict.Length.ShortName)
                .IsRequired();
            Property(x => x.AccessFailedCount).IsRequired();
            Property(x => x.Deleted).IsRequired();
            Property(x => x.RowVersion).IsRowVersion();
            HasRequired(x => x.Profile).WithRequiredPrincipal();
        }
    }
}
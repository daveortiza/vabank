﻿using System.Data.Entity.ModelConfiguration;
using VaBank.Core.App;
using VaBank.Core.App.Entities;
using VaBank.Data.EntityFramework.Common;

namespace VaBank.Data.EntityFramework.App.Mappings
{
    internal class ApplicationActionMap : EntityTypeConfiguration<ApplicationAction>
    {
        public ApplicationActionMap()
        {
            ToTable("Action", "App");
            HasKey(x => x.EventId);
            Property(x => x.Code).HasMaxLength(Restrict.Length.ShortName).IsRequired();
            Property(x => x.Data).IsMaxLength().IsOptional();
            Property(x => x.Description).HasMaxLength(Restrict.Length.BigString).IsOptional();
            Property(x => x.TimestampUtc).IsRequired();

            HasRequired(x => x.Operation).WithMany().Map(x => x.MapKey("OperationId"));
        }
    }
}

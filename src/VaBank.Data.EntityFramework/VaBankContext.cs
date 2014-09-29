﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using VaBank.Core.Entities;
using VaBank.Data.EntityFramework.Mappings;

namespace VaBank.Data.EntityFramework
{
    public class VaBankContext: DbContext
    {
        public VaBankContext(): base("Name=VaBank.Db")
        {
        }

        public VaBankContext(string connectionStringName): base("Name=" + connectionStringName)
        {
        }

        public IDbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LogMap());
        }
    }
}

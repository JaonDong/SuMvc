﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CpMvc.Data;
using SuMvc.Core.Domain.Tests;

namespace SuMvc.Data.EntityFramework
{
    public class MvcContext:NopObjectContext
    {
        public MvcContext(string nameOrConnectionString) : 
            base(nameOrConnectionString)
        {

        }

        public MvcContext() : base("name=connectionstring")
        {

        }


        public virtual IDbSet<TestEtity> TestEtities { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
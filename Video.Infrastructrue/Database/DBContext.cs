using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Video.Core.Entities;
using Video.Infrastructrue.Database.EntityConfigurations; 

namespace Video.Infrastructrue.Database
{
    public class DBContext :DbContext
    {
        public DBContext(DbContextOptions<DBContext> option) : base(option)
        {
            //Add-Migration Initial
            //Update-Database -Verbose
        }

        public DbSet<Account> Accounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
        } 
    }
}

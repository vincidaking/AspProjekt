using Apka2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.Model
{
    public class Context : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Law> Laws { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;initial catalog=ApkaDP;Integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

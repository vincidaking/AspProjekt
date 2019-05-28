using Apka2.Entities;
using Apka2.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                FirstName = "SA",
                LastName = "SA",
                Password = "123456",
                Role = RoleNames.Admin,
                Username = "Admin"
            });
        }
    }
}

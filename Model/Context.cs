using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.Model
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;initial catalog=ApkaDP;Integrated Security=true");
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-NGOLGBR;initial catalog=BloodDB;Integrated Security=true");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Person>()
            //    .HasMany(e => e.EventBloods)
            //    .WithOne(p => p.Person)

            //    ;

            //modelBuilder.Entity<Person>().ToTable("Person");                
            //modelBuilder.Entity<EventBlood>().ToTable("EventBlood");


        }



        public DbSet<User> User { get; set; }
        public DbSet<Law> Laws { get; set; }

    }
}

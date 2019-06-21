
using Apka2.Model;
using Microsoft.EntityFrameworkCore;

namespace Apka2.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Law> Laws { get; set; }
        //public DbSet<VoteType> VoteTypes { get; set; }


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
                Username = "admin",
                Password = "admin",
                Role = RoleNames.Admin,
            });


            //modelBuilder.Entity<VoteType>().HasData(
            //    new VoteType() { Id = 1, Name = "Za" },
            //    new VoteType() { Id = 2, Name = "Przeciw" },
            //    new VoteType() { Id = 3, Name = "Wstrzymanie" }
            //    );

            //modelBuilder.Entity<Law>()
            //    .HasMany<VoteType>(n => n.VoteTypes)
            //    .WithOne(x => x.Law);

            //modelBuilder.Entity<VoteType>()
            //    .HasOne(n => n.Law)
            //    .WithMany(x => x.VoteTypes);



            ////wiele do wielu
            //modelBuilder.Entity<LawUser>()
            //    .HasKey(lu => new { lu.LawId, lu.UserId });

            //modelBuilder.Entity<LawUser>()
            //    .HasOne(lu => lu.Law)
            //    .WithMany(l => l.LawUsers)
            //    .HasForeignKey(lu => lu.LawId);

            //modelBuilder.Entity<LawUser>()
            //    .HasOne(lu => lu.User)
            //    .WithMany(u => u.LawUsers)
            //    .HasForeignKey(lu => lu.UserId);





        }
    }
}

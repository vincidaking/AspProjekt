
using Apka2.Model;
using Microsoft.EntityFrameworkCore;
using Apka2.ViewModel;
using Apka2.Services.Users;

namespace Apka2.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Law> Laws { get; set; }
        public DbSet<Model.Vote> Votes { get; set; }

        
        

        

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
                Username = "admin@admin.pl",
                
                Password = "lOaMF0Sbcf/4RpDWzLAtX8+9DPGn4x/RcvVodFSUJsri97GO",
                Role = RoleNames.Admin,
            });


            //modelBuilder.Entity<VoteType>().HasData(
            //    new VoteType() { Id = 1, Name = "Za" },
            //    new VoteType() { Id = 2, Name = "Przeciw" },
            //    new VoteType() { Id = 3, Name = "Wstrzymanie" }
            //    );


            //jeden do wielu


            modelBuilder.Entity<Law>()
                   .HasMany(x => x.Votes)
                   .WithOne(c => c.Law);

            modelBuilder.Entity<Model.Vote>()
                .HasOne(x => x.Law)
                .WithMany(c => c.Votes);



            modelBuilder.Entity<User>()
                   .HasMany(x => x.Votes)
                   .WithOne(c => c.User);

            modelBuilder.Entity<Model.Vote>()
                .HasOne(x => x.User)
                .WithMany(c => c.Votes);

        }

        //public DbSet<Apka2.ViewModel.VoteFromReact> VoteFromReact { get; set; }
    }
}

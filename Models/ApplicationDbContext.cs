using Microsoft.EntityFrameworkCore;

namespace AuthApplication.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "lameyiy197@gholar.com",
                Password = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", //password with HASH
                FirstName = "FirstName",
                LastName = "LastName",
                Address = "Address",
                BirthDate = new DateTime(1998, 1, 1),
                IsActive = true
            });
        }
    }
}

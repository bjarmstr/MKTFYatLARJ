using Microsoft.EntityFrameworkCore;
using MKTFY.Models.Entities;

namespace MKTFY.Repositories
{
    public class ApplicationDbContext : DbContext
    {

        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Category>().HasData(
                new Category { Name = "electronics" },
                new Category { Name = "realEstate"},
                new Category { Name = "vehicles" },
                new Category { Name = "furniture" },
                new Category { Name ="deals"}

            ); 
        }
        

    }
}

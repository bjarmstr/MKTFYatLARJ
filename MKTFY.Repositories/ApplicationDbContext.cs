using Microsoft.EntityFrameworkCore;
using MKTFY.Models.Entities;

namespace MKTFY.Repositories
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Listing> Listings { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SearchHistory> SearchHistories { get; set; }

        public virtual DbSet<FAQ> FAQs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Deals" },
                new Category { Id = 2, Name = "Cars & Vehicles" },
                new Category { Id = 3, Name = "Furniture" },
                new Category { Id = 4, Name = "Electronics" },
                new Category { Id = 5, Name = "Real Estate" }

            );
        }


    }
}

using Microsoft.EntityFrameworkCore;
using MKTFY.Models.Entities;

namespace MKTFY.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }
        public DbSet<Listing> Listings { get; set; }
    }
}

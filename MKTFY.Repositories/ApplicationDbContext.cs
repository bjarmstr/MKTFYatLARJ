﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "deals" },
                new Category { Id = 2, Name = "vehicles" },
                new Category { Id = 3, Name = "furniture" },
                new Category { Id = 4, Name = "electronics" },
                new Category { Id = 5, Name = "realEstate" }

            );
        }


    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EverGardenNew.Models;

namespace EverGardenNew.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantActivity> PlantActivities { get; set; }

        public DbSet<CategoryEdible> CategoryEdibles { get; set; }
        public DbSet<CategoryPlace> CategoryPlaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>().ToTable("Plant");
            modelBuilder.Entity<PlantActivity>().ToTable("PlantActivity");
            modelBuilder.Entity<CategoryEdible>().ToTable("CategoryEdible");
            modelBuilder.Entity<CategoryPlace>().ToTable("CategoryPlace");
            base.OnModelCreating(modelBuilder);
        }
    }
}

﻿using CatsBlog.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CatsBlog.Web.Data
{
    public class CatsDbContext : DbContext
    {
        public CatsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }

        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Cat>()
                .HasOne(c => c.Owner)
                .WithMany(o => o.Cats)
                .HasForeignKey(c => c.OwnerId);
        }
    }
}

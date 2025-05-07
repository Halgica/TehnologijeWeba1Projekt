using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL.DB
{
    public class TW1DbContext : DbContext
    {
        public TW1DbContext(DbContextOptions<TW1DbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<AuthUser> AuthUsers { get; set; }
        //public DbSet<AuthRole> AuthRoles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        //    modelBuilder.Entity<Resource>().HasIndex(r => r.Name).IsUnique();
        //}
    }
}

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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        //public DbSet<AuthUser> AuthUsers { get; set; }
        //public DbSet<AuthRole> AuthRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Resource>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Resource)
                .WithMany()
                .HasForeignKey(r => r.ResourceId);
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Slot)
                .WithMany()
                .HasForeignKey(r => r.SlotId);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.users)
                .WithOne();
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany();
            modelBuilder.Entity<Review>()
                .HasOne(r => r.EscapeRoom)
                .WithOne();
            modelBuilder.Entity<Promotion>().HasIndex(p => p.Code).IsUnique();
            modelBuilder.Entity<TimeSlot>(); // Sus


        }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMovieStore.Models.Contracts;
using OnlineMovieStore.Models.Models;
using System;
using System.Linq;

namespace OnlineMovieStore.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<WatchedMovies> WatchedMovies { get; set; }

        public DbSet<GenreMovie> GenreMovies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WatchedMovies>()
                .HasKey(wM => new { wM.UserId, wM.MovieId });

            modelBuilder.Entity<Order>()
                .HasKey(o => new { o.UserId, o.MovieId });

            modelBuilder.Entity<GenreMovie>()
                 .HasKey(o => new { o.MovieId, o.GenreId });

            modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "Admin", Id = 1.ToString(), NormalizedName = "Admin".ToUpper(), ConcurrencyStamp = "aaa" });

            modelBuilder.Entity<IdentityRole>()
                .HasData(new IdentityRole { Name = "User", Id = 2.ToString(), NormalizedName = "User".ToUpper(), ConcurrencyStamp = "bbb" });

            var adminUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Balance = 0,
                UserName = "VksAdmin",
                NormalizedUserName = "VksAdmin".ToUpper(),
                Email = "vksn@mail.com",
                NormalizedEmail = "vks@mail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+55555",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            var hashePass = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "!Password2018");
            adminUser.PasswordHash = hashePass;

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = 1.ToString(),
                UserId = adminUser.Id
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
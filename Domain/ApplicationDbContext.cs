using System;
using System.Collections.Generic;
using Coursework.Domain.Entities;
using Coursework.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Domain
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewGroup> ReviewGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = 1,
                UserName = "admin",
                AvatarUrl = "/Files/no_avatar.jpg",
                NormalizedUserName = "ADMIN",
                SecurityStamp = String.Empty,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "superpassword")
            });

            builder.Entity<ApplicationRole>().HasData(new ApplicationRole()
            {
                Id = 1,
                Name = "Admin"
            });

            builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole()
            {
                UserId = 1,
                RoleId = 1
            });

            builder.Entity<Review>()
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");

            builder.Entity<ReviewGroup>()
                .HasData(new List<ReviewGroup>()
                {
                    new ReviewGroup() {Id = 1, Name = "Movies"},
                    new ReviewGroup() {Id = 2, Name = "Events"},
                    new ReviewGroup() {Id = 3, Name = "Games"},
                    new ReviewGroup() {Id = 4, Name = "Books"}
                });
        }
    }
}

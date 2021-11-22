using System;
using Coursework.Domain.Entities;
using Coursework.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Domain
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = new Guid("{252352F7-E127-4C9D-AD06-DD6B859043D8}"),
                UserName = "admin",
                AvatarUrl = "/Files/no_avatar.jpg",
                NormalizedUserName = "ADMIN",
                SecurityStamp = String.Empty,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "superpassword")
            });

            builder.Entity<ApplicationRole>().HasData(new ApplicationRole()
            {
                Id = new Guid("{7A08F647-1C30-4453-B46B-A9AD1A79C168}"),
                Name = "Admin"
            });

            builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole()
            {
                UserId = new Guid("{252352F7-E127-4C9D-AD06-DD6B859043D8}"),
                RoleId = new Guid("{7A08F647-1C30-4453-B46B-A9AD1A79C168}")
            });
        }
    }
}
